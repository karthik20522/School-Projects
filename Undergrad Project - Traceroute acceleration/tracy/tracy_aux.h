#ifndef _TRACY_AUX_H
#define _TRACY_AUX_H

#include <winsock2.h>

char *name2sockaddr(char *hostname, SOCKADDR_IN *sockaddr);
int init_winsock(void);
SOCKET init_icmp_socket(void);
SOCKET init_udp_socket(int seq);
int send_udp_packet(SOCKADDR_IN *dst, int ttl, unsigned short seq);
int wait_for_icmp(SOCKET s, unsigned short *seq_resp, int *icmp_type, SOCKADDR_IN *from, int *copied_ttl, int timeout);
int udping(SOCKET sr, SOCKADDR_IN *dst, SOCKADDR_IN *from, unsigned int timeout, 
		   ULONG *rtt, int *packetwaste, unsigned short ttl);

#define INITIAL_SEQ 33434 // as used by unix traceroute.

#define DEST_PORT (32768+666)

#endif
