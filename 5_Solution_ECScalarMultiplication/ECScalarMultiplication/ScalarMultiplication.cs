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
            EllipticCurve P224 = EllipticCurve.P224();
            ECPoint basePoint = new ECPoint();
            ECPoint P = new ECPoint();
            ECPoint Q = new ECPoint();
            ECPoint test = new ECPoint();

            P.SetECPoint(BigInteger.Parse("2"), BigInteger.Parse("6"));
            Q.SetECPoint(BigInteger.Parse("2"), BigInteger.Parse("6"));
            test = CurveMethods.PointAddition(P, Q, P224);
            test.printPoint();

            //basePoint.SetECPoint(p224.GetBaseX(), p224.GetBaseY());
            //basePoint.printPoint();
            //p224.CurveToString();

            Console.WriteLine("\nPress the any key to end...");
            Console.ReadKey();
        }
    }
}
