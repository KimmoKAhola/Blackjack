using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    public static class Deck
    {
        private static Card[]? cards = GetNewDeck();
        private static bool firstDeal = true;
        private static int counter = 0;
        public static Card[] AllCards
        {
            get => cards ??= GetNewDeck();
            //get => cards = (cards == null) ? GetNewDeck() : cards;
        }

        private static Card[] GetNewDeck()
        {
            Card[] cardNumbers = new Card[52];


            for (int i = 0; i <= 51; i++)
            {
                cardNumbers[i] = new Card(Enum.GetNames(typeof(AllCards))[i]);
            }

            return cardNumbers;
        }

        public static void ShuffleDeck()
        {
            Random randomNum = new();
            for (int i = 51; i > 0; i--)
            {
                int j = randomNum.Next(0, i);

                (cards[j], cards[i]) = (cards[i], cards[j]);
            }
        }

        public static void PrintAllCards()
        {
            foreach (Card card in cards)
            {
                Console.Write($"{card.Title}, ");
            }
        }

        public static void DealCard()
        {
            if (firstDeal)
            {
                Console.WriteLine($"{cards[counter].Title} + {cards[counter + 1].Title}");
                counter+=2;
                firstDeal = false;
            }

            else
            {
                Console.WriteLine($"{cards[counter].Title}");
                counter++;
            }
        }
    }
}
