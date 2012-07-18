#include <winsock2.h>
#include <ws2tcpip.h>
#include <stdio.h>
#include "ipheaders.h"
#include "tracy.h"
#include "tracy_aux.h"

#define CHUNK_SIZE 5

typedef struct _ProbeInfo {			//keeps information about one probe packet
	USHORT seq;						//seq of the packet
	int	ttl;						//ttl with which packet was sent
	struct in_addr address;			//response address
	int sent;						// is this probeinfo valid?
	int received, from_dst;
	unsigned long rtt;
} ProbeInfo;


static ProbeInfo probes[MAX_TTL];		//information about probes

static ProbeInfo *probe_by_seq(UINT seq)
{
	int i=seq-INITIAL_SEQ;
	if (i<0 || i>sizeof(probes)) return 0;
	return &probes[i];
}

int parallel(SOCKADDR_IN *dst, unsigned int timeout, RouteInfo *rinfo)
{
	int i, ttl, thettl, is_packet_recieved, icmp_type;
	unsigned short seq_resp;
	ProbeInfo *pinfo;
	SOCKET sr;
	int reached_dst = 0;
	ULONG start_test_timestamp = GetTickCount();
	SOCKADDR_IN from;
	int resent=0;

	for (i=0; i<sizeof(probes)/sizeof(ProbeInfo); i++) probes[i].sent=0;
	sr = init_icmp_socket();
	rinfo->cnt_packet_waste=0;
	rinfo->resp_ttl = UNDEFINED_TTL;

	for (ttl=1; ttl<=MAX_TTL && !reached_dst &&  
		GetTickCount()-start_test_timestamp<timeout*1000; ttl+=CHUNK_SIZE)
	{
		ULONG start_packet_timestamp = GetTickCount();

		// send a chunk of udp packets
		for (thettl=ttl; thettl<ttl+CHUNK_SIZE; thettl++)
		{
			probes[thettl].sent = 1;
			probes[thettl].received = 0;
			probes[thettl].from_dst = 0;
			probes[thettl].address.S_un.S_addr = 0;
			probes[thettl].seq = (unsigned short)(INITIAL_SEQ+thettl);
			probes[thettl].rtt = start_packet_timestamp;
			probes[thettl].ttl = thettl;
			send_udp_packet(dst, (unsigned short)thettl, probes[thettl].seq);
			rinfo->cnt_packet_waste++;
		}

		// wait for chunk of responses
		for (i=0; i<CHUNK_SIZE; i++) 
		{
			is_packet_recieved = wait_for_icmp(sr, &seq_resp, &icmp_type, &from, 0, timeout);
			if (!is_packet_recieved)		//we got no reply
			{					
				break;
			}
			// fprintf(stderr, "received seq %d from %s\n", seq_resp, inet_ntoa(from.sin_addr));
			pinfo = probe_by_seq(seq_resp);
			if (!pinfo) continue;	// this was not a legal seq.
			pinfo->received = 1;
			pinfo->address = from.sin_addr; 
			pinfo->rtt = GetTickCount()-pinfo->rtt;

			switch (icmp_type)
			{
			case ICMP_DEST_UNREACH:
				reached_dst = 1;
				pinfo->from_dst = 1;
				break;
			case ICMP_TTL_EXPIRE:
				break;
			default:
				fprintf(stderr, "parallel: unexpected icmp packet type %d ttl %d\n", icmp_type, pinfo->ttl);
			}
		}
	}
	if (!reached_dst)
	{
		rinfo->time_waste_on_test = GetTickCount() - start_test_timestamp;
		rinfo->rtt = UNDEFINED_RTT;
		rinfo->hopdist = UNDEFINED_DISTANCE;
		closesocket(sr);
		return 0;
	}

	// go through probes to build route info
	rinfo->path[0]=0;
	for (ttl=1; ttl<=MAX_TTL; ttl++)
	{
		pinfo = probe_by_seq(INITIAL_SEQ+ttl);
		if (!pinfo) continue;
		if (pinfo->received)
		{
			strncat(rinfo->path, inet_ntoa(pinfo->address), sizeof(rinfo->path));
			if (!pinfo->from_dst) strncat(rinfo->path, ", ", sizeof(rinfo->path));
		} else strncat(rinfo->path, "*, ", sizeof(rinfo->path));
		if (pinfo->from_dst)
		{
			rinfo->rtt = pinfo->rtt;
			rinfo->hopdist = pinfo->ttl;
			break;
		}
	}
	rinfo->time_waste_on_test = GetTickCount() - start_test_timestamp;
	closesocket(sr);
	return 1;
}