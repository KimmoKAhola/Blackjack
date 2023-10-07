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
        //private static List<Card>? cards = GetAceDeck(); // Only for testing
        private static List<Card> animationCards = GetNewAnimationDeck();

        public static List<Card> AllCards
        {
            get => cards ??= GetNewDeck();
            //get => cards = (cards == null) ? GetNewDeck() : cards;
        }

        public static List<Card> AnimationCards
        {
            get => animationCards ??= GetNewAnimationDeck();
        }

        /// <summary>
        /// This deck is only used for testing animations. Uses a card
        /// which has a blue background and a background graphic
        /// which is loaded from the last index of our card enum.
        /// </summary>
        /// <returns></returns>
        private static List<Card> GetNewAnimationDeck()
        {
            List<Card> cards = new List<Card>();
            cards.Add(new Card(Card.allCardGraphics[52], Card.allCardGraphics[53]));
            return cards;
        }
        //Only for testing edge cases
        private static List<Card> GetAceDeck()
        {
            List<Card> aceCards = new List<Card>();
            for (int i = 0; i < 52; i++)
            {
                aceCards.Add(new Card(Enum.GetNames(typeof(AllCards))[0], 1, Card.allCardGraphics[0], "♦"));
            }
            return aceCards;
        }

        private static List<Card> GetNewDeck()
        {
            List<Card> cardNumbers = new();
            int cardIndex = 0;
            for (int j = 0; j <= 3; j++)
            {
                for (int i = 0; i <= 12; i++)
                {
                    Card card = new();
                    card.CardSymbol = "♠";
                    if (j == 1)
                    {
                        card.IsRed = true;
                        card.CardSymbol = "♥";
                    }
                    else if (j == 2)
                    {
                        card.CardSymbol = "♣";
                    }
                    else if (j == 3)
                    {
                        card.IsRed = true;
                        card.CardSymbol = "♦";
                    }
                    if (i < 10)
                    {
                        if (i == 0)
                        {
                            card.Title = "A";
                            card.Value = i + 1;
                            card.CardGraphic = Card.allCardGraphics[cardIndex];
                        }
                        else
                        {
                            card.Title = (i+1).ToString();
                            card.Value = i + 1;
                            card.CardGraphic = Card.allCardGraphics[cardIndex];
                        }
                        cardNumbers.Add(card);    
                        cardIndex++;
                        continue;
                    }
                    card.Title = Enum.GetNames(typeof(AllCards))[cardIndex][..1];
                    card.Value = 10;
                    card.CardGraphic = Card.allCardGraphics[cardIndex];
                    cardNumbers.Add(card);
                    cardIndex++;
                }
            }
            return cardNumbers;
        }
        public static void FirstDeal(List<Player> participants, Dealer dealer)
        {
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < participants.Count; j++)
                {
                    Deck.DealCard(participants[j]);
                }
                Deck.DealCard(dealer);
            }
        }
        public static void ShuffleDeck()
        {
            cards = GetNewDeck();

            Random randomNum = new();
            for (int i = 51; i > 0; i--)
            {
                int j = randomNum.Next(0, i);

                (cards[j], cards[i]) = (cards[i], cards[j]);
            }
        }
        public static void DealCard(Participant participant)
        {
            participant.Hand.Add(cards[0]);
            cards.RemoveAt(0);
        }
    }
}
