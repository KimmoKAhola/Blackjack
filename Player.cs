namespace Blackjack
{
    public class Player : Participant
    {
        private static int _playerCounter = 1;

        /// <summary>
        /// Creates a new player and immediately adds two hands, one main hand and a second empty "slpit" hand
        /// </summary>
        /// <param name="name"></param>
        /// <param name="buyIn"></param>
        public Player(string name, int buyIn)
        {
            Name = name;
            PlayerNumber = _playerCounter++;
            Wallet = buyIn;
            Hands.Add(new());
            Hands.Add(new());
            CurrentHand = Hands[0];
        }

        public string Name { get; set; }
        public int PlayerNumber { get; set; }
        public int Wallet { get; set; }
        public Hand CurrentHand { get; set; }
        public void UpdateWallet()
        {
            foreach (var hand in Hands)
            {
                if (hand.HandState == HandState.BLACKJACK)
                {
                    Wallet += hand.Bet * 3;
                }
                else if (hand.HandState == HandState.WIN)
                {
                    Wallet += hand.Bet * 2;
                }

                FileManager.SavePlayerWallet($"{Name}, {hand.HandState}, WALLET: {Wallet}");
            }
        }
    }
}
