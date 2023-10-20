namespace Blackjack
{
    /// <summary>
    /// This class inherits from the Player class
    /// and creates ONE dealer who runs the place.
    /// Implemented using singleton.
    /// </summary>
    public class Dealer : Participant
    {
        private static Dealer instance = null;
        private Dealer()
        {
            Hands.Add(new());
        }

        public Hand Hand { get { return Hands[0]; } set { Hands[0] = value; } }

        public static Dealer Instance
        {
            get
            {
                instance ??= new Dealer();
                return instance;
            }
        }
    }
}
