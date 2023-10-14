namespace Blackjack
{
    internal class Hand
    {
        public Hand()
        {
            Cards = new();
        }

        public List<Card> Cards { get; set; }
    }
}
