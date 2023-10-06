using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    public class Participant 
    {
        public Participant()
        {
            PlayerHand = new List<Card>();
        }

        public List<Card> PlayerHand { get; set; }

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
