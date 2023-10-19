namespace Blackjack
{
    public class Player : Participant
    {
        private static int _playerCounter = 1;
        public Player(string name, int buyIn)
        {
            Name = name;
            PlayerNumber = _playerCounter++;
            Wallet = buyIn;
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
