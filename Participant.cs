namespace Blackjack
{
    public class Participant
    {
        public Participant()
        {
            Hand = new List<Card>();
            SplitHand = null;
        }

        public List<Card> Hand { get; set; }
        public List<Card>? SplitHand { get; set; }
        public PlayerAction LatestAction { get; set; }
    }
}
