using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    internal class BlackJackGameHistory
    {
        public BlackJackGameHistory(BlackJack blackJack, List<Player> listOfPlayers)
        {
            BlackJack = blackJack;
            TimeStamp = DateTime.UtcNow;

        }
        public BlackJack BlackJack { get; set; }
        public DateTime TimeStamp { get; }
    }
}
