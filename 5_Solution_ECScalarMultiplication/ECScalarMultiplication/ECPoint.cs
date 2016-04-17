/*
 
  EC Scalar Multiplication
  Cryptographic Protocolls ITS17
  Author: Thomas Schmiedecker
  16. 04. 2016
  
*/


using System;
using System.Numerics;

struct ECPoint
{
    private BigInteger x;
    private BigInteger y;

    public void SetECPoint(BigInteger x, BigInteger y)
    {
        this.x = x;
        this.y = y;
    }

    public void SetX(BigInteger x)
    {
        this.x = x;
    }

    public void SetY(BigInteger y)
    {
        this.y = y;
    }

    public BigInteger GetX()
    {
        return x;
    }

    public BigInteger GetY()
    {
        return y;
    }

    public void PrintPoint()
    {
        Console.WriteLine("\nCoordinates of EC Point");
        Console.WriteLine("X: {0}\nY: {1}\n", x, y);
    }
}

