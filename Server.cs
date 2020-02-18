using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace UDPServer
{
    class Server
    {
        UdpClient server;
        IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, 0);

        public Server(int listenPort)
        {
            server = new UdpClient(listenPort);
            Console.WriteLine("Server initialised");
        }

        public void ReceiveData()
        {
            try
            {
                while (true)
                {
                    Console.WriteLine("Waiting for broadcast");
                    byte[] bytes = server.Receive(ref endPoint);
                    string data = Encoding.ASCII.GetString(bytes);
                    Console.WriteLine(String.Format("Received broadcast from: {0}:{1}", endPoint.Address.ToString(), endPoint.Port.ToString()));
                    Console.WriteLine("Data received: " + data);
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine(e);
            }
        }
    }
}