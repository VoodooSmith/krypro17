/*
 
  EC Scalar Multiplication
  Cryptographic Protocolls ITS17
  Author: Thomas Schmiedecker
  21. 03. 2016
  
*/

using System;
using System.Globalization;
using System.Numerics;

 public class EllipticCurve
{
    private string curveName;       //name of the EC
    private BigInteger p;           //Prime modulus p
    private BigInteger n;           //Order n
    private BigInteger a;           //Shapepoint a
    private BigInteger b;           //Shapepoint b
    private BigInteger BPx;         //Basepoint x-coordinate
    private BigInteger BPy;         //Basepoint y-coordinate

   public static EllipticCurve P224()
    {
        EllipticCurve curve224 = new EllipticCurve();
                
        curve224.curveName = "Nist P-224";
        curve224.p = BigInteger.Parse("26959946667150639794667015087019630673557916260026308143510066298881");
        curve224.n = BigInteger.Parse("26959946667150639794667015087019625940457807714424391721682722368061");
        curve224.a = BigInteger.Parse("-3");
        curve224.b = BigInteger.Parse("b4050a850c04b3abf54132565044b0b7d7bfd8ba270b39432355ffb4", NumberStyles.HexNumber);
        curve224.BPx = BigInteger.Parse("b70e0cbd6bb4bf7f321390b94a03c1d356c21122343280d6115c1d21", NumberStyles.HexNumber);
        curve224.BPy = BigInteger.Parse("bd376388b5f723fb4c22dfe6cd4375a05a07476444d5819985007e34", NumberStyles.HexNumber);
        
        return curve224;
    }

    public BigInteger GetModulus()
    {
        return this.p;
    }

    public BigInteger GetOrder()
    {
        return this.n;
    }

    public BigInteger GetA()
    {
        return this.a;
    }

    public BigInteger GetBaseX()
    {
        return this.BPx;
    }

    public BigInteger GetBaseY()
    {
        return this.BPy;
    }

    public void CurveToString()
    {
        Console.WriteLine("\nCurve name:\t\t{0}\nPrime modulus p:\t{1}\nOrder n:\t\t{2}" +
            "\nCoefficient a:\t\t{3}\nCoefficient b:\t\t{4}\nBasepoint(x):\t\t{5}\nBasepoint(y):\t\t{6}",
            curveName, p, n, a, b, BPx, BPy);
    }
}

/*
Curve P-224 
E: y^2 = x^3 - 3x + b (mod p) 
p =  26959946667150639794667015087019630673557916260026308143510066298881 (Prime modulus)
n =  26959946667150639794667015087019625940457807714424391721682722368061 (order)
SEED = bd713447 99d5c7fc dc45b59f a3b9ab8f 6a948bc5 (160-Bit Input Seed)
c = 5b056c7e 11dd68f4 0469ee7f 3c7a7d74 f7d12111 6506d031 218291fb (Output of SHA1 algo)
a = -3;
b = b4050a85 0c04b3ab f5413256 5044b0b7 d7bfd8ba 270b3943 2355ffb4 
Gx = b70e0cbd 6bb4bf7f 321390b9 4a03c1d3 56c21122 343280d6 115c1d21 (Base Point x)
Gy = bd376388 b5f723fb 4c22dfe6 cd4375a0 5a074764 44d58199 85007e34 (Base Point y)
*/


