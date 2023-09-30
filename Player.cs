using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    public class Player
    {
        private static int playerCounter = 0;
        public Player(string name)
        {
            Name = name;
            PlayerNumber = playerCounter++;
            PlayerHand = new List<int>();
            //PlayerHand = playerHand;
        }

        public string Name { get; set; }
        public int PlayerNumber { get; set; }
        public List<int> PlayerHand { get; set; }

        public void PlayerInfo()
        {
            Console.WriteLine($"{Name}, {PlayerNumber}, {PlayerHand}");
        }
    }
}
