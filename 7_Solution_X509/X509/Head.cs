/*
 
  X.509 - Certificate reading and verifing
  Cryptographic Protocolls ITS17
  Author: Thomas Schmiedecker
  16. 04. 2016
  
*/


using System;
using System.Security.Cryptography.X509Certificates;

namespace X509
{
    class Head
    {
        static void Main(string[] args)
        {
            /* Certificate created with Visual Studio */
            //string xFile = "C:\\Users\\Thoma_000\\Documents\\Visual Studio 2015\\Projects\\7_Solution_X509\\X509\\VerisignTest.cer";
            //string xFile = "C:\\Users\\Tom\\Source\\Repos\\krypro17\\7_Solution_X509\\X509\\GeoTrust.cer";
            string xFile = "C:\\Users\\Tom\\Source\\Repos\\krypro17\\7_Solution_X509\\X509\\DigiCert.cer";

            try
            {

                Console.WriteLine("Entered path of certificate:\n{0}", xFile);

                /* Create certificate to proof with imported information*/
                X509Certificate2 xCert = new X509Certificate2();
                xCert.Import(xFile);
                
                Console.WriteLine("{0}", xCert.ToString(true));
               
                /* Certificate validation */
                Console.WriteLine("Certificate validation...");
                Console.WriteLine("{0}!", xCert.Verify() ? "Certificate is valid" : "Certificate is invalid");
            }
            catch (Exception)
            {
                Console.Error.WriteLine("Loading error! Please contact your system administrator.");
            }
            Console.WriteLine("Press the any key to end...");
            Console.ReadKey();
        }
    }
}
