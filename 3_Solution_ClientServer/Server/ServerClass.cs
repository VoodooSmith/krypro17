using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Server
{
    public class SynchronousSocketServer
    {

        // Incoming data from the client.
        public static string data = null;

        public static void StartListening()
        {
            // Data buffer for incoming data.
            byte[] bytes = new Byte[1024];

            // Establish the local endpoint for the socket.
            // Dns.GetHostName returns the name of the 
            // host running the application. 
            IPHostEntry ipHostInfo = Dns.GetHostEntry("localhost");
            IPAddress ipAddress = ipHostInfo.AddressList[1];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 30000);

            // Create a TCP/IP socket.
            Socket listener = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);

            // Bind the socket to the local endpoint and 
            // listen for incoming connections.
            try
            {
                listener.Bind(localEndPoint);
                listener.Listen(10);

                // Start listening for connections.
                while (true)
                {
                    Console.WriteLine("Waiting for a connection...");
                    // Program is suspended while waiting for an incoming connection.
                    Socket handler = listener.Accept();
                    data = null;

                    // An incoming connection needs to be processed.
                    bytes = new byte[1024];
                    while (true)
                    {
                        int bytesRec = handler.Receive(bytes);
                        data = Encoding.ASCII.GetString(bytes, 0, bytesRec);
                        Console.WriteLine("Text received : {0}", data);
                        if (data.IndexOf("exit") > -1)
                        {
                            break;
                        }
                        Console.Write("Please enter your text: ");
                        string input = Console.ReadLine();
                        byte[] asciitext = System.Text.Encoding.ASCII.GetBytes(input);

                        // Send the data through the socket.
                        int bytesSent = handler.Send(asciitext);
                        if (input.IndexOf("exit") > -1)
                        {
                            break;
                        }

                    }
                    handler.Shutdown(SocketShutdown.Both);
                    handler.Close();
                    Environment.Exit(0);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        public static int Main(String[] args)
        {
            StartListening();
            return 0;
        }
    }
}