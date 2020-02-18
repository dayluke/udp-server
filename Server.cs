using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Linq;

namespace UDPServer
{
    class Server
    {
        UdpClient server;
        IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, 0);
        Dictionary<string, Action<object[]>> functionDict = new Dictionary<string, Action<object[]>>();
        List<Player> players = new List<Player>();

        public Server(int listenPort)
        {
            server = new UdpClient(listenPort);
            functionDict["Print"] = x => Print(x.ToString());
            functionDict["PlayerJoined"] = x => PlayerJoined(x);
            functionDict["PlayerLeft"] = x => PlayerLeft(x);
            // functionDict["PlayerJoined"] = new Action<object>(PlayerJoined);
            Console.WriteLine("Server initialised");
        }

        public void ReceiveData()
        {
            try
            {
                while (true)
                {
                    byte[] bytes = server.Receive(ref endPoint);
                    string data = Encoding.ASCII.GetString(bytes);
                    Console.WriteLine(String.Format("Server received broadcast from: {0}:{1}", endPoint.Address.ToString(), endPoint.Port.ToString()));

                    object[] info = data.Split(" -> ");
                    string command = info[0].ToString();

                    List<object> t = info.ToList();
                    t.RemoveAt(0);
                    info = t.ToArray();

                    Console.WriteLine("    Command: {0}", command);
                    Console.WriteLine("    Parameters: [{0}]", string.Join(", ", info));
                    Console.WriteLine("    Info Length: {0}", info.Length);

                    // functionDict[command].DynamicInvoke(info[0]);
                    functionDict[command](info);
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                server.Close();
            }
        }

        public void PlayerJoined(object[] args)
        {
            string playerName = args[0].ToString();
            PlayerType playerType;
            if (Enum.TryParse(args[1].ToString(), out playerType)) Console.WriteLine("Enum successfully parsed.");

            players.Add(new Player(playerName, playerType));

            Console.WriteLine("{0} joined the lobby as an {1}", playerName, playerType);
            Console.WriteLine("There are now {0} players in the lobby.", players.Count);
        }

        public void PlayerLeft(object[] args)
        {
            string playerName = args[0].ToString();
            players.Remove(players.Single(player => player.username == playerName));
            
            Console.WriteLine("Player {0} has left the lobby. There are now {1} players in the lobby.", playerName, players.Count);
        }

        public void Print(string str)
        {
            Console.WriteLine("Server received: " + str);
        }
    }
}