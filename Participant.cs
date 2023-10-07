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
            Hand = new List<Card>();
        }

        public List<Card> Hand { get; set; }

        public int HandSum()
        {
            int sum = 0;
            int aceCount = 0; // To keep track of the number of Aces

            foreach (var card in Hand)
            {
                if (card.Title.Contains("Ace"))
                {
                    aceCount++;
                    card.Value = 11;
                }
                sum += card.Value;
            }

            // Adjust Ace values if the total sum exceeds 21
            while (aceCount > 0 && sum > 21)
            {
                sum -= 10; // Change the value of an Ace from 11 to 1
                aceCount--;
            }

            return sum;
        }
    }
}
