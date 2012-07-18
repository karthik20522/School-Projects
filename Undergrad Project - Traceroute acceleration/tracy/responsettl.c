/////////////////////////////////////////////////////////////////////////////////////// 
// 
//  FILE: responsettl.c
// 
//       Response TTL algorithm implementation using WinPCap SDK
// 
//  FUNCTIONS: 
// 
//      init_http_socket()		-	create a tcp socket and connect it to port 80 
//									at destination
//		init_http_sniffer()		-	create a WinPCap filter that captures http packets 
//									coming from destination
//		wait_for_http_packet()	-	waits timeout seconds for a packet to be caught 
//									by the WinPCap filter
//		response_ttl()			-	evaluets the hop distance to the destination host
//									by sending HTTP request with some ttl value and 
//									getting the respons packet
// 
//  AUTHORS: Dan Kenigsberg, Avital Tuviana, Arkady Krishtul
//
//	DATE:	July 2002 
/////////////////////////////////////////////////////////////////////////////////////// 

#include <winsock2.h>
#include <ws2tcpip.h>
#include <stdio.h>
#include <pcap.h>
#include "ipheaders.h"
#include "tracy.h"
#include "tracy_aux.h"

#define HTTP_PORT 80
#define ETHERNET_SIZE 14
#define SINGLE_TIMEOUT_MS 20
#define RESPONSE_TTL_PACKET_WASTE 3

/* create a tcp socket and connect it to port 80 at dst */
SOCKET init_http_socket(SOCKADDR_IN *dst)
{
	SOCKADDR_IN src;
	SOCKET s;

	if ((s = WSASocket(AF_INET, SOCK_STREAM, IPPROTO_TCP, 0, 0, 0)) < 0) {
		perror("tracy: http socket");
		exit(8);
	}
	memset(&src, 0 ,sizeof(src));
	src.sin_family = AF_INET;
	if (bind (s, (SOCKADDR *)&src, sizeof(src))) {
		fprintf(stderr, "tracy: bind error=%d\n", WSAGetLastError ());
		exit(6);
	}
	dst->sin_port = htons(HTTP_PORT);
	if (connect(s, (const struct sockaddr *)dst, sizeof(SOCKADDR_IN))) {
		fprintf(stderr, "tracy: connect: socket error %d\n", WSAGetLastError());
//		exit(8);
		return 0;
	}
	return s;
}
/////////////////////////////////////////////////////////////////////////////// 

/* create a WinPCap filter that captures http packets 
   coming from dst. */
pcap_t *init_http_sniffer(SOCKADDR_IN *dst)
{
	char *dev;
	pcap_t *fp;
	char error[PCAP_ERRBUF_SIZE];
	bpf_u_int32 mask, net;
	struct bpf_program filter;
	char filter_str[200];

	dev = pcap_lookupdev(error); /* find a default net device */
//	printf("Device %s\n", dev);
	/* create a filter for that dev */
	if ( (fp= pcap_open_live(dev, 100, 1, SINGLE_TIMEOUT_MS, error) ) == NULL)
	{
		fprintf(stderr,"\nError opening adapter\n");
		return 0;
	}
	/* find the net id of the default network device */
	pcap_lookupnet(dev, &net, &mask, error);
	/* compile the filter string that captures every http packet that is coming from dst */
	sprintf(filter_str, "port %d and src host %s", HTTP_PORT, inet_ntoa(dst->sin_addr));
	if (pcap_compile(fp, &filter, filter_str, 0, net) == -1)
	{
		fprintf(stderr, pcap_geterr(fp));
		return 0;
	}
	/* start filtering packets */
	pcap_setfilter(fp, &filter);
	return fp;
}
/////////////////////////////////////////////////////////////////////////////// 

/* wait timeout seconds for a packet to be caught by the filter fp */
int wait_for_http_packet(pcap_t *fp, int timeout, int *ttl)
{
	struct pcap_pkthdr header;
	BYTE *packet;
	IPHeader *ip;

	// God knows why this has to be an ugly almost-busy wait loop
	// instead of a simple timeout param to pcap_next
	timeout = timeout*1000/SINGLE_TIMEOUT_MS;
	while (timeout-- > 0 && 
		   (packet = (BYTE *)pcap_next(fp, &header))==0);

	if (packet == 0) //timeout
	{
		return 0;
	}
	ip = (IPHeader *)&packet[ETHERNET_SIZE];
    if (ttl) *ttl=ip->ttl;
	return 1;
}
/////////////////////////////////////////////////////////////////////////////// 

//sends the connection request to the destination host and waits 
//for the ACK packet to arrive and exstracts the hop distance form ttl in it
int response_ttl(SOCKADDR_IN *dst, int timeout, RouteInfo *rinfo)
{
	pcap_t *fp;
	ULONG guess_init_ttl, ttl, start_timestamp;
	SOCKET s;

	start_timestamp = GetTickCount();
	fp = init_http_sniffer(dst);
	if (!fp) exit(9);

	s = init_http_socket(dst);

	/* wait for a packet coming from dst over the socket s.
	   notice that the first (and only packet) to arrive is 
	   simply the ACK for the connection request. */
	if (!wait_for_http_packet(fp, timeout, &ttl))
	{
		fprintf(stderr, "wait_for_http_packet: timeout\n");
		return 0;
	}
	pcap_close(fp);
	closesocket(s);

	//printf("response ttl is %d\n", ttl);
	if (ttl>128) guess_init_ttl = 256;
	else if (ttl>64) guess_init_ttl = 128;
	else if (ttl>32) guess_init_ttl = 64;
	else guess_init_ttl = 32;
		
	rinfo->resp_ttl = ttl;
	rinfo->hopdist = guess_init_ttl - ttl;
	rinfo->rtt = GetTickCount()-start_timestamp;
	rinfo->cnt_packet_waste = RESPONSE_TTL_PACKET_WASTE;
	rinfo->time_waste_on_test = rinfo->rtt;
	strcpy(rinfo->path, UNDEFINED_PATH);

	return 1;
}

