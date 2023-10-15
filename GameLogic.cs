namespace Blackjack
{
    public static class GameLogic
    {
        public static void PlayersTurn(Player player)
        {
            foreach (var hand in player.Hands)
            {
                while (true)
                {
                    //Utilities.SavePlayerAction(player, hand); //first deal
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
                        char response = Console.ReadKey(false).KeyChar;
                        if (response == 'y' || response == 'Y')
                        {
                            player.Hands.Add(new());
                            player.Hands[1].Cards = new() { firstHand.Cards[1] };
                            player.Hands[1].Bet = player.Hands[2].Bet;
                            firstHand.Cards.RemoveAt(1);
                            return true;
                        }
                        else if (response == 'n' || response == 'N')
                        {
                            return false;
                        }
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
                    else if (Dealer.Instance.Hands[0].HandSum() > 21)
                    {
                        Sounds.WinSound();
                        Dealer.Instance.LatestAction = PlayerAction.BUST;
                        hand.HandState = HandState.WIN;
                    }
                    else if (hand.HandSum() > Dealer.Instance.Hands[0].HandSum())
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
            while (Dealer.Instance.Hands[0].HandSum() < 17)
            {
                Deck.DealCard(Dealer.Instance.Hands[0], Dealer.Instance);
                Dealer.Instance.LatestAction = PlayerAction.HIT;

                Graphics.UpdateDealerBoard();
                Thread.Sleep(1000);
            }
            Dealer.Instance.LatestAction = PlayerAction.STAND;
        }
    }
}
