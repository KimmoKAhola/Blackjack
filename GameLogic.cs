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
            Graphics.PrintPlayerTitleAndSum(player);
            Graphics.PrintLog(); //Why does this not update correctly??????

            foreach (var hand in player.Hands)
            {
                CheckForBlackJack(player, hand);
                CheckForSplit(player);
                //Utilities.SavePlayerAction(player, hand); //Fix a separate method for first deal!

                GetPlayerMove(player, hand);

                Utilities.SavePlayerAction(player);

                Graphics.PrintLog();
            }
        }
        private static void GetPlayerMove(Player player, Hand currentHand)
        {

            while (player.LatestAction != PlayerAction.STAND && player.LatestAction != PlayerAction.BUST)
            {
                Utilities.PromptPlayerMove(player);
                char response = Char.ToUpper(Console.ReadKey(false).KeyChar);
                if (response == ' ')
                {
                    player.LatestAction = PlayerAction.HIT;
                    Utilities.ErasePrompt();
                    Deck.DealCard(currentHand, player);
                    CheckForBust(player); //Add a BUST "prompt"
                }
                else if (response == 'S')
                {
                    player.LatestAction = PlayerAction.STAND;
                    Utilities.ErasePrompt();
                }
                Utilities.LogPlayerInfo(player, currentHand);
            }
            Graphics.PrintPlayerTitleAndSum(player);
        }
        public static void CheckForSplit(Player player)
        {
            Hand firstHand = player.Hands[0];
            if (player.Hands.Count == 1
                && firstHand.CurrentCards.Count == 2
                && player.Wallet >= player.Hands[0].Bet
                && firstHand.CurrentCards[0].Value == firstHand.CurrentCards[1].Value)
            {
                Utilities.PromptPlayerSplit(player);

                while (true)
                {
                    char response = Char.ToUpper(Console.ReadKey(false).KeyChar);
                    if (response == 'Y')
                    {
                        player.Hands.Add(new());
                        player.Hands[1].CurrentCards = new() { firstHand.CurrentCards[1] };
                        player.Hands[1].Bet = player.Hands[2].Bet;
                        firstHand.CurrentCards.RemoveAt(1);
                        player.LatestAction = PlayerAction.SPLIT;
                        player.CurrentHand = player.Hands[0];
                        break;
                    }
                    else if (response == 'N')
                    {
                        break;
                    }
                }
            }
        }
        public static void CheckForBlackJack(Player player, Hand hand)
        {
            if (hand.CurrentCards.Count == 2 && hand.HandSum() == 21)
            {
                hand.HandState = HandState.BLACKJACK;
                player.LatestAction = PlayerAction.BLACKJACK;
            }
        }
        public static void CheckForBust(Player player)
        {
            if (player.CurrentHand.HandSum() > 21)
            {
                player.LatestAction = PlayerAction.BUST;
            }
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
