using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Linq;


namespace TcpSim_Server
{
    class TCPSim_server
    {
        static TcpListener Listener;
        static TcpClient serverSocket;

        static Int32 port = 5000;
        static IPAddress localAddress = IPAddress.Parse(GetIpAddress());

        public void Start()
        {
            Listener = new TcpListener(localAddress, port);
            Listener.Start();
            Console.WriteLine("Server Create " + Listener.LocalEndpoint);

            try
            {
                serverSocket = Listener.AcceptTcpClient();
                Console.WriteLine("Client Accepted");

                while (true)
                {
                    ClientListener(serverSocket);

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public void ClientListener(object argument)
        {
            TcpClient tcpClient = (TcpClient)argument;
            StreamReader reader = new StreamReader(tcpClient.GetStream());
            StreamWriter writer = new StreamWriter(tcpClient.GetStream());

            Console.WriteLine("Client Connected");

            try
            {
                while (true)
                {
                    string message = reader.ReadLine();
                    string chat = "client : " + message;
                    Console.WriteLine(chat);

                }
            }
            catch (IOException e)
            {
                Console.WriteLine(e.ToString());
                Console.WriteLine(" ");
            }
            finally
            {
                Console.WriteLine("Client Disconnected");

                serverSocket.Client.Disconnect(true);
                serverSocket.Client.Close();
                Console.WriteLine("Server Closed");
                System.Environment.Exit(1);

            }

        }

        public static string GetIpAddress()
        {
            IPHostEntry localhost;
            string localAddress = "";

            // Get the hostname of the local machine
            localhost = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress address in localhost.AddressList)
            {
                // Look for the IPv4 address of the local machine
                if (address.AddressFamily.ToString() == "InterNetwork")
                {
                    // Convert the IP address to a string and return it
                    localAddress = address.ToString();
                }
            }
            return localAddress;
        }

    }

    class Program
    {
        
        static public void Main()
        {
            var server = new TCPSim_server();

            server.Start();
        }
    }
}
