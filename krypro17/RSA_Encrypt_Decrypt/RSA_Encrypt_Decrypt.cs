/*
 
  RSA Encrypt / Decrypt / Key Generation 
  Cryptographic Protocolls ITS17
  Author: Thomas Schmiedecker
  23. 02. 2016
  
*/

using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Net;
using System.Collections;
using System.Numerics;
using Extended_Euclid;
using MillerRabinTest;


namespace Kryptprot_RSA
{
    class RSA_Encrypt_Decrypt
    {
        /* Variables */
		private static BigInteger big_px = 0;
		private static BigInteger big_qy = 0;
        private static BigInteger big_n = 0;
        private static BigInteger phiOfN = 0;
        private static BigInteger d = 0;
        private static int exponent = 65537; /* e = 2^16+1 (1 modulare Multiplikation + 16 Quadrierungen)*/
        private static Byte[] byte_px;
		private static Byte[] byte_qy;

        static void Main(string[] args)
        {
            /* Starting Secure RNG */
            System.Security.Cryptography.RNGCryptoServiceProvider secrand = new System.Security.Cryptography.RNGCryptoServiceProvider();
            byte_px = new byte[256];
            byte_qy = new byte[256];

            //do
            //{
                /* Get cryptographic bytearray */
                secrand.GetBytes(byte_px);
                /* Convert bytearray to BigInteger */
                big_px = new BigInteger(byte_px);
                big_px = BigInteger.Abs(big_px);
            //} while (!MRT.IsPrime(big_px, big_n));

            //do
            //{
                /* Get cryptographic bytearray */
                secrand.GetBytes(byte_qy);
                /* Convert bytearray to BigInteger */
                big_qy = new BigInteger(byte_qy);
                big_qy = BigInteger.Abs(big_qy);
            //} while (!MRT.IsPrime(big_qy, big_n));

            /* Calculate Modulus n */
            big_n = BigInteger.Multiply(big_px, big_qy);

            /* Calculate Phi(n)*/
            phiOfN = BigInteger.Multiply(BigInteger.Subtract(big_px, 1), BigInteger.Subtract(big_qy, 1));

            /* Proof of gcd(e, phi(n)) = 1 */
            BigInteger proofGcd = BigInteger.GreatestCommonDivisor(exponent, phiOfN);
            if (proofGcd != 1)
            {
                throw new System.ArgumentOutOfRangeException();
            }

            /* Calculate multiplicative invers d of e */
            ExtendedEuclidianAlgo multinvert_e = new ExtendedEuclidianAlgo();
            d = multinvert_e.ExtendedEuclid(exponent, phiOfN); 
            BigInteger proofinvers = (BigInteger.Multiply(exponent, d)) % phiOfN;
            if (proofinvers != 1)
            {
                throw new System.ArgumentOutOfRangeException();
            }

            /* Ecryption of bytestream "m^e mod n" */
            byte[] bMes = {1,2,3,4,5};
            BigInteger message = new BigInteger(bMes);
            BigInteger cipher = BigInteger.ModPow(message, exponent, big_n);
            Console.WriteLine("Encryption: {0}", cipher);
            byte[] decrypt_bmes = cipher.ToByteArray();
            
            /* Decryption of bytestream */

            Console.WriteLine ("End of code");
            Console.WriteLine ("Press Enter to end");
            Console.ReadLine();
		}

        /* Display bytearray */
        static void Display(byte[] array)
        {
            // Loop through array and display bytes 
            foreach (byte value in array)
            {
                Console.Write(value);
                Console.Write(' ');
            }
            Console.WriteLine("\n");
        }
    }
}
/*	x1. Zwei große Primzahlen p und q erzeugen.
    x2. n = p ∗ q berechnen.
    x3. Phi(n) = (p − 1)(q − 1) berechnen.
    x4. Eine zufällige Zahl e wählen, für die 1 < e < Phi(n) und gcd(e, Phi(n)) = 1 gilt. 
    x5.EineZahl d mit 1 < d < Phi(n)errechnen,sodass ed mod Phi(n) = 1 gilt.

    Öffentliche Teile von RSA: n, e 
    Private Teile von RSA: p, q, Phi(n), d

    E: c = m^e mod n
    D: m' = c^d mod n 

*/