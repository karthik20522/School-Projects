/////////////////////////////////////////////////////////////////////////////// 
// 
//S.Karthik , jOse , Benno
//
/////////////////////////////////////////////////////////////////////////////// 

#include <winsock2.h>
#include <ws2tcpip.h>
#include <stdio.h>
#include "tracy.h"
#include "tracy_aux.h"
#include "getopt.h" 


int classic(SOCKADDR_IN *dst, unsigned int timeout, RouteInfo *rinfo);
int copied_ttl(SOCKADDR_IN *dst, unsigned int timeout, RouteInfo *rinfo);
int parallel(SOCKADDR_IN *dst, unsigned int timeout, RouteInfo *rinfo);
int human_readable_output=1;		



void print_route_info(char *method_name, RouteInfo *rinfo)
{
	if (human_readable_output)
	{
		fprintf(stdout, "method used  : %s\n", method_name);
		fprintf(stdout, "hop distance : %3d\n", rinfo->hopdist);
		fprintf(stdout, "rtt          : %3d\n", rinfo->rtt);
		fprintf(stdout, "packet wasted: %3d\n", rinfo->cnt_packet_waste);
		fprintf(stdout, "time wasted  : %3d\n", rinfo->time_waste_on_test);
		fprintf(stdout, "path: %s\n\n", rinfo->path);
	}
	
}


static char remote_name[] = "ioiio.8k.com";
static int timeout = 5;	

void measure_host(char *hostname, char method)
{
	SOCKADDR_IN dst;
	RouteInfo rinfo;

	hostname = name2sockaddr(hostname, &dst);
	if (!hostname) return;

	if (human_readable_output) fprintf(stdout, "route info for %s (%s):\n", hostname, inet_ntoa(dst.sin_addr));
	else fprintf(stdout, "%s\t;", hostname);

	switch(method) {
		case 't':								//standart
			if (classic(&dst, timeout, &rinfo)) print_route_info("classic_trace", &rinfo); else fprintf(stdout, "classic_trace: timeout\n");
		break;
		case 'p':								//prallel
			if (parallel(&dst, timeout, &rinfo)) print_route_info("parallel", &rinfo); else fprintf(stdout, "parallel: timeout\n");
		break;
		case 'c':								//copied
			if (copied_ttl(&dst, timeout, &rinfo)) print_route_info("copied_ttl", &rinfo); else fprintf(stdout, "copied_ttl: timeout\n");
		break;
		default:							
			if (classic(&dst, timeout, &rinfo)) print_route_info("classic_trace", &rinfo); else fprintf(stdout, "classic_trace: timeout\n");
			if (parallel(&dst, timeout, &rinfo)) print_route_info("parallel", &rinfo); else fprintf(stdout, "parallel: timeout\n");
			if (copied_ttl(&dst, timeout, &rinfo)) print_route_info("copied_ttl", &rinfo); else fprintf(stdout, "copied_ttl: timeout\n");

	}

}
int main(int argc, char **argv)
{
	char *hostname;					
	char *parameter;				
	char option;					
	char *inputfile = NULL;			
	int nargs = 0;					
	char method = '\0';




	while ((option = GetOption(argc, argv, "hf:w:m:?", &parameter, &nargs)) != 0 && option != 1)
		switch(option) {
		case 'm':
			if(parameter != NULL) 
				method = parameter[0];	
			if(strchr("t,p,c", method) == NULL){
				fprintf(stderr,
				"Methods are:\n\t t - standart,\n\t p - parallel,\n\t c - copied ttl\n");
				exit(1);
			}
			break;
		default:
			fprintf(stderr,"Error in ur options!!!");
		}
	argc -= nargs;
	argv += nargs;

	
	if (argc==0 )
	{
		fprintf(stderr,	"Project By, S.Karthik, jOse   , Benno.\n");
		exit(1);
	}														 
	else if (*argv)
		hostname = *argv;

	init_winsock();
	
	if (inputfile == NULL)
		measure_host(hostname, method);
	WSACleanup();
	return 0;
}