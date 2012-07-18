#ifndef __IPHEADERS_H
#define __IPHEADERS_H

// ICMP packet types
#define ICMP_ECHO_REPLY 0
#define ICMP_DEST_UNREACH 3
#define ICMP_TTL_EXPIRE 11
#define ICMP_ECHO_REQUEST 8

// Minimum ICMP packet size, in bytes
#define ICMP_MIN 8

#ifdef _MSC_VER
#pragma pack(1)
#endif

// The IP header
typedef struct _IPHeader {
    BYTE h_len:4;           // Length of the header in dwords
    BYTE version:4;         // Version of IP
    BYTE tos;               // Type of service
    USHORT total_len;       // Length of the packet in dwords
    USHORT ident;           // unique identifier
    USHORT flags;           // Flags
    BYTE ttl;               // Time to live
    BYTE proto;             // Protocol number (TCP, UDP etc)
    USHORT checksum;        // IP checksum
    ULONG source_ip;
    ULONG dest_ip;
} IPHeader;

// ICMP header
typedef struct _ICMPHeader {
    BYTE type;          // ICMP packet type
    BYTE code;          // Type sub code
    USHORT checksum;
    USHORT id;
    USHORT seq;
} ICMPHeader;

//UDP header

typedef struct _UDPHeader { 
   USHORT uh_sport;				// src port
   USHORT uh_dport;				// dst port 
   USHORT uh_ulen;				// udp length 
   USHORT uh_sum;				// checksum
   USHORT uh_id;				// id of header
} UDPHeader;

//TCP header

typedef struct _TCPHeader { 
   USHORT sport;				// src port
   USHORT dport;				// dst port 
   UINT seq;
   UINT ack;
   BYTE unused:4;
   BYTE h_len:4;
   BYTE flags;
#  define TH_FIN        0x01
#  define TH_SYN        0x02
#  define TH_RST        0x04
#  define TH_PUSH       0x08
#  define TH_ACK        0x10
#  define TH_URG        0x20
   USHORT winsize;
   USHORT checksum;				// checksum
   USHORT urp;
} TCPHeader;

#ifdef _MSC_VER
#pragma pack()
#endif

#endif //__IPHEADERS_H