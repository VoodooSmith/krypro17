/*
 
  RSA Encrypt / Decrypt / Key Generation 
  Cryptographic Protocolls ITS17
  Author: Thomas Schmiedecker
  07. 03. 2016
  
*/

using System;
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

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the RSA encryption and decryption solution");
            Console.WriteLine("Please wait while we are generating the prime numbers...\n");
            /* Generting prime numbers "p" and "q"*/
            big_px = MRT.getPrime(1024);
            big_qy = MRT.getPrime(1024);

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
            Console.WriteLine("Please enter your text here:");
            string text = Console.ReadLine();
            byte[] asciitext = System.Text.Encoding.ASCII.GetBytes(text);
            BigInteger message = new BigInteger(asciitext);
            Console.WriteLine("Message before encryption: {0}", text);
            BigInteger cipher = BigInteger.ModPow(message, exponent, big_n);
            Console.WriteLine("Encryption: {0}", cipher);


            /* Decryption of bytestream */
            BigInteger decrypt_mes = BigInteger.ModPow(cipher, d, big_n);
            byte[] decrypt_bmes = decrypt_mes.ToByteArray();
            string de_text = System.Text.Encoding.ASCII.GetString(decrypt_bmes);
            Console.WriteLine("Decryption: {0}", decrypt_mes);
            Console.WriteLine("Message after decryption: {0}", de_text);

            Console.WriteLine("End of code");
            Console.WriteLine("Press Enter to end");
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
/*	
    1. Zwei große Primzahlen p und q erzeugen.
    2. n = p ∗ q berechnen.
    3. Phi(n) = (p − 1)(q − 1) berechnen.
    4. Eine zufällige Zahl e wählen, für die 1 < e < Phi(n) und gcd(e, Phi(n)) = 1 gilt. 
    5.EineZahl d mit 1 < d < Phi(n)errechnen,sodass ed mod Phi(n) = 1 gilt.

    Öffentliche Teile von RSA: n, e 
    Private Teile von RSA: p, q, Phi(n), d

    E: c = m^e mod n
    D: m' = c^d mod n
*/
