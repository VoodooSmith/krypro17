using System;
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
        ECPoint R = new ECPoint();

        /* Calculation of Rx */
        BigInteger sx = BigInteger.ModPow(
            BigInteger.Multiply(
                BigInteger.Subtract(Q.GetY(), P.GetY()), ExtendedEuclidianAlgo.ExtendedEuclid(BigInteger.Subtract(Q.GetX(), P.GetX()), P224.GetModulus())), 2, P224.GetModulus());
        R.SetX(BigInteger.Subtract(sx, BigInteger.Subtract(P.GetX(), Q.GetX())));

        /* Calculation of Ry */
        BigInteger sy1 = BigInteger.Multiply(
            BigInteger.Subtract(Q.GetY(), P.GetY()), ExtendedEuclidianAlgo.ExtendedEuclid(BigInteger.Subtract(Q.GetX(), P.GetX()), P224.GetModulus()));
        BigInteger sy2 = BigInteger.Subtract(BigInteger.Subtract(P.GetX(), R.GetX()), P.GetY());
        R.SetY(BigInteger.Multiply(sy1, sy2));

        return R;
    }

    public static ECPoint PointDuplication(ECPoint P, EllipticCurve P224)
    {
        ECPoint R = new ECPoint();
        Console.WriteLine("Bin da, wer noch?!");

        /* Calculation of Rx */
        BigInteger x1 = BigInteger.Multiply(3, BigInteger.ModPow(P.GetX(), 2, P224.GetModulus()));
        BigInteger x2 = BigInteger.Multiply(BigInteger.Add(x1, P224.GetA()), ExtendedEuclidianAlgo.ExtendedEuclid(BigInteger.Multiply(2, P.GetY()), P224.GetModulus()));
        R.SetX(BigInteger.Subtract(BigInteger.ModPow(x2, 2, P224.GetModulus()), BigInteger.Multiply(2, P.GetX())));
        
        /* Calculation of Ry */
        BigInteger y1 = BigInteger.Multiply(3, BigInteger.ModPow(P.GetX(), 2, P224.GetModulus()));
        BigInteger y2 = BigInteger.Multiply(BigInteger.Add(y1, P224.GetA()), ExtendedEuclidianAlgo.ExtendedEuclid(BigInteger.Multiply(2, P.GetY()), P224.GetModulus()));
        R.SetY(BigInteger.Subtract(BigInteger.Multiply(y2, BigInteger.Subtract(P.GetX(), R.GetX())), P.GetY()));

        return R;
    }
}

