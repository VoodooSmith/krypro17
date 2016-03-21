/*
 
  EC Scalar Multiplication
  Cryptographic Protocolls ITS17
  Author: Thomas Schmiedecker
  21. 03. 2016
  
*/

using System;
using System.Numerics;

struct ECPoint
{
    BigInteger x;
    BigInteger y;

    public void SetECPoint(BigInteger x, BigInteger y)
    {
        this.x = x;
        this.y = y;
    }

    public void printPoint ()
    {
        Console.WriteLine("\nCoordinates of EC Point");
        Console.WriteLine("X: {0}\nY: {1}", this.x, this.y);
    }
}

