namespace Blackjack
{
    /// <summary>
    /// A class creating the game deck with a certain number of cards.
    /// Uses the Card class.
    /// </summary>
    public static class Deck
    {
        private static List<Card>? cards = GetNewDeck();
        //private static List<Card>? cards = GetAceDeck(); // Only for testing

        /// <summary>
        /// Creates a list of cards.
        /// </summary>
        public static List<Card> AllCards
        {
            get => cards ??= GetNewDeck();
            //get => cards ??= GetAceDeck(); // Only for testing
        }
        /// <summary>
        /// Creates a test deck for testing different types of edge cases.
        /// Should not be used in the live version of the program.
        /// </summary>
        /// <returns></returns>
        private static List<Card> GetAceDeck()
        {
            List<Card> aceCards = new List<Card>();
            for (int i = 0; i < 52; i++)
            {
                aceCards.Add(new Card(Enum.GetNames(typeof(AllCards))[11], 10, Card.allCardGraphics[11], "♦"));
                aceCards[i].LatestCardPosition = (101, 18);
            }
            return aceCards;
        }

        /// <summary>
        /// Creates a new deck using our allCards enum.
        /// Creates 52 card objects containing different card properties
        /// such as title, color, value etc.
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// A method which handles the first deal.
        /// Deals 2 cards to each player, including the dealer, currently at the table.
        /// </summary>
        /// <param name="players"></param>
        public static void FirstDeal(List<Player> players)
        {
            string firstDealInfo = $"[FIRST DEAL]~~~~~~\n";
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < players.Count; j++)
                {
                    DealCard(players[j].CurrentHand, players[j]);

                    Card latestCard = players[j].CurrentHand.CurrentCards.Last();
                    firstDealInfo += $"{players[j].Name} was dealt a [{latestCard.Title}{latestCard.CardSymbol}]\n";

                    GameLogic.CheckForBlackJack(players[j]);
                    Graphics.PrintPlayerHeaders(players[j]);

                }
                DealCard(Dealer.Instance.Hands[0], Dealer.Instance);

                Card dealersLatestCard = Dealer.Instance.Hand.CurrentCards.Last();
                firstDealInfo += $"The dealer was dealt a [{dealersLatestCard.Title}{dealersLatestCard.CardSymbol}]\n";
            }
            FileManager.SaveFirstDealInfo(firstDealInfo);
        }
        /// <summary>
        /// Shuffles the card deck using the Fisher-Yates shuffle algorithm.
        /// </summary>
        public static void ShuffleDeck()
        {
            cards = GetNewDeck();
            //cards = GetAceDeck(); //Only for testing
            Random randomNum = new();
            for (int i = 51; i > 0; i--)
            {
                int j = randomNum.Next(0, i);
                (cards[j], cards[i]) = (cards[i], cards[j]);
            }
        }
        /// <summary>
        /// A method which deals a single card to a specific participants
        /// active hand.
        /// </summary>
        /// <param name="hand"></param>
        /// <param name="participant"></param>
        public static void DealCard(Hand hand, Participant participant)
        {
            hand.CurrentCards.Add(cards[0]);
            if (participant is Player)
            {
                var currentPlayer = (Player)participant;

                switch (currentPlayer.PlayerNumber)
                {
                    case 1:
                        Graphics.AnimateACardFromLeftToRight(currentPlayer);
                        break;
                    case 2:
                        Graphics.AnimateACardFromTopToBottom(currentPlayer);
                        break;
                    case 3:
                        Graphics.AnimateACardFromRightToLeft(currentPlayer);
                        break;
                }
                if (hand.HandSum() == 21)
                    hand.HandState = HandState.BLACKJACK;
                Graphics.PrintHandStatus(currentPlayer, hand);
            }
            if (participant is Dealer)
            {
                Graphics.AnimateACardFromBottomToTop(Dealer.Instance.Hands.Last());
            }
            cards.RemoveAt(0);
        }

        /// <summary>
        /// A method which calculates the chance of not getting a bust when drawing a new card
        /// </summary>
        /// <param name="HandSum"></param>
        /// <returns></returns>
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
