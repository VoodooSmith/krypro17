﻿/*
 
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
using System.Threading;
using Chat = System.Net;



namespace Client_Server
{
    class Client_Server_Conn
    {
        System.Net.Sockets.TcpListener chatServer;
        public static Hashtable nickName;
        public static Hashtable nickNameByConnect;

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.WriteLine("I'm fucking alive!");
            Console.WriteLine("Press a fuckin' key...");
            Console.ReadKey();
        }
    }
}