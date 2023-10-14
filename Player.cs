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
        }

        public string Name { get; set; }
        public int PlayerNumber { get; set; }
        public int Wallet { get; set; }
        public void UpdateWallet()
        {
            if (GameState == HandState.BLACKJACK)
            {
                Wallet += Bet * 3;
            }
            else if (GameState == HandState.WIN)
            {
                Wallet += Bet * 2;
            }

            FileManager.SavePlayerWallet($"{Name}, {GameState}, WALLET: {Wallet}");
        }
    }
}
