using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    public class Player : Participant
    {
        private static int _playerCounter = 0;
        public Player(string name)
        {
            Name = name;
            PlayerNumber = _playerCounter++;
            Wallet = 1000;
        }

        public string Name { get; set; }
        public int PlayerNumber { get; set; }
        public int Wallet { get; set; }

    }
}
