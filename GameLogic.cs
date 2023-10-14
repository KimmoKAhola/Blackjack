namespace Blackjack
{
    public static class GameLogic
    {
        public static void PlayersTurn(Player player)
        {
            while (true)
            {
                Utilities.SavePlayerAction(player); //first deal
                Graphics.PrintPlayerTitleAndSum(player);

                if (GameLogic.CheckForBlackJack(player))
                {
                    player.GameState = GameState.BLACKJACK;
                    player.LatestAction = PlayerAction.BLACKJACK;
                    Utilities.SavePlayerAction(player);
                    break;
                }

                if (GameLogic.CheckForSplit(player))
                {
                    player.LatestAction = PlayerAction.SPLIT;
                }

                if (GameLogic.CheckForBust(player))
                {
                    player.LatestAction = PlayerAction.BUST;
                    Utilities.SavePlayerAction(player);
                    break;
                }

                Utilities.PromptPlayer(player);
                char response;
                response = Console.ReadKey(false).KeyChar;

                if (response != ' ')
                {
                    player.LatestAction = PlayerAction.STAND;
                    Utilities.SavePlayerAction(player);
                    break;
                }

                Deck.DealCard(player);
                player.LatestAction = PlayerAction.HIT;
                Utilities.SavePlayerAction(player);
            }
            Graphics.PrintPlayerTitleAndSum(player);
        }
        public static bool CheckForSplit(Player player)
        {
            if (player.Hand.Count == 2)
            {
                if (player.Hand[0].Value == player.Hand[1].Value)
                {
                    Utilities.PromptPlayerSplit(player);

                    while (true)
                    {
                        char response = Console.ReadKey(false).KeyChar;
                        if (response == 'y' || response == 'Y')
                        {
                            player.SplitHand = new() { player.Hand[1] };
                            player.Hand.RemoveAt(1);
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
        public static bool CheckForBlackJack(Player player)
        {
            //This is a win condition
            if (player.HandSum() == 21)
            {
                //This is blackjack after first deal
                if (player.Hand.Count == 2)
                {
                    return true;
                }
            }
            return false;
        }
        public static bool CheckForBust(Player player)
        {
            //This is if the players hand exceeds 21 
            if (player.HandSum() > 21)
            {
                return true;
            }
            return false;
        }
        public static void CheckResults(List<Player> players)
        {
            foreach (var player in players)
            {
                if (player.GameState == GameState.BLACKJACK)
                {
                    //Player got blackjack in first deal
                    BlackJack.FunMethod();
                }
                else if (player.HandSum() > 21)
                {
                    player.GameState = GameState.LOSS;
                }
                else if (Dealer.Instance.HandSum() > 21)
                {
                    BlackJack.FunMethod();
                    Dealer.Instance.LatestAction = PlayerAction.BUST;
                    player.GameState = GameState.WIN;
                }
                else if (player.HandSum() > Dealer.Instance.HandSum())
                {
                    player.GameState = GameState.WIN;
                    BlackJack.FunMethod();
                }
                else
                    player.GameState = GameState.LOSS;
            }
        }
        public static void DealersTurn()
        {
            while (Dealer.Instance.HandSum() < 17)
            {
                Deck.DealCard(Dealer.Instance);
                Dealer.Instance.LatestAction = PlayerAction.HIT;

                Graphics.UpdateBoard();
                Thread.Sleep(1000);
            }
            Dealer.Instance.LatestAction = PlayerAction.STAND;
        }
    }

}
