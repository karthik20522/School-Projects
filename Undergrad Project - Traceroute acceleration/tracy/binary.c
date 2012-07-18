/////////////////////////////////////////////////////////////////////////////// 
// 
//  FILE: binary.c 
// 
//      binary_search function 
// 
//  FUNCTIONS: 
// 
//      binary_search() - implements the binary search algorithm of traceroute
// 
//  AUTHORS: Dan Kenigsberg, Avital Tuviana, Arkady Krishtul
//
//	DATE:	July 2002 
/////////////////////////////////////////////////////////////////////////////// 

#include <winsock2.h>
#include <ws2tcpip.h>
#include <stdio.h>
#include "ipheaders.h"
#include "tracy.h"
#include "tracy_aux.h"

#define MAX_ITERATIONS	15
#define STARTING_TTL	15
#define NUMBER_OF_RETRIES 3 

int binary_search(SOCKADDR_IN *dst, int timeout, RouteInfo *rinfo)
{
	
	int ttl=0, high=32, low=1, iterations=MAX_ITERATIONS, retries = 0;
	ULONG start_test_timestamp = GetTickCount();
	SOCKET sr;
	
	sr = init_icmp_socket();

	rinfo->cnt_packet_waste=0;
	strcpy(rinfo->path, UNDEFINED_PATH);
	rinfo->resp_ttl=UNDEFINED_TTL;
	rinfo->hopdist = UNDEFINED_TTL;

	// counting iterations is just to make sure 
	// no one is fooling us into an infinite loop
	while (iterations--) 
	{
		if (!ttl) ttl = STARTING_TTL;	// this is the first time ttl is set.
										// since most sites are closer than 32, we start with it.
		else ttl = (high+low)/2;

		switch (udping(sr, dst, 0, timeout, &rinfo->rtt,
						&rinfo->cnt_packet_waste, (unsigned short)ttl))
		{
		case -1: // too short
			low = ttl+1;
			break;
		case 1: // exact or too long
			if (high == ttl || (ttl+low)/2 == ttl) // exact!
			{
				rinfo->time_waste_on_test = GetTickCount()-start_test_timestamp;
				rinfo->hopdist = ttl;
				closesocket(sr);
				return 1;
			}
			high = ttl; // too long...
			break;
		case 0: // timeout
			if (ttl < high && retries != NUMBER_OF_RETRIES) // if possible, let's give it another chance.
			{
				high--;
				retries++;
				break;
			}
			rinfo->time_waste_on_test = GetTickCount()-start_test_timestamp;
			closesocket(sr);
			return 0;
		}
	}
	rinfo->time_waste_on_test = GetTickCount()-start_test_timestamp;
	closesocket(sr);
	return 0;
}
/////////////////////////////////////////////////////////////////////////////// 

