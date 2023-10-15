namespace Blackjack
{
    public class Participant
    {
        public Participant()
        {
            Hands = new List<Hand>()
            {
                new()
            };
        }

        public List<Hand> Hands { get; set; }
        public PlayerAction LatestAction { get; set; }
    }
}
