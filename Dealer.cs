namespace Blackjack
{
    /// <summary>
    /// This class inherits from the Player class
    /// and creates ONE dealer who runs the place.
    /// Not implemented
    /// </summary>
    public class Dealer : Participant
    {
        private static Dealer instance = null;
        private Dealer()
        {

        }

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
