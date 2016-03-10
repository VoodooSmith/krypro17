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
        static void Main(string[] args)
        {
            System.Security.Cryptography.HashAlgorithm secHash = null;

            /* Selection of hash algorithm */
            Console.WriteLine("Please select a hash algorithm\n");
            Console.WriteLine("Press 1 for MD5");
            Console.WriteLine("Press 2 for SHA256");
            Console.WriteLine("Press 3 for SHA384");
            Console.WriteLine("Press 4 for SHA512\n");
            Console.WriteLine("Confirm with ENTER");    
            Console.Write("Choice (1-4): ");
			int hashAlgo = Convert.ToInt32(Console.ReadLine());
            if (hashAlgo < 1 || hashAlgo > 4)
            {
                do
                {
                    Console.Write("Please enter valid choice (1 - 4): ");
                    hashAlgo = Convert.ToInt32(Console.ReadLine());
                } while (hashAlgo < 1 || hashAlgo > 4);
            }
			
            switch (hashAlgo)
            {
                case 1:
                    Console.WriteLine("Hash algorithm MD5 selected\n");
                    secHash = new System.Security.Cryptography.MD5CryptoServiceProvider();
                    break;
                case 2:
                    Console.WriteLine("Hash algorithm SHA256 selected\n");
                    secHash = new System.Security.Cryptography.SHA256CryptoServiceProvider();
                    break;
                case 3:
                    Console.WriteLine("Hash algorithm SHA384 selected\n");
                    secHash = new System.Security.Cryptography.SHA384CryptoServiceProvider();
                    break;
                case 4:
                    Console.WriteLine("Hash algorithm SHA512 selected\n");
                    secHash = new System.Security.Cryptography.SHA512CryptoServiceProvider();
                    break;
                default:
                    Console.WriteLine("Default case");
                    break;
            }

            /* User input */
            Console.Write("Please enter your text here: ");
            string input = Console.ReadLine();
            byte[] asciitext = System.Text.Encoding.ASCII.GetBytes(input);

            /* Hashing of user input */
            byte[] textHash = secHash.ComputeHash(asciitext);

            /* Hashed output as byte array*/
            Console.Write("Hashed string: ");
            Display(textHash);
           
            Console.WriteLine("End of code");
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
