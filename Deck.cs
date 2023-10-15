namespace Blackjack
{
    public static class Deck
    {
        private static List<Card>? cards = GetNewDeck();
        //private static List<Card>? cards = GetAceDeck(); // Only for testing

        public static List<Card> AllCards
        {
            get => cards ??= GetNewDeck();
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
                            card.Title = (i + 1).ToString();
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
        public static void FirstDeal(List<Player> participants)
        {
            string firstDealInfo = $"[FIRST DEAL]~~~~~~\n";
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < participants.Count; j++)
                {
                    DealCard(participants[j].Hands[0], participants[j]);

                    Card latestCard = participants[j].Hands[0].Cards.Last();
                    firstDealInfo += $"{participants[j].Name} was dealt a [{latestCard.Title}{latestCard.CardSymbol}]\n";
                }
                DealCard(Dealer.Instance.Hands[0], Dealer.Instance);
                Graphics.AnimateACardFromBottomToTop(Dealer.Instance.Hands[0].Cards.Last());

                Card dealersLatestCard = Dealer.Instance.Hands[0].Cards.Last();
                firstDealInfo += $"The dealer was dealt a [{dealersLatestCard.Title}{dealersLatestCard.CardSymbol}]\n";
            }
            FileManager.SaveFirstDealInfo(firstDealInfo);
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
        public static void DealCard(Hand hand, Participant participant)
        {
            Utilities.ErasePrompt();
            hand.Cards.Add(cards[0]);
            if (participant is Player)
            {
                var temp = (Player)participant;

                switch (temp.PlayerNumber)
                {
                    case 1:
                        Graphics.AnimateACardFromLeftToRight(hand);
                        break;
                    case 2:
                        Graphics.AnimateACardFromTopToBottom(hand);
                        break;
                    case 3:
                        Graphics.AnimateACardFromRightToLeft(hand);
                        break;
                }
            }
            //Graphics.AnimateACardFromBottomToTop(Dealer.Instance.Hand.Last());
            cards.RemoveAt(0);
        }

        public static double CalculateChanceOfSuccess(int HandSum)
        {
            int bustCards = 0;
            foreach (Card card in cards)
            {
                int cardValue = card.Value;

                if (card.Title.Contains("A"))
                {
                    cardValue = 1;
                }

                if (HandSum + cardValue > 21)
                {
                    bustCards++;
                }
            }

            double probabilityOfBust = (double)bustCards / cards.Count;
            double chanceOfSuccess = 1 - probabilityOfBust;

            return Math.Round(chanceOfSuccess, 2);
        }
    }
}
