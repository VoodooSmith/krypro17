/*
 
  HMAC calculation of arbitrary text
  Cryptographic Protocolls ITS17
  Author: Thomas Schmiedecker
  16. 04. 2016
  
*/

using System;

namespace HMAC
{
    class HashMessageAuthenticationCode
    { 
        static void Main()
        {
            Console.Write("Please enter your key: ");
            string key = Console.ReadLine();

            Console.Write("Please enter the message you want to secure: ");
            string msg = Console.ReadLine();

            byte[] hmac_md5 = HMACUtils.hmac_md5(key, msg);
            Console.Write("\nHMAC_MD5: ");
            HMACUtils.Display(hmac_md5);

            byte[] hmac_sha1 = HMACUtils.hmac_sha1(key, msg);
            Console.Write("HMAC_SHA1: ");
            HMACUtils.Display(hmac_sha1);

            Console.WriteLine("\nPress the any key to end...");
            Console.ReadKey();
        }
    }
}

