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
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
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
        }
    }
}