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


namespace RSA
{
    class RSA_Encrypt_Decrypt
    {
		private BigInteger p = 0;
		private BigInteger q = 0;
		private static Byte[] px;
		//private static Byte[] qy;

        static void Main(string[] args)
        {
			Console.WriteLine ("Ping!");
            System.Security.Cryptography.RNGCryptoServiceProvider secrand = new System.Security.Cryptography.RNGCryptoServiceProvider();
            px = new byte[256];
            secrand.GetBytes(px);
            BigInteger pxvalue = BitConverter.ToUInt64(px, 0);
            //pxvalue = System.Math.Abs(pxvalue);
            Console.WriteLine(pxvalue);
            //for (int i = 0; i < 10; i++)
            //{
            //    // fill array
            //    secrand.getbytes(px);

            //    // convert to int32
            //    int pxvalue = bitconverter.toint32(px, 0);
            //    pxvalue = system.math.abs(pxvalue);
            //    console.writeline(pxvalue);
            //}
            Display(px);
            Console.WriteLine ("End of code");
            Console.WriteLine("Press Enter to end");
            Console.ReadLine();
		}

        static void Display(byte[] array)
        {
            // Loop through and display bytes in array.
            foreach (byte value in array)
            {
                Console.Write(value);
                Console.Write(' ');
            }
            Console.WriteLine();
        }
    }
}
		/*	1. Zwei große Primzahlen p und q erzeugen.
			2. n = p ∗ q berechnen.
			3. Phi(n) = (p − 1)(q − 1) berechnen.
			4. Eine zufällige Zahl e wählen, für die 1 < e < Phi(n) und gcd(e, Phi(n)) = 1 gilt. 
			5.EineZahl d mit 1 < d < Phi(n)errechnen,sodass ed mod Phi(n) = 1 gilt.

			Öffentliche Teile von RSA: n, e 
			Private Teile von RSA: p, q, Phi(n), d

			E: c = m^e mod n
			D: m' = c^d mod n 

		*/
