#include <winsock2.h>
#include <ws2tcpip.h>
#include <stdio.h>
#include "tracy_aux.h"
#include "ipheaders.h"

char *name2sockaddr(char *hostname, SOCKADDR_IN *sockaddr)
{
	HOSTENT *hp;

	memset(sockaddr, 0 ,sizeof(SOCKADDR_IN));
	sockaddr->sin_family = AF_INET;	
	sockaddr->sin_addr.s_addr = inet_addr(hostname);
	sockaddr->sin_port = DEST_PORT;					
	if (sockaddr->sin_addr.s_addr != INADDR_NONE)	
		return hostname;
											
	hp = gethostbyname(hostname);					
	if (hp) {										
		sockaddr->sin_family = hp->h_addrtype;			
		memcpy((char *)&(sockaddr->sin_addr), hp->h_addr,hp->h_length);
	//	return hp->h_name;
		return hostname;
	}
	fprintf(stderr, "tracy: unknown host %s\n", hostname);
	return 0;
}

int init_winsock(void)
{
	WSADATA wsaData;
	
	if (WSAStartup(MAKEWORD(2,1),&wsaData) != 0)	
	{
		fprintf(stderr, "WSAStartup failed: %d\n", GetLastError()); 
		exit(1);
	}
	return 1;
}
SOCKET init_icmp_socket(void)
{
	SOCKET s;
	static SOCKADDR_IN local;

	if ((s = WSASocket(AF_INET, SOCK_RAW, IPPROTO_ICMP, 0, 0, 0)) < 0) {
		perror("tracy: icmp socket");
		exit(5);
	}
	memset(&local, 0 ,sizeof(local));
	local.sin_family = AF_INET;
	bind (s, (SOCKADDR *)&local, sizeof(local));
	return s;
}
int send_udp_packet(SOCKADDR_IN *dst, int ttl, unsigned short seq)
{
	static char outpacket[]="AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA";
	SOCKET s;

	s = init_udp_socket(seq);

	if (setsockopt(s, IPPROTO_IP, IP_TTL, (const char*)&ttl, sizeof(ttl)) < 0) {
		fprintf(stderr, "tracy: IP_TTL : error=%d\n", WSAGetLastError ());
		perror("tracy: IP_TTL");
		exit(6);
	}

	sendto(s, outpacket, sizeof(outpacket), 0, (const struct sockaddr *)dst, sizeof(SOCKADDR_IN));

	closesocket(s);
	return 1;
}

int wait_for_icmp(SOCKET s, unsigned short *seq_resp, int *icmp_type, 
				  SOCKADDR_IN *from, int *copied_ttl, int timeout)
{
	static addrsize = sizeof(SOCKADDR_IN), cc;
	static USHORT net_procid;
	char packet[512];			
	
	ICMPHeader *icmp;
	IPHeader *ip;
	IPHeader *orig_ip;
	UDPHeader *orig_udp;

	struct timeval wait;
	fd_set fds;
	int selected;

	FD_ZERO(&fds);	
	FD_SET(s, &fds);	
	wait.tv_sec = timeout; 
	wait.tv_usec = 0;

	while (1) 
	{
		
		selected = select(0, &fds, 0, 0, &wait);
		if (selected < 0) 
		{
			fprintf(stderr, "tracy: select: socket error %d\n", WSAGetLastError());
		}
		if (selected <= 0) return 0; 
		
	
		if((cc=recvfrom(s, (char *)packet, sizeof(packet), 0,
				(struct sockaddr *)from, &addrsize)) < 0) {
			fprintf(stderr, "tracy: recvfrom : error=%d, cc = %d\n", WSAGetLastError (), cc);
		}
		
		ip = (IPHeader *)packet;
		if (ip->proto != IPPROTO_ICMP) continue; 
		icmp = (ICMPHeader *)(&packet[sizeof(IPHeader)]);
		orig_ip = (IPHeader *)(&packet[sizeof(IPHeader)+sizeof(ICMPHeader)]);
		orig_udp = (UDPHeader *)(&packet[2*sizeof(IPHeader)+sizeof(ICMPHeader)]);
		if (icmp_type != 0) *icmp_type=icmp->type;
			switch (icmp->type)
			{ 
			case ICMP_TTL_EXPIRE: 
			case ICMP_DEST_UNREACH: 
				if (seq_resp != 0) *seq_resp = orig_udp->uh_sport;
				if (copied_ttl !=0) *copied_ttl = orig_ip->ttl;
				return cc;

			}
		
	}
}
SOCKET init_udp_socket(int seq)
{
	SOCKADDR_IN src;
	SOCKET s;

	if ((s = WSASocket(AF_INET, SOCK_RAW, IPPROTO_UDP, 0, 0, 0)) < 0) {
		perror("tracy: udp socket");
		exit(5);
	}
	memset(&src, 0 ,sizeof(src));
	src.sin_family = AF_INET;
	src.sin_port = seq;
	if (bind (s, (SOCKADDR *)&src, sizeof(src))) {
		fprintf(stderr, "tracy: bind error=%d\n", WSAGetLastError ());
		exit(6);
	}
	return s;
}
int udping(SOCKET sr, SOCKADDR_IN *dst, SOCKADDR_IN *from, unsigned int timeout, 
		   ULONG *rtt, int *packetwaste, unsigned short ttl)
{
	unsigned short seq, seq_sent = (unsigned short)(INITIAL_SEQ+ttl);
	int icmp_received, icmp_type;
	ULONG start_packet_timestamp, start_test_timestamp = GetTickCount();
	SOCKADDR_IN localfrom;
	if (!from) from = &localfrom; 

	while (GetTickCount()-start_test_timestamp < timeout*1000)
	{
		start_packet_timestamp = GetTickCount();
		send_udp_packet(dst, ttl, seq_sent);
		if (packetwaste) (*packetwaste)++;
		icmp_received = wait_for_icmp(sr, &seq, &icmp_type, from, 0, timeout);
		if (!icmp_received) return 0; 

		if(seq_sent == seq)	
		{
			if (rtt) *rtt = GetTickCount()-start_packet_timestamp;
			switch (icmp_type)
			{
			case ICMP_DEST_UNREACH:
				return 1; 
			case ICMP_TTL_EXPIRE:
				return -1; 
			default:
				continue; 
			}	
		}
		else 	
			continue;
	}
	return 0; 
}
/////////////////////////////////////////////////////////////////////////////// 
