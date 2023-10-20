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

                GetPlayerMove(player);

                //Utilities.SavePlayerAction(player);
                Graphics.PrintLog();

                player.CurrentHand = player.Hands[1];

            }
        }
        private static void GetPlayerMove(Player player)
        {
            while (player.CurrentHand.HandState != HandState.BUST && player.CurrentHand.HandState != HandState.STOP && player.CurrentHand.CurrentCards.Count > 0)
            {
                Utilities.PromptPlayerMove(player, out int promptWidth, out int promptYPosition);
                char response = Char.ToUpper(Console.ReadKey(false).KeyChar);
                if (response == ' ')
                {
                    player.LatestAction = PlayerAction.HIT;
                    Utilities.ErasePrompt(73, 30);
                    Deck.DealCard(player.CurrentHand, player);
                    CheckForBust(player); //Add a BUST "prompt"
                }
                else if (response == 'S')
                {
                    player.LatestAction = PlayerAction.STAND;
                    player.CurrentHand.HandState = HandState.STOP;
                    Utilities.ErasePrompt(promptWidth, promptYPosition);
                }
                Utilities.LogPlayerInfo(player, player.CurrentHand);
            }
            Graphics.PrintPlayerTitleAndSum(player);
        }
        public static void CheckForSplit(Player player)
        {
            Hand mainHand = player.Hands[0];
            Hand splitHand = player.Hands[1];
            if (mainHand.CurrentCards.Count == 2
                && player.CurrentHand == player.Hands[0]
                && splitHand.CurrentCards.Count == 0
                && player.Wallet >= player.Hands[0].Bet
                && mainHand.CurrentCards[0].Value == mainHand.CurrentCards[1].Value)
            {
                Utilities.PromptPlayerSplit(player, out int promptWidth, out int promptYPosition);

                while (player.LatestAction != PlayerAction.SPLIT)
                {
                    char response = Char.ToUpper(Console.ReadKey(false).KeyChar);
                    if (response == 'Y')
                    {
                        splitHand.CurrentCards.Add(mainHand.CurrentCards[1]);
                        splitHand.Bet = mainHand.Bet;
                        mainHand.CurrentCards.RemoveAt(1);
                        player.LatestAction = PlayerAction.SPLIT;
                        player.CurrentHand = mainHand;
                    }
                    else if (response == 'N')
                    {
                        break;
                    }
                }
                Utilities.ErasePrompt(promptWidth, promptYPosition);
                Utilities.LogPlayerInfo(player, player.CurrentHand);
                //Utilities.SavePlayerAction(player);
                Graphics.PrintLog();

            }
        }
        public static void CheckForBlackJack(Player player, Hand hand)
        {
            if (hand.CurrentCards.Count == 2 && hand.HandSum() == 21)
            {
                hand.HandState = HandState.BLACKJACK;
            }
        }
        public static void CheckForBust(Player player)
        {
            if (player.CurrentHand.HandSum() > 21)
            {
                player.CurrentHand.HandState = HandState.BUST;
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
                        hand.HandState = HandState.LOST;
                    }
                    else if (Dealer.Instance.Hand.HandSum() > 21)
                    {
                        Sounds.WinSound();
                        Dealer.Instance.Hand.HandState = HandState.BUST;
                        hand.HandState = HandState.WIN;
                    }
                    else if (hand.HandSum() > Dealer.Instance.Hand.HandSum())
                    {
                        hand.HandState = HandState.WIN;
                        Sounds.WinSound();
                    }
                    else
                        hand.HandState = HandState.LOST;
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
