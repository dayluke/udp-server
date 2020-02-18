namespace UDPServer
{
    class Player
    {
        public string username;
        public PlayerType playerType;
        public int port;
        public Player(string name, PlayerType playerSpecies)
        {
            username = name;
            playerType = playerSpecies;
            // port = portNum;
        }
    }

    public enum PlayerType
    {
        ALIEN,
        MARINE
    }
}