#ifndef _TRACY_H
#define _TRACY_H

#define MAX_PATH_STR_LEN	(512)
#define UNDEFINED_TTL		(-1)
#define UNDEFINED_PATH		("unknown")
#define UNDEFINED_DISTANCE	(-1)
#define UNDEFINED_RTT		(-1)

#define MAX_TTL	255

typedef struct _RouteInfo {
	int rtt;		
	int hopdist;
	int time_waste_on_test;
	int cnt_packet_waste;
	int resp_ttl;
	char path[MAX_PATH_STR_LEN];
} RouteInfo;


void print_route_info(char *method_name, RouteInfo *rinfo);


#endif
