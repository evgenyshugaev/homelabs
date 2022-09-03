namespace SpaceShipGame
{
    public class MessageDto
    {
        public string GameId { get; set; }
        public string ObjectId { get; set; }
        public string Command { get; set; }
        public object[] Parameters { get; set; }
    }
}