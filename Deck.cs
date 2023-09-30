using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    public static class Deck
    {
        private static Card[]? cards = GetNewDeck();
        public static Card[] AllCards
        {
            get
            {
                if (cards == null)
                {
                    cards = GetNewDeck();
                }

                return cards;
            }
        }

        private static Card[] GetNewDeck()
        {
            Card[] cardsArray = new Card[52];

            for (int i = 1; i < 52; i++)
            {
                cardsArray[i] = new Card(i);
            }

            return cardsArray;
        }

        //public Card[] Cards { get; set; }

        public static void ShuffleDeck()
        {
            Random randomNum = new();
            for (int i = 52; i > 1; i--)
            {
                int j = randomNum.Next(0, i);

                (cards[j], cards[i]) = (cards[i], cards[j]);
            }
        }

        public static void PrintAllCards()
        {
            cards = GetNewDeck();
            foreach (Card card in cards)
            {
                Console.WriteLine(card.CardNumber);
            }
        }
    }
}
