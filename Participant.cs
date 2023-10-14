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
        public int HandSum()
        {
            int sum = 0;
            int aceCount = 0; // To keep track of the number of Aces

            foreach (var card in Hand)
            {
                if (card.Title.Contains("A"))
                {
                    aceCount++;
                    card.Value = 11;
                }
                sum += card.Value;
            }

            // Adjust Ace values if the total sum exceeds 21
            while (aceCount > 0 && sum > 21)
            {
                sum -= 10; // Change the value of an Ace from 11 to 1
                aceCount--;
            }

            return sum;
        }
    }
}
