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
                if (sum <= 21 && card.Title.Contains("Ace"))
                {
                    card.Value = 11;
                }
                sum += card.Value;

                if (sum > 21)
                {
                    var aces = PlayerHand.FindAll(card => card.Title.Contains("Ace"));
                    foreach (var ace in aces)
                    {
                        ace.Value = 1;
                        sum = sum - 11 + 1;
                        if(sum < 21)
                        {
                            break;
                        }
                    }
                    //card.Value = card.Value-10+1;
                    //sum = 0;
                }

            }
            return sum;
        }
    }
}
