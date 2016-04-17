/*
 
  EC Scalar Multiplication
  Cryptographic Protocolls ITS17
  Author: Thomas Schmiedecker
  16. 04. 2016
  
*/

using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Net;
using System.Collections;
using System.Globalization;
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
            string temp;
            bool result = false;

            P.SetX(BigInteger.Parse("6eca814ba59a930843dc814edd6c97da95518df3c6fdf16e9a10bb5b", NumberStyles.AllowHexSpecifier));
            P.SetY(BigInteger.Parse("ef4b497f0963bc8b6aec0ca0f259b89cd80994147e05dc6b64d7bf22", NumberStyles.AllowHexSpecifier));
            P.PrintPoint();

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
            Result.PrintPoint();

            Console.WriteLine("Press the any key to end...");
            Console.ReadKey();
        }       
    }
}
