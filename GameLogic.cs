namespace Blackjack
{
    public static class GameLogic
    {
        public static bool CheckForDealerBlackJack()
        {
            if (Dealer.Instance.Hand.HandSum() == 21)
            {
                return true;
            }
            return false;
        }
        public static void PlayersTurn(Player player)
        {
            Hand hand = player.Hands[0];
            while (true)
            {
                //Utilities.SavePlayerAction(player, hand); //first deal
                if (CheckForDealerBlackJack())
                {
                    break;
                }
                Graphics.PrintPlayerTitleAndSum(player);
                Graphics.UpdateBoard();
                Utilities.LogPlayerInfo(player, hand);

                if (CheckForBlackJack(hand))
                {
                    hand.HandState = HandState.BLACKJACK;
                    player.LatestAction = PlayerAction.BLACKJACK;
                    Utilities.SavePlayerAction(player, hand);
                    break;
                }

                if (CheckForSplit(player))
                {
                    player.LatestAction = PlayerAction.SPLIT;
                    break;
                }

                if (CheckForBust(hand))
                {
                    player.LatestAction = PlayerAction.BUST;
                    Utilities.SavePlayerAction(player, hand);
                    break;
                }

                Utilities.PromptPlayer(player);
                char response;
                response = Console.ReadKey(false).KeyChar;

                if (response != ' ')
                {
                    player.LatestAction = PlayerAction.STAND;
                    Utilities.SavePlayerAction(player, hand);
                    break;
                }

                Deck.DealCard(hand, player);
                player.LatestAction = PlayerAction.HIT;
                Utilities.SavePlayerAction(player, hand);

            }
            Graphics.PrintPlayerTitleAndSum(player);

            if (player.Hands.Count > 1)
            {
                foreach (var currentHand in player.Hands)
                {
                    while (true)
                    {
                        //Utilities.SavePlayerAction(player, hand); //first deal
                        if (CheckForDealerBlackJack())
                        {
                            break;
                        }
                        Graphics.PrintPlayerTitleAndSum(player);
                        Graphics.UpdateBoard();
                        Utilities.LogPlayerInfo(player, currentHand);

                        if (CheckForBlackJack(currentHand))
                        {
                            currentHand.HandState = HandState.BLACKJACK;
                            player.LatestAction = PlayerAction.BLACKJACK;
                            Utilities.SavePlayerAction(player, currentHand);
                            break;
                        }

                        if (CheckForSplit(player))
                        {
                            player.LatestAction = PlayerAction.SPLIT;
                            break;
                        }

                        if (CheckForBust(currentHand))
                        {
                            player.LatestAction = PlayerAction.BUST;
                            Utilities.SavePlayerAction(player, currentHand);
                            break;
                        }

                        Utilities.PromptPlayer(player);
                        char response;
                        response = Console.ReadKey(false).KeyChar;

                        if (response != ' ')
                        {
                            player.LatestAction = PlayerAction.STAND;
                            Utilities.SavePlayerAction(player, currentHand);
                            break;
                        }

                        Deck.DealCard(currentHand, player);
                        player.LatestAction = PlayerAction.HIT;
                        Utilities.SavePlayerAction(player, currentHand);
                    }
                    Graphics.PrintPlayerTitleAndSum(player);
                }
            }

        }
        public static bool CheckForSplit(Player player)
        {
            Hand firstHand = player.Hands[0];
            if (player.Hands.Count == 1 && firstHand.Cards.Count == 2 && player.Wallet >= player.Hands[0].Bet)
            {
                if (firstHand.Cards[0].Value == firstHand.Cards[1].Value)
                {
                    Utilities.PromptPlayerSplit(player);

                    while (true)
                    {

                        Hand secondHand = new Hand();
                        player.Hands.Add(secondHand);

                        int x = firstHand.Cards[0].LatestCardPosition.LatestXPosition + 7 / 2;
                        int y = firstHand.Cards[0].LatestCardPosition.LatestYPosition;

                        //Erase both cards
                        Graphics.EraseAPrintedCard(x, y);
                        Graphics.EraseAPrintedCard(x + 7 / 2 + 1, y);


                        secondHand.Cards.Add(firstHand.Cards[1]);
                        firstHand.Cards.RemoveAt(1);

                        secondHand.Bet = firstHand.Bet;


                        firstHand.Cards[0].LatestCardPosition = (x, y);
                        secondHand.Cards[0].LatestCardPosition = (x, y + 6 + 1); // 6 = card height

                        Graphics.PrintASplitHand(player.Hands);

                        return true;


                    }
                }
            }

            return false;
        }
        public static bool CheckForBlackJack(Hand hand)
        {
            if (hand.Cards.Count == 2)
            {
                if (hand.HandSum() == 21)
                {
                    return true;
                }
            }
            return false;
        }
        public static bool CheckForBust(Hand hand)
        {
            if (hand.HandSum() > 21)
            {
                return true;
            }
            return false;
        }
        public static void CheckResults(List<Player> players)
        {
            foreach (var player in players)
            {
                foreach (var hand in player.Hands)
                {
                    if (hand.HandState == HandState.BLACKJACK)
                    {
                        //Player got blackjack in first deal
                        Sounds.WinSound();
                    }
                    else if (hand.HandSum() > 21)
                    {
                        hand.HandState = HandState.LOSS;
                    }
                    else if (Dealer.Instance.Hand.HandSum() > 21)
                    {
                        Sounds.WinSound();
                        Dealer.Instance.LatestAction = PlayerAction.BUST;
                        hand.HandState = HandState.WIN;
                    }
                    else if (hand.HandSum() > Dealer.Instance.Hand.HandSum())
                    {
                        hand.HandState = HandState.WIN;
                        Sounds.WinSound();
                    }
                    else
                        hand.HandState = HandState.LOSS;
                }
            }
        }
        public static void DealersTurn()
        {
            while (Dealer.Instance.Hand.HandSum() < 17)
            {
                Deck.DealCard(Dealer.Instance.Hand, Dealer.Instance);
                Dealer.Instance.LatestAction = PlayerAction.HIT;

                Graphics.UpdateDealerBoard();
                Thread.Sleep(1000);
            }
            Dealer.Instance.LatestAction = PlayerAction.STAND;
        }
    }
}
