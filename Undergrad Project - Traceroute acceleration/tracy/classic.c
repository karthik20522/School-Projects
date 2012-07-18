#include <winsock2.h>
#include <ws2tcpip.h>
#include <stdio.h>
#include "ipheaders.h"
#include "tracy.h"
#include "tracy_aux.h"

int classic(SOCKADDR_IN *dst, int timeout, RouteInfo *rinfo)
{
	SOCKET sr;
	SOCKADDR_IN from;
	unsigned char ttl;
	ULONG start_test_timestamp = GetTickCount();

	sr = init_icmp_socket();
	
	rinfo->path[0]=0; // initial path is ""
	rinfo->resp_ttl = UNDEFINED_TTL;
	rinfo->cnt_packet_waste = 0;

	for (ttl=1; ttl<=MAX_TTL; ttl++)
	{
		switch (udping(sr, dst, &from, timeout, &rinfo->rtt, &rinfo->cnt_packet_waste, ttl))
		{
		case -1: 
			strncat(rinfo->path, inet_ntoa(from.sin_addr), sizeof(rinfo->path));
			strncat(rinfo->path, ", ", sizeof(rinfo->path));
			break;
		case 1:
			strncat(rinfo->path, inet_ntoa(from.sin_addr), sizeof(rinfo->path));
			rinfo->time_waste_on_test = GetTickCount() - start_test_timestamp;
			rinfo->hopdist = ttl;
			printf("%d",ttl);
			closesocket(sr);
			return 1;
		case 0:
			if (ttl > 5) {				//if ttl > 5 and we got timeout assume that it is hopeless
				rinfo->time_waste_on_test = GetTickCount() - start_test_timestamp;
				rinfo->rtt = UNDEFINED_RTT;
				rinfo->hopdist = UNDEFINED_DISTANCE;
				closesocket(sr);
				return 0;  
			}							
			else						//if ttl < 5 we still have hope to reach the destination
				break;
		default: ;
			//fprintf(stderr, "classic: timeout");
		}
	}
	rinfo->time_waste_on_test = GetTickCount() - start_test_timestamp;
	rinfo->rtt = UNDEFINED_RTT;
	rinfo->hopdist = UNDEFINED_DISTANCE;
	closesocket(sr);
	return 0;
}