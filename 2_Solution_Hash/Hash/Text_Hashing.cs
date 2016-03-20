/*
 
  HASH of arbitrary texts with arbitraty functions
  Cryptographic Protocolls ITS17
  Author: Thomas Schmiedecker
  07. 03. 2016
  
*/

using System;

namespace Hash
{
    class Text_Hashing
    {
        private static string hashIn, hashOut;
        private static int hashAlgo;
        private static bool result;

        static void Main(string[] args)
        {
            System.Security.Cryptography.HashAlgorithm secHash = null;

            /* Selection of hash algorithm */
            Console.WriteLine("Please select a hash algorithm\n");
            Console.WriteLine("Press 1 for MD5");
            Console.WriteLine("Press 2 for SHA1");
            Console.WriteLine("Press 3 for SHA256");
            Console.WriteLine("Press 4 for SHA384");
            Console.WriteLine("Press 5 for SHA512\n");
            Console.WriteLine("Confirm with ENTER");    
            Console.Write("Choice (1-5): ");

            hashIn = Console.ReadLine();
            while (hashAlgo < 1 || hashAlgo > 5)
            {
                result = false;
                while (!result)
                {
                    result = Int32.TryParse(hashIn, out hashAlgo);
                    if (!result || hashAlgo < 1 || hashAlgo > 5)
                    {
                        Console.Write("Please enter valid choice (1 - 5): ");
                        hashIn = Console.ReadLine();
                        result = Int32.TryParse(hashIn, out hashAlgo);
                    }
                }
            }
					
            switch (hashAlgo)
            {
                case 1:
                    Console.WriteLine("\nHash algorithm MD5 selected\n");
                    secHash = new System.Security.Cryptography.MD5CryptoServiceProvider();
                    break;
                case 2:
                    Console.WriteLine("\nHash algorithm SHA1 selected\n");
                    secHash = new System.Security.Cryptography.SHA1CryptoServiceProvider();
                    break;
                case 3:
                    Console.WriteLine("\nHash algorithm SHA256 selected\n");
                    secHash = new System.Security.Cryptography.SHA256CryptoServiceProvider();
                    break;
                case 4:
                    Console.WriteLine("\nHash algorithm SHA384 selected\n");
                    secHash = new System.Security.Cryptography.SHA384CryptoServiceProvider();
                    break;
                case 5:
                    Console.WriteLine("\nHash algorithm SHA512 selected\n");
                    secHash = new System.Security.Cryptography.SHA512CryptoServiceProvider();
                    break;
            }

            /* User input */
            Console.Write("Please enter your text here: ");
            string input = Console.ReadLine();
            byte[] asciitext = System.Text.Encoding.ASCII.GetBytes(input);

            /* Hashing of user input */
            byte[] textHash = secHash.ComputeHash(asciitext);
            string hashOut = BitConverter.ToString(textHash);

            /* Hashed output as byte array*/
            Console.Write("\nHashed string in hexadecimal format:\n{0}\n", hashOut);
           
            Console.WriteLine("\nEnd of code");
            Console.WriteLine("Press any key to end");
            Console.ReadKey();
        }

        /* Display bytearray */
        static void Display(byte[] array)
        {
            /* Loop through array and display bytes */
            foreach (byte value in array)
            {
                Console.Write(value);
                Console.Write(' ');
            }
            Console.WriteLine("\n");
        }
    }
}
