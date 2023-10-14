namespace Blackjack
{
    public static class GameLogic
    {
        public static bool CheckForBlackJack(Player currentPlayer)
        {
            //This is a win condition
            if (currentPlayer.HandSum() == 21)
            {
                //This is blackjack after first deal
                if (currentPlayer.Hand.Count == 2)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool CheckForBust(Player currentPlayer)
        {
            //This is if the players hand exceeds 21 
            if (currentPlayer.HandSum() > 21)
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
