namespace Blackjack
{
    internal class Hand
    {
        public Hand()
        {
            Cards = new();
            HandState = HandState.UNDECIDED;
        }

        public List<Card> Cards { get; set; }
        public HandState HandState { get; set; }
    }
}
