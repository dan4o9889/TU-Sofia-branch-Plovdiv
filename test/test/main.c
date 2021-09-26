//
//  main.c
//  test
//
//  Created by Daniel Ivanov on 29.05.21.
//  Copyright © 2021 Daniel Ivanov. All rights reserved.
//

#include <string.h>
#include <stdio.h>

int main(int argc, const char * argv[]) {
    int n;
    char *str1 = "iordan dani salih mitko";
    char *str2 = "salih iordan";
    n = func1(str1, str2);
    printf("%d",n);
   
}
int func1(char *str1, char *str2)
{
    int broi = 0;
    char *chast1 = NULL;
    char *chast2 = NULL;
    while (chast1!=NULL)
    {
        chast1 = strtok(str1, " ");
        while (chast2!=NULL)
        {
            chast2 = strtok(str2," ");
            if (strcmp(chast1, chast2)) broi++;
        }
        
    }
    return broi;
}
