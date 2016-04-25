#include <openssl/sha.h>
#include <stdio.h>
#include <stdlib.h>
#include <time.h>


int main()
{
	unsigned char *shawong;
	int i, j, count = 1000;
	/* 15414068 = 14,7MByte acc. http://www.umrechnung.org/masseinheiten-datenmenge-umrechnen-bit-byte-mb/datenmenge-filegroesse-speicherplatz.htm*/
	int arraysize = 15414068;
	/* unsigned char = 1 Byte */
	int randmax = 255;
	clock_t start, end;
	time_t t;

	printf("Start of Shawong measurement\n");
	start = clock();

	/* Initial memory allocation for 14,7MB */
	shawong = (unsigned char  *)malloc(arraysize);

	/* Start the speudo rng */
	srand((unsigned)time(&t));

	/* Fill array with arbitrary values */
	for (i = 0; i<arraysize; i++)
	{
		shawong[i] = (unsigned char)rand() % randmax;
	}

	/* Define length of unhashed array and create array for hash value*/
	size_t length = sizeof(shawong);
	unsigned char hashvalue[SHA_DIGEST_LENGTH];

	/* hash the array and write the output to "hashvalue" */
	for (j = 0; j<count; j++)
	{
		SHA1(shawong, length, hashvalue);
	}

	end = clock();

	/* Output  hash value */
	int hasharraysize = sizeof(hashvalue);
	printf("Hash value: ");
	for (int i = 0; i<hasharraysize; i++)
	{
		printf("%d", hashvalue[i]);
	}
	printf("\n");

	/* Free memory */
	free(shawong);

	printf("Runtime: %.2f Sekunden\n", (float)(end - start) / CLOCKS_PER_SEC);
	getchar();
	return 0;
}


