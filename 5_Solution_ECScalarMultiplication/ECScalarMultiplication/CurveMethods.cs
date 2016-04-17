/*
 
  EC Scalar Multiplication
  Cryptographic Protocolls ITS17
  Author: Thomas Schmiedecker
  16. 04. 2016
  
*/


using System;
using System.Text;
using System.Numerics;


class CurveMethods
{
    public static ECPoint PointAddition(ECPoint P, ECPoint Q, EllipticCurve P224)
    {
        /* If P == Q, than it is an Point Duplication */
        if((P.GetX() == Q.GetX()) && (P.GetY() == Q.GetY()))
        {
            return PointDuplication(P, P224);
        }
        //Console.WriteLine("PointAddition");
        ECPoint R = new ECPoint();

        /* Calculation of Rx */
        BigInteger sx = BigInteger.ModPow(
            BigInteger.Multiply(
                BigInteger.Subtract(Q.GetY(), P.GetY()), ExtendedEuclidianAlgo.ExtendedEuclid(BigInteger.Subtract(Q.GetX(), P.GetX()), P224.GetModulus())), 2, P224.GetModulus());
        R.SetX(BigInteger.Subtract(sx, BigInteger.Subtract(P.GetX(), Q.GetX())));
        R.SetX(R.GetX() % P224.GetModulus());

        /* Calculation of Ry */
        BigInteger sy1 = BigInteger.Multiply(
            BigInteger.Subtract(Q.GetY(), P.GetY()), ExtendedEuclidianAlgo.ExtendedEuclid(BigInteger.Subtract(Q.GetX(), P.GetX()), P224.GetModulus()));
        BigInteger sy2 = BigInteger.Subtract(BigInteger.Subtract(P.GetX(), R.GetX()), P.GetY());
        R.SetY(BigInteger.Multiply(sy1, sy2));
        R.SetY(R.GetY() % P224.GetModulus());

        /* Reflect point R' to get R */
        R = Mirror(R);

        return R;
    }

    public static ECPoint PointDuplication(ECPoint P, EllipticCurve P224)
    {
        ECPoint R = new ECPoint();
        //Console.WriteLine("PointDuplication\n");

        /* Calculation of Rx */
        BigInteger x1 = BigInteger.Multiply(3, BigInteger.ModPow(P.GetX(), 2, P224.GetModulus()));
        BigInteger x2 = BigInteger.Multiply(BigInteger.Add(x1, P224.GetA()), ExtendedEuclidianAlgo.ExtendedEuclid(BigInteger.Multiply(2, P.GetY()), P224.GetModulus()));
        R.SetX(BigInteger.Subtract(BigInteger.ModPow(x2, 2, P224.GetModulus()), BigInteger.Multiply(2, P.GetX())));
        R.SetX(R.GetX() % P224.GetModulus());

        /* Calculation of Ry */
        BigInteger y1 = BigInteger.Multiply(3, BigInteger.ModPow(P.GetX(), 2, P224.GetModulus()));
        BigInteger y2 = BigInteger.Multiply(BigInteger.Add(y1, P224.GetA()), ExtendedEuclidianAlgo.ExtendedEuclid(BigInteger.Multiply(2, P.GetY()), P224.GetModulus()));
        R.SetY(BigInteger.Subtract(BigInteger.Multiply(y2, BigInteger.Subtract(P.GetX(), R.GetX())), P.GetY()));
        R.SetY(R.GetY() % P224.GetModulus());
        
        /* Reflect point R' to get R */
        R = Mirror(R);

        return R;
    }

    /* Scalar Multiplication */ 
    public static ECPoint ScalarMultiplication(ECPoint P, BigInteger scalarBigInt, EllipticCurve P224)
    {
        string scalar = ToBinaryString(scalarBigInt);
        int counter = 0;
        ECPoint Tmp = new ECPoint();
        ECPoint Result = new ECPoint();

        Console.WriteLine("\nBinary format of scalar: {0}", scalar);

        Result.SetECPoint(0, 0);
        Tmp = P;

        for (int i = 0; i < scalar.Length; i++)
        {
            if (Char.IsDigit(scalar[i]))
                if (Char.GetNumericValue(scalar[i]) == 1)
                    counter++;
        }
        //Console.WriteLine("Binary Check\n");

        for (int n = scalar.Length-1; n >= 0 ; n--)
        {
            if (Char.IsDigit(scalar[n]))
            {
                if (Char.GetNumericValue(scalar[n]) == 1)
                {
                    //Console.WriteLine("Binary = 1");
                    Result = PointAddition(Result, Tmp, P224);
                    Tmp = PointDuplication(Tmp, P224);
                }
                else
                {
                    //Console.WriteLine("Binary = 0");
                    Result = PointDuplication(Tmp, P224);
                }
            }
        }
        return Result;
    }

    /* Reflect R' to get R */
    public static ECPoint Mirror(ECPoint R)
    {
        ECPoint Reflect = R;

        if (Reflect.GetY() < 0 || Reflect.GetY() > 0)
        {
            Reflect.SetY(BigInteger.Multiply(R.GetY(), -1));
        } if(Reflect.GetY() == BigInteger.Zero)
        {
            return R;
        }
        return Reflect;
    }


    /* Dispaly bytes in big endian order in own row */
    public static void DisplayBytes(byte[] array)
    {
        foreach (byte value in array)
        {
            Console.Write(value);
            Console.Write(' ');
        }
        Console.WriteLine("\n");
    }


    /* Dispaly bytes in numbered rows */
    public static void DisplayNumberedBytes(byte[] binInt)
    {
        int i = 0;
        foreach (byte bin in binInt)
        {
            Console.WriteLine("{0}.Byte = {1}", i++, bin);
        }
        Console.WriteLine();
    }


    /* Convert scalar to binary string */
     public static string ToBinaryString(BigInteger bigint)
    {
        var bytes = bigint.ToByteArray();
        var bytelen = bytes.Length - 1;

        // Create a StringBuilder having appropriate capacity.
        var base2 = new StringBuilder(bytes.Length * 8);

        // Convert first byte to binary.
        var binary = Convert.ToString(bytes[bytelen], 2);

        // Append binary string to StringBuilder.
        base2.Append(binary);

        // Convert remaining bytes adding leading zeros.
        for (bytelen--; bytelen >= 0; bytelen--)
        {
            base2.Append(Convert.ToString(bytes[bytelen], 2).PadLeft(8, '0'));
        }
        return base2.ToString();
    }
}

