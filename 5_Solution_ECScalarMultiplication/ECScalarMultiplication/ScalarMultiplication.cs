/*
 
  EC Scalar Multiplication
  Cryptographic Protocolls ITS17
  Author: Thomas Schmiedecker
  21. 03. 2016
  
*/

using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Net;
using System.Collections;
using System.Numerics;


namespace ECScalarMultiplication
{
    class ScalarMultiplication
    {
        static void Main(string[] args)
        {
            Console.WriteLine("###########################################");
            Console.WriteLine("#                                         #");
            Console.WriteLine("# Welcome to the EC scalar multiplication #");
            Console.WriteLine("#                                         #");
            Console.WriteLine("###########################################");

            EllipticCurve P224 = EllipticCurve.CreateP224();
            ECPoint P = new ECPoint();
            ECPoint Result = new ECPoint();
            BigInteger scalar = 0;
            string userInput;
            bool result = false;

            P.SetECPoint(BigInteger.Parse("2"), BigInteger.Parse("6"));

            Console.Write("\nPlease enter scalar for multiplication: ");
            userInput = Console.ReadLine();

            while (!result || scalar < 0)
            {
                result = BigInteger.TryParse(userInput, out scalar);
                if (!result || scalar < 0)
                {
                    Console.Write("Please enter valid number ( > 0 ): ");
                    userInput = Console.ReadLine();
                    result = BigInteger.TryParse(userInput, out scalar);
                }
            }

            Result = CurveMethods.ScalarMultiplication(P, scalar, P224);
            Result.printPoint();

            Console.WriteLine("Press the any key to end...");
            Console.ReadKey();
        }       
    }
}
