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
                d = (n - 1) / BigInteger.Pow(two, r);
            } while (d.IsEven);

            // repeat k times (the more cycles, the more accurate the result)
            int k = 18;
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

        static void Main(string[] args)
        { }
    }
}
