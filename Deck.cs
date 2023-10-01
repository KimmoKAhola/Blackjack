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
        private static List<Card>? cards = GetNewDeck();


        public static List<Card> AllCards
        {
            get => cards ??= GetNewDeck();
            //get => cards = (cards == null) ? GetNewDeck() : cards;
        }

        private static List<Card> GetNewDeck()
        {
            List<Card> cardNumbers = new();
            int cardIndex = 0;
            for (int j = 0; j <= 3; j++)
            {
                for (int i = 0; i <= 12; i++)
                {
                    if (i < 9)
                    {
                        cardNumbers.Add(new Card(Enum.GetNames(typeof(AllCards))[cardIndex], i + 1, Card.allCardGraphics[cardIndex]));
                        cardIndex++;
                        continue;
                    }
                    cardNumbers.Add(new Card(Enum.GetNames(typeof(AllCards))[cardIndex], 10, Card.allCardGraphics[cardIndex]));
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
        public static void DealCard(Player player, bool firstDeal)
        {
            if (firstDeal)
            {
                Console.WriteLine($"{cards[0].Title} + {cards[1].Title}");

                player.PlayerHand.Add(cards[0]);
                cards.RemoveAt(0);
                player.PlayerHand.Add(cards[0]);
                cards.RemoveAt(0);
                firstDeal = false;
            }
            else
            {
                Console.WriteLine($"{cards[0].Title}");
                player.PlayerHand.Add(cards[0]);
                cards.RemoveAt(0);
            }
        }
    }
}
