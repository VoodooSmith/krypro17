/*
 
  HMAC calculation of arbitrary text
  Cryptographic Protocolls ITS17
  Author: Thomas Schmiedecker
  16. 04. 2016
  
*/


using System;
using System.Security.Cryptography;

namespace HMAC
{
    public class HMACUtils
    {
        public static byte[] hmac_md5(string key, string msg)
        {
            /* Convert ASCII to Byte[] */
            byte[] keyBytes = System.Text.Encoding.ASCII.GetBytes(key);
            byte[] msgBytes = System.Text.Encoding.ASCII.GetBytes(msg);

            return hmac_md5(keyBytes, msgBytes);
        }

        public static byte[] hmac_md5(byte[] key, byte[] msg)
        {
            /* Create new MD5 Hash & send to hash*/
            HashAlgorithm md5 = new MD5CryptoServiceProvider();
            return hmac(md5, 64, key, msg);
        }

        public static byte[] hmac_sha1(string key, string msg)
        {
            /* Convert ASCII to Byte[] */
            byte[] keyBytes = System.Text.Encoding.ASCII.GetBytes(key);
            byte[] msgBytes = System.Text.Encoding.ASCII.GetBytes(msg);

            return hmac_sha1(keyBytes, msgBytes);
        }

        public static byte[] hmac_sha1(byte[] key, byte[] msg)
        {
            /* Create new SHA1 hash & send to hmac */
            HashAlgorithm sha1 = new SHA1CryptoServiceProvider();
            return hmac(sha1, 64, key, msg);
        }


       public static void Display(byte[] array)
        {
            Console.Write("\t0x");
            // Loop through array and display bytes 
            foreach (byte value in array)
            {
                Console.Write("{0:x2}", value);
            }
            Console.WriteLine();
        }

       
        /* Calculation of HMAC */
        private static byte[] hmac(HashAlgorithm hash, int blocksize, byte[] key, byte[] msg)
        {
            // shrink keys longer than blocksize 
            if (key.Length > blocksize)
            {
                key = hash.ComputeHash(key);
            }

            // fill keys shorter than blocksize 
            if (key.Length < blocksize)
            {
                byte[] tmp = new byte[blocksize];
                key.CopyTo(tmp, 0);
                for (int i = key.Length; i < blocksize; i++)
                {
                    tmp[i] = 0x00;
                }
                key = tmp;
            }
            
            // Create OPAD - outer padding
            byte[] opad = new byte[blocksize];
            for (int i = 0; i < opad.Length; i++)
            {
                /* ^ bitwise XOR*/
                opad[i] = (byte)(0x5c ^ key[i]);
            }

            // Create IPAD - inner padding
            byte[] ipad = new byte[blocksize];
            for (int i = 0; i < ipad.Length; i++)
            {
                /* Bitwise XOR */
                ipad[i] = (byte)(0x36 ^ key[i]);
            }

            // hash(ipad || msg) - Concatenation
            byte[] temp = new byte[ipad.Length + msg.Length];
            ipad.CopyTo(temp, 0);
            msg.CopyTo(temp, ipad.Length);
            temp = hash.ComputeHash(temp);

            // hash(opad || tmp) - Concatenation
            byte[] result = new byte[opad.Length + temp.Length];
            opad.CopyTo(result, 0);
            temp.CopyTo(result, opad.Length);
            result = hash.ComputeHash(result);

            return result;
        }
        /* HMAC _k(M) = H((k xor opad) || H((K xor ipad) || M)) */
    }

}
