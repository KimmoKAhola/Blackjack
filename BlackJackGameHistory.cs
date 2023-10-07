using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    public class BlackJackGameHistory
    {
        public BlackJackGameHistory(List<Player> listOfAllPlayers)
        {
            TimeStamp = DateTime.UtcNow;
            ListOfAllPlayers = listOfAllPlayers;
        }
        public DateTime TimeStamp { get; set; }
        public List<Player> ListOfAllPlayers { get; set; }
    }
}
