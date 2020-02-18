using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace UDPServer
{
    class Client
    {
        UdpClient client = new UdpClient();
        public Client(int listenPort)
        {
            IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), listenPort);
            client.Connect(ipEndPoint);
            Console.WriteLine(String.Format("Client connected to: {0}:{1}", ipEndPoint.Address.ToString(), ipEndPoint.Port.ToString()));
        }

        public void SendData(string dataToSend)
        {
            Byte[] sendBytes = Encoding.ASCII.GetBytes(dataToSend);
            client.Send(sendBytes, sendBytes.Length);
            Console.WriteLine("Client sent data");
        }
    }
}