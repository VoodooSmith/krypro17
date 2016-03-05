/*
 
  Client & Server Architecture 
  Cryptographic Protocolls ITS17
  Author: Thomas Schmiedecker
  23. 02. 2016
  
*/

using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Net;
using System.Collections;
using System.Numerics;

using System.Collections.Generic;
using System.Linq;
using System.Text;
//using NetworkCommsDotNet;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            //Request server IP and port number
            Console.WriteLine("Please enter the server IP and port in the format 192.168.0.1:10000 and press return:");
            string serverInfo = Console.ReadLine();

            //Parse the necessary information out of the provided string
            string serverIP = serverInfo.Split(':').First();
            int serverPort = int.Parse(serverInfo.Split(':').Last());

            //Keep a loopcounter
            int loopCounter = 1;
            while (true)
            {
                //Write some information to the console window
                string messageToSend = "This is message #" + loopCounter;
                Console.WriteLine("Sending message to server saying '" + messageToSend + "'");

                //Send the message in a single line
                //NetworkComms.SendObject("Message", serverIP, serverPort, messageToSend);

                //Check if user wants to go around the loop
                Console.WriteLine("\nPress q to quit or any other key to send another message.");
                if (Console.ReadKey(true).Key == ConsoleKey.Q) break;
                else loopCounter++;
            }

            //We have used comms so we make sure to call shutdown
            //networkComms.Shutdown();
        }
    }
}