using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Net;
using System.Collections;
using System.Numerics;

namespace MillerRabinTest
{
    public static class MRT
    {
        public static BigInteger getPrime(int N)
        {
            BigInteger number = getRandomPositiveBigInteger(N, true);
            while (!isProbablePrime(number, N))
            {
                number = getRandomPositiveBigInteger(N, true);
            }

            return number;
        }

        private static bool isProbablePrime(BigInteger n, int N)
        {
            // Miller-Rabin primality test
            // https://en.wikipedia.org/wiki/Primality_test
            // https://en.wikipedia.org/wiki/Miller%E2%80%93Rabin_primality_test

            // Pseude code from wikipedia (18.02.2016)
            //  write n − 1 as 2r·d with d odd by factoring powers of 2 from n − 1
            //  WitnessLoop: repeat k times:
            //      pick a random integer a in the range [2, n − 2]
            //      x ← ad mod n
            //      if x = 1 or x = n − 1 then
            //          continue WitnessLoop
            //      repeat r − 1 times:
            //          x ← x2 mod n
            //          if x = 1 then
            //              return composite
            //          if x = n − 1 then
            //              continue WitnessLoop
            //      return composite
            //  return probably prime

            // write n - 1 as 2^r * d with d odd
            BigInteger two = new BigInteger(2);
            BigInteger d = new BigInteger(0);
            Int32 r = 0;
            do
            {
                r++;
                // diese VERDAMMTE klammer
                d = (n - 1) / BigInteger.Pow(two, r);
            } while (d.IsEven);

            // repeat k times (the more cycles, the more accurate the result)
            int k = 17;
            for (int i = 0; i < k; i++)
            {
                // pick a random integer a in the range [2, n - 2]
                var a = getRandomPositiveBigInteger(N, false) % n;
                while (a < 2 || a > n - 2)
                {
                    a = getRandomPositiveBigInteger(N, false) % n;
                }

                // compute x = a ^ d mod n
                BigInteger x = BigInteger.ModPow(a, d, n);
                if (x == 1 || x == n - 1)
                {
                    continue;
                }

                for (int j = 0; j < r; j++)
                {
                    x = BigInteger.ModPow(x, 2, n);
                    if (x == 1)
                    {
                        return false;
                    }
                    else if (x == n - 1)
                    {
                        break;
                    }
                }

                if (x == n - 1)
                {
                    continue;
                }

                return false;
            }

            return true;
        }

        public static BigInteger getRandomPositiveBigInteger(int N, bool forceMsb)
        {
            System.Security.Cryptography.RNGCryptoServiceProvider rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
            byte[] bytes = new byte[N / 8];

            rng.GetBytes(bytes);
            rng.Dispose();
            // set the msb and lsb
            if (forceMsb)
            {
                bytes[0] |= 0x80;
                bytes[N / 8 - 1] |= 1;
            }
            return BigInteger.Abs(new BigInteger(bytes));
        }
    }
}

            //    // constants for common referenced BigInteger values
            //    private static BigInteger two = 2;
            //    private static BigInteger three = 3;


            //    // Determines if the specified value is a prime number, deterministically for
            //    // values 64 bits and less, and probabilistically for values larger than 64 bits.

            //    // This method chooses the algorithm to use based on
            //    // the magnitude of the specified value. For smaller values, a
            //    // simple trial division algorithm is used. For larger values up to
            //    // and including 64 bit values, a deterministic version of the 
            //    // Miller-Rabin algorithm is used. For values more than 64 bits,
            //    // a probabilistic Miller-Rabin algorithm with a default of 64
            //    // witness iterations is used

            //    // "value" = the value to be tested for primality
            //    // "maxWitnessCount" = the maximum number of witness iterations (default is 64)
            //    // Returns True if the value is prime, otherwise, false
            //    public static bool IsPrime(this BigInteger value, int maxWitnessCount = 64)
            //    {
            //        if (value < two) return false;
            //        if (value <= ulong.MaxValue) return ((ulong)value).IsPrime();
            //        return value.IsPrimeMR(maxWitnessCount);
            //    }


            //    // Determines if the specified value is a prime number, using the
            //    // probabilistic Miller-Rabin algorithm.

            //    // "value" = the value to be tested for primality
            //    // "maxWitnessCount" = the maximum number of witness iterations
            //    // Returns True if the value is probably prime, otherwise, false
            //    public static bool IsPrimeMR(this BigInteger value, int maxWitnessCount = 64)
            //    {
            //        // take care of the simple cases of small primes and the
            //        // common composites having those primes as factors
            //        if (value <= BigInteger.One) return false;
            //        if ((value % two) == BigInteger.Zero) return value == two;
            //        if ((value % three) == BigInteger.Zero) return value == three;
            //        if ((value % 5) == BigInteger.Zero) return value == 5;
            //        if (((value % 7) == BigInteger.Zero) || ((value % 11) == BigInteger.Zero)
            //            || ((value % 13) == BigInteger.Zero) || ((value % 17) == BigInteger.Zero)
            //            || ((value % 19) == BigInteger.Zero) || ((value % 23) == BigInteger.Zero)
            //            || ((value % 29) == BigInteger.Zero) || ((value % 31) == BigInteger.Zero)
            //            || ((value % 37) == BigInteger.Zero) || ((value % 41) == BigInteger.Zero)
            //            || ((value % 43) == BigInteger.Zero))
            //        {
            //            return (value <= 43);
            //        }
            //        return InternalIsPrimeMR(value, maxWitnessCount, new ulong[0]);
            //    }


            //    // Determines if the specified odd, >= 3 value is a prime number, using the
            //    // Miller-Rabin algorithm.

            //    // "value" = the value to be tested for primality
            //    // "maxWitnessCount" = the maximum number of witness iterations
            //    // Returns True if the value is probably prime, otherwise, false
            //    internal static bool InternalIsPrimeMR(BigInteger value, int witnessCount, params ulong[] witnesses)
            //    {
            //        // compute n − 1 as (2^s)·d (where d is odd)
            //        BigInteger valLessOne = value - BigInteger.One;
            //        BigInteger d = valLessOne / two; // we know that value is odd and valLessOne is even, so unroll 1st iter of loop
            //        uint s = 1;
            //        while ((d % two) == BigInteger.Zero)
            //        {
            //            d /= two;
            //            s++;
            //        }

            //        // test value against each witness
            //        BigInteger rand = RandomBigInt();
            //        for (int i = 0; i < witnessCount; i++)
            //        {
            //            BigInteger a;
            //            if (i < witnesses.Length)
            //            {
            //                a = witnesses[i];
            //                if (a >= valLessOne)
            //                {
            //                    a %= value - three;
            //                    a += three;
            //                }
            //            }
            //            else {
            //                if (rand == null) rand = new RandomBigInteger(3, valLessOne);
            //                a = rand.Next();
            //            }
            //            BigInteger x = BigInteger.ModPow(a, d, value);

            //            if (x == BigInteger.One) continue;
            //            for (uint r = 1; (r < s) && (x != valLessOne); r++)
            //            {
            //                x = BigInteger.ModPow(x, two, value);
            //                if (x == BigInteger.One) return false;
            //            }
            //            if (x != valLessOne) return false;
            //        }
            //        // witnesses confirm value is prime
            //        return true;
            //    }


            //    public static BigInteger RandomBigInt()
            //    { 
            //        BigInteger r;
            //        byte[] bytear = new byte[256];

            //        System.Security.Cryptography.RNGCryptoServiceProvider secrand = new System.Security.Cryptography.RNGCryptoServiceProvider();
            //        /* Get cryptographic bytearray */
            //        secrand.GetBytes(bytear);
            //        /* Convert bytearray to BigInteger */
            //        BigInteger big = new BigInteger(bytear);
            //        big = BigInteger.Abs(big);
            //        return big;
            //    }


            //    static void Main(string[] args)
            //    { }
            //}
        
