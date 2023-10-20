namespace Blackjack
{
    /// <summary>
    /// The base class for players and the dealer.
    /// Contains a constructor and a list of hands.
    /// </summary>
    public class Participant
    {

        public Participant()
        {
            Hands = new List<Hand>();
        }

        public List<Hand> Hands { get; set; }
        public PlayerAction LatestAction { get; set; }
    }
}
