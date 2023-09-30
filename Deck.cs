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


        public static Card[] AllCards
        {
            get => cards ??= GetNewDeck();
            //get => cards = (cards == null) ? GetNewDeck() : cards;
        }

        private static Card[] GetNewDeck()
        {
            Card[] cardNumbers = new Card[52];
            int cardIndex = 0;
            for (int j = 0; j <= 3; j++)
            {
                for (int i = 0; i <= 12; i++)
                {
                    if (i < 9)
                    {
                        cardNumbers[cardIndex] = new Card(Enum.GetNames(typeof(AllCards))[cardIndex], i + 1);
                        cardIndex++;
                        continue;
                    }
                    cardNumbers[cardIndex] = new Card(Enum.GetNames(typeof(AllCards))[cardIndex], 10);
                    cardIndex++;
                }
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
                Console.WriteLine($"[Card value: {card.Value}, {card.Title}]");
            }
        }

        private static int counter = 0;
        public static void DealCard(Player player, bool firstDeal)
        {
            if (firstDeal)
            {
                Console.WriteLine($"{cards[counter].Title} + {cards[counter + 1].Title}");

                player.PlayerHand.Add(cards[counter]);
                //player.PlayerHand.Add(cards[counter + 1].Title);
                counter += 2;
                firstDeal = false;
            }

            else
            {
                //Console.WriteLine($"{}");
                //player.PlayerHand.Add(cards[counter].Title);
                counter++;
            }
        }
    }
}
