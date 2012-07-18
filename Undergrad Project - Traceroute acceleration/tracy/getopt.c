#include <stddef.h> 
#include <ctype.h> 
#include <string.h> 
#include "getopt.h" 
 
int GetOption (int argc, char** argv, char* pszValidOpts, char** ppszParam, int* nargs) 
{ 
    static int iArg = 1; 
    char chOpt; 
    char* psz = NULL; 
    char* pszParam = NULL; 
 
    if (iArg < argc) 
    { 
        psz = &(argv[iArg][0]); 
        if (*psz == '-' || *psz == '/') 
        { 
            chOpt = argv[iArg][1]; 
            if (isalnum(chOpt) || ispunct(chOpt)) 
            { 
                psz = strchr(pszValidOpts, chOpt); 
                if (psz != NULL) 
                { 
                    if (psz[1] == ':') 
                    { 
                        psz = &(argv[iArg][2]); 
                        if (*psz == '\0') 
                        { 
                            if (iArg+1 < argc) 
                            { 
                                psz = &(argv[iArg+1][0]); 
                                if (*psz == '-' || *psz == '/') 
                                {  } 
                                else 
                                { 
                                    iArg++; 
                                    pszParam = psz; 
                                } 
                            } 
                            else 
                            { 

                            } 
 
                        } 
                        else 
                        { 
                            pszParam = psz; 
                        } 
                    } 
                    else 
                    { 
                   } 
                } 
                else 
                { 
                    chOpt = -1; 
                    pszParam = &(argv[iArg][0]); 
                } 
            } 
            else 
            { 
                chOpt = -1; 
                pszParam = &(argv[iArg][0]); 
            } 
        } 
        else 
        { 
             chOpt = 1; 
            pszParam = &(argv[iArg][0]); 
        } 
    } 
    else 
    { 
         chOpt = 0; 
    } 
 

	*nargs = iArg;
    iArg++; 
    *ppszParam = pszParam; 
    return (chOpt); 
} 