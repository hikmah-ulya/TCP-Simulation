using System;
using System.IO;
using System.Net.Sockets;
using System.Net;

namespace TcpSim_Client
{
    class TCPSim_client
    {
        static IPAddress localAddress = IPAddress.Parse(GetIpAddress());

        public void Start()
        {
            try
            {
                Console.WriteLine("Client Start");
                TcpClient clientSocket = new TcpClient();
                clientSocket.Connect(localAddress, 5000);
                Console.WriteLine("Server Connected");

                StreamWriter writer = new StreamWriter(clientSocket.GetStream());

                while (true)
                {
                    if (clientSocket.Connected)
                    {
                        //send message  
                        string input = Console.ReadLine();
                        writer.WriteLine(input);
                        writer.Flush();
                    }
                }
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }
            Console.WriteLine("Press any key to exit from client program");

            Console.ReadKey();
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
        static void Main()
        {
            var client = new TCPSim_client();

            client.Start();

        }
    }
}
