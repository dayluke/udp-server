using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace UDPServer
{
    class Program
    {
        private const int listenPort = 44564;

        static void Main(string[] args)
        {
            Server server = new Server(listenPort);
            Client client = new Client(listenPort, "Luke");

            // string data = "Print -> Hello World!";

            // client.SendData(data);
            server.ReceiveData();
            
            // Test();
            // Tutorial();
        }

        public static void Test()
        {
            UdpClient server = new UdpClient(listenPort);
            Console.WriteLine("Server initialised");

            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), listenPort);
            UdpClient client = new UdpClient();
            client.Connect(endPoint);
            Console.WriteLine("Client connected");

            IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);

            Byte[] sendBytes = Encoding.ASCII.GetBytes("Is anybody there?");
            client.Send(sendBytes, sendBytes.Length);
            Console.WriteLine("Bytes sent from client");

            try
            {
                Byte[] bytes = server.Receive(ref RemoteIpEndPoint);
                string data = Encoding.ASCII.GetString(bytes);
                Console.WriteLine("Bytes received on server");
                Console.WriteLine(data);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            /*try
            {
                while (true)
                {
                    Console.WriteLine("Waiting for broadcast");
                    byte[] bytes = server.Receive(ref endPoint);
                    
                    Console.WriteLine("Received broadcast: " + endPoint);
                    Console.WriteLine(Encoding.ASCII.GetString(bytes, 0, bytes.Length));
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                server.Close();
            }*/
        }

        public static void Tutorial()
        {
            UdpClient udpClient = new UdpClient(11000);
            try{
                udpClient.Connect("127.0.0.1", 11000);

                // Sends a message to the host to which you have connected.
                Byte[] sendBytes = Encoding.ASCII.GetBytes("Is anybody there?");
            
                udpClient.Send(sendBytes, sendBytes.Length);

                //IPEndPoint object will allow us to read datagrams sent from any source.
                IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);

                // Blocks until a message returns on this socket from a remote host.
                Byte[] receiveBytes = udpClient.Receive(ref RemoteIpEndPoint); 
                string returnData = Encoding.ASCII.GetString(receiveBytes);
        
                // Uses the IPEndPoint object to determine which of these two hosts responded.
                Console.WriteLine("This is the message you received " +
                                            returnData.ToString());
                Console.WriteLine("This message was sent from " +
                                            RemoteIpEndPoint.Address.ToString() +
                                            " on their port number " +
                                            RemoteIpEndPoint.Port.ToString());

                udpClient.Close();
            }  
            catch (Exception e )
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
