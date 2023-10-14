namespace Blackjack
{
    public class Participant
    {
        public Participant()
        {
            Hands = new List<Hand>();
            SplitHand = null;
        }

        public List<Hand> Hands { get; set; }
        public List<Card>? SplitHand { get; set; }
        public PlayerAction LatestAction { get; set; }
    }
}
