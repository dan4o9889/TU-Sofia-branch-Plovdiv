#include <stdio.h>
#include <omp.h>
#include <math.h>
#include <stdlib.h>

#define masiv_MAX_broi_elementi 500000
int masiv[masiv_MAX_broi_elementi];

void razmqna(int* a, int* b) {
    int t = *a;
    *a = *b;
    *b = t;
}

int razdelqne(int masiv[], int nachalo, int krai) {
    int glaven = masiv[krai];
    int i = (nachalo - 1);
    
    for (int j = nachalo; j <= krai- 1; j++) {
        if (masiv[j] <= glaven) {
            i++;
            razmqna(&masiv[i], &masiv[j]);
        }
    }
    
    razmqna(&masiv[i + 1], &masiv[krai]);
    
    return (i + 1);
}

void quickSort(int masiv[], int nachalo, int krai) {
    if (nachalo < krai) {
        int pi = razdelqne(masiv, nachalo, krai);

        #pragma omp task firstprivate(masiv, nachalo, pi)
        {
            quickSort(masiv, nachalo, pi - 1);
        }

        //#pragma omp task firstprivate(masiv, krai, pi)
        {
            quickSort(masiv, pi + 1, krai);
        }
    }
}

void izvejdane_masiv(int masiv[], int broi_elementi) {
    int i;
    for (i=0; i < broi_elementi; i++) {
        printf("%d ", masiv[i]);
    }
    printf("\n");
}

int main() {
    double nachalno_vreme, vreme_na_izpulnenie;
    
    for( int i = 0; i < masiv_MAX_broi_elementi-1; i++ ) {
       masiv[i] = rand() % 50 + 1;
       printf("%d\n", masiv[i]);
    }
    
    int n = sizeof(masiv)/sizeof(masiv[0]);


    //int pi = razdelqne(masiv, 0, n-1);

    /*#pragma omp parallel sections
    {
        #pragma omp section {
            quickSort(masiv,0, pi - 1);
        }
        #pragma omp section {
            quickSort(masiv, pi + 1, n-1);
        }
    }*/

    omp_set_num_threads(8);
    nachalno_vreme = omp_get_wtime();

    #pragma omp parallel
    {
        int id          = omp_get_thread_num();
        int broi_nishki = omp_get_num_threads();
        //printf("Nishkata e %d\n",id);
        #pragma omp single nowait
        quickSort(masiv, 0, n-1);
    }

    vreme_na_izpulnenie = omp_get_wtime() - nachalno_vreme;
    printf("\n Vremeto na izpulnenie beshe %lf sekundi \n", vreme_na_izpulnenie);
    //printf("Sortiraniqt masiv: \n");
    //izvejdane_masiv(masiv, n);
    
    return 0;
}
