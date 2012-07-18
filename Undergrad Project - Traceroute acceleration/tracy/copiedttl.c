#include <winsock2.h>
#include <ws2tcpip.h>
#include <stdio.h>
#include "ipheaders.h"
#include "tracy.h"
#include "tracy_aux.h"

int copied_ttl(SOCKADDR_IN *dst, unsigned int timeout, RouteInfo *rinfo)
{
	SOCKET sr;
	SOCKADDR_IN from;
	int icmp_type, delta, is_packet_recieved, copiedttl;
	unsigned short seq_sent, seq_resp;
	ULONG start_test_timestamp = GetTickCount();

	sr = init_icmp_socket();
	
	strcpy(rinfo->path, UNDEFINED_PATH);
	rinfo->resp_ttl = UNDEFINED_TTL;
	rinfo->cnt_packet_waste = 0;
	seq_sent = INITIAL_SEQ;

	while(GetTickCount()-start_test_timestamp < timeout*1000)
	{
		ULONG start_packet_timestamp = GetTickCount();
		
		send_udp_packet(dst, MAX_TTL, seq_sent);
		
		rinfo->cnt_packet_waste++;

		is_packet_recieved = wait_for_icmp(sr, &seq_resp, &icmp_type, &from, &copiedttl, timeout);
		delta = GetTickCount()-start_packet_timestamp;
		if (!is_packet_recieved)		//we got no reply
		{					
		//	fprintf(stderr, "%3d  Request timed out\t", MAX_TTL);
			break;
		}		
		if(seq_sent == seq_resp)		//check if this is our ICMP packet
		{
			if (icmp_type == ICMP_DEST_UNREACH)
			{
				rinfo->time_waste_on_test = GetTickCount() - start_test_timestamp;
				rinfo->rtt = delta;
				rinfo->hopdist = MAX_TTL+1-copiedttl;
				closesocket(sr);
				return 1;
			}
		}
		//it was not a response to our packet. send it again		
	}

	closesocket(sr);
	return 0;
}
