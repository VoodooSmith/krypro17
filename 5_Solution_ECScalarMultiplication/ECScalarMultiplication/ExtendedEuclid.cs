/*
 
  EC Scalar Multiplication
  Cryptographic Protocolls ITS17
  Author: Thomas Schmiedecker
  16. 04. 2016
  
*/


using System.Numerics;

public class ExtendedEuclidianAlgo
{
    public static BigInteger ExtendedEuclid(BigInteger n, BigInteger m)
    {
        BigInteger temp_n = n, temp_m = m;                   // temp variables to store "n" and "m" for final output
        BigInteger temp_x = 0, temp_y = 0;                   // temp variables to stors "x" und "y" for the matrix transformation
        BigInteger q = 0, temp = 0, future_n = 0;            // "q" is the quotient, temp is a buffer
        BigInteger a = 1, b = 0, inv = 0;                    // "a" and "b" is the first row of column 1 (1 0) 
        BigInteger x = 0, y = 1;                             // "x" and "y" is the first row of column 2 (0 1

        if (m > n)
        {
            /* Before the calculation, "n" and "m" have to switch places */
            temp = n;
            n = m;
            m = temp;
        }
        while (m != 0)
        {
            /* Storage of "m" in "future_n" for future "n" */
            future_n = m;

            /* Calculation of quotient */
            q = n / m;
            /* Calculation of future "m" */
            m = n % m;

            temp_x = x;                       // x is getting stored for later use 
            x = x * (q * (-1));               // Matrix transformation - multiplication with negative "q"
            x = x + a;                        // Matrix transformation - Addition "x" and "a" for new "x"

            temp_y = y;                       // y is getting stored for later use 
            y = y * (q * (-1));               // Matrix transformation - Multiplication with negative "q"
            y = y + b;                        // Matrix transformation - Addition of "y" and "b" for new "y"

            a = temp_x;                       // Matrix transformation - "a" gets value of "x" before matrix transformation
            b = temp_y;                       // Matrix transformation - "b" gets value ov "y" before matrix transformation

            /* "n" for next round */
            n = future_n;
        }

        //Console.WriteLine ("The integer coordinates are {0} * {1} + {2} * {3} = {4}\n", a, temp_n, b, temp_m, n);

        if (b < 0)
        {
            inv = temp_m - a;
            return inv;
        }
        return b;
    }
}
