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
            StartTimeStamp = DateTime.UtcNow;
            EndTimeStamp = DateTime.UtcNow;
            ListOfAllPlayers = listOfAllPlayers;
        }
        public DateTime StartTimeStamp { get; set; }
        public DateTime EndTimeStamp { get; }
        public List<Player> ListOfAllPlayers { get; set; }
    }
}
