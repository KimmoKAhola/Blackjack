using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

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
            GameState = GameState.UNDECIDED;
            Bet = 0;
        }

        public string Name { get; set; }
        public int PlayerNumber { get; set; }
        public int Wallet { get; set; }
        public int Bet { get; set; }
        public GameState GameState { get; set; }

        public void UpdateWallet()
        {
            if (GameState == GameState.BLACKJACK)
            {
                Wallet += Bet * 3;
            }
            else if (GameState == GameState.WIN)
            {
                Wallet += Bet * 2;
            }
            else
            {
                //Bet is already withdrawn from the wallet at this point
            }
            FileManager.SavePlayerWallet($"{Name}, {GameState}, wallet: {Wallet}");
        }
    }
}
