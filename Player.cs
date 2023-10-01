using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    public class Player
    {
        private static int _playerCounter = 0;
        public Player(string name)
        {
            Name = name;
            PlayerNumber = _playerCounter++;
            PlayerHand = new List<Card>();
        }

        public string Name { get; set; }
        public int PlayerNumber { get; set; }
        public List<Card> PlayerHand { get; set; }
        public int PlayerSeat { get; set; }
        public int HandSum()
        {
            int sum = 0;
            foreach (var card in PlayerHand)
            {
                sum += card.Value;
            }
            return sum;
        }
    }
}
