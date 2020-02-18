using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace UDPServer
{
    class Client
    {
        UdpClient client = new UdpClient();
        public Client(int listenPort, string name)
        {
            IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), listenPort);
            client.Connect(ipEndPoint);
            Console.WriteLine("Client connected to: {0}:{1}", ipEndPoint.Address.ToString(), ipEndPoint.Port.ToString());
            
            SendData(String.Format("PlayerJoined -> {0} -> {1}", name, PlayerType.MARINE));
            SendData(String.Format("PlayerLeft -> {0}", name));

            client.Close();
        }

        public void SendData(string dataToSend)
        {
            Byte[] sendBytes = Encoding.ASCII.GetBytes(dataToSend);
            client.Send(sendBytes, sendBytes.Length);
            Console.WriteLine("Client sent data");
        }
    }
}