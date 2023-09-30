using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    public class Deck
    {
        public Deck()
        {
            cards = GetNewDeck();
        }

        private Card[] cards = new Card[52];

        private static Card[] GetNewDeck()
        {
            Card[] cardsArray = new Card[52];

            for (int i = 1; i <= 52; i++)
            {
                cardsArray[i] = new Card(i);
            }

            return cardsArray;
        }
    }
}
