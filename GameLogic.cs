﻿namespace Blackjack
{
    public static class GameLogic
    {
        /// <summary>
        /// A method checking if the dealer has blackjack at the start of a round.
        /// </summary>
        /// <returns></returns>
        public static bool CheckForDealerBlackJack()
        {
            if (Dealer.Instance.Hand.HandSum() == 21)
            {
                Dealer.Instance.Hand.HandState = HandState.BLACKJACK;
                Utilities.UpdateDealerLog();
                Graphics.PrintLog();
                return true;
            }
            return false;
        }
        /// <summary>
        /// A method that runs the current players turn.
        /// Loops through all of the player's hands, if more than one, and
        /// contains methods that check for win, bust, blackjack, split.
        /// Contains print methods to update the board.
        /// </summary>
        /// <param name="player"></param>
        public static void PlayersTurn(Player player)
        {
            Utilities.log.Add(Utilities.GetCenteredPadding($"- - - - {player.Name.ToUpper()}'S TURN - - - -", 80));
            Graphics.PrintLog();
            foreach (var hand in player.Hands)
            {
                CheckForSplit(player);
                if (hand.HandState != HandState.BLACKJACK)
                    GetPlayerMove(player);

                player.CurrentHand = player.Hands[1];
            }
        }
        /// <summary>
        /// A method that reads the player's move and handles the logic.
        /// Contains methods for deciding player actions and hand state.
        /// </summary>
        /// <param name="player"></param>
        private static void GetPlayerMove(Player player)
        {
            while (player.CurrentHand.HandState != HandState.BUSTED
                && player.CurrentHand.HandState != HandState.STOOD
                && player.CurrentHand.HandState != HandState.BLACKJACK
                && player.CurrentHand.CurrentCards.Count > 0)
            {
                CheckForBlackJack(player);
                if (player.CurrentHand.HandState == HandState.BLACKJACK)
                    break;

                Utilities.PromptPlayerMove(player, out int promptWidth, out int promptYPosition);
                char response = Char.ToUpper(Console.ReadKey(false).KeyChar);
                if (response == ' ')
                {
                    player.LatestAction = PlayerAction.HIT;
                    Utilities.ErasePrompt(promptWidth, promptYPosition);
                    Deck.DealCard(player.CurrentHand, player);
                    CheckForBust(player);
                }
                else if (response == 'S')
                {
                    player.LatestAction = PlayerAction.STAND;
                    player.CurrentHand.HandState = HandState.STOOD;
                    Utilities.ErasePrompt(promptWidth, promptYPosition);
                }
                Utilities.UpdatePlayerLog(player, player.CurrentHand);
                Graphics.PrintLog();
            }

            Utilities.PromptEndedHand(player);
        }
        /// <summary>
        /// A methods which contains logic if the player will split its hand.
        /// Contains calls to several print methods from the Graphics class.
        /// </summary>
        /// <param name="player"></param>
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
                        int x = mainHand.CurrentCards[0].LatestCardPosition.LatestXPosition;
                        int y = mainHand.CurrentCards[0].LatestCardPosition.LatestYPosition;

                        if (player.PlayerNumber != 3)
                        {
                            for (int i = 0; i < 4; i++)
                            {
                                Graphics.EraseAPrintedCard(x + i * Graphics._cardWidth / 2, y);
                            }
                        }
                        else if (player.PlayerNumber == 3)
                        {
                            for (int i = 0; i < 2; i++)
                            {
                                Graphics.EraseAPrintedCard(x + Graphics._cardWidth + Graphics._cardWidth * i / 2, y);
                            }
                        }
                        Graphics.PrintASplitHand(player);
                        Graphics.PrintHandSum(player, player.Hands[0]);
                        Graphics.PrintHandSum(player, player.Hands[1]);
                    }
                    else if (response == 'N')
                    {
                        break;
                    }
                }
                Utilities.ErasePrompt(promptWidth, promptYPosition);
                Utilities.UpdatePlayerLog(player, player.CurrentHand);
                //Graphics.PrintLog();

            }
        }
        /// <summary>
        /// Checks if a player has blackjack on its current hand.
        /// Changes the Hand State to BLACKJACK if the player has blackjack.
        /// </summary>
        /// <param name="hand"></param>
        public static void CheckForBlackJack(Player player)
        {
            if (player.CurrentHand.CurrentCards.Count == 2 && player.CurrentHand.HandSum() == 21)
            {
                Utilities.UpdatePlayerLog(player, player.CurrentHand);
                Graphics.PrintLog();
                player.CurrentHand.HandState = HandState.BLACKJACK;
            }
        }
        /// <summary>
        /// Checks if a player busts on its current hand.
        /// Changes the Hand State to BUSTED if the player has a card sum over 21.
        /// </summary>
        /// <param name="player"></param>
        public static void CheckForBust(Player player)
        {
            if (player.CurrentHand.HandSum() > 21)
            {
                player.CurrentHand.HandState = HandState.BUSTED;
            }
        }
        /// <summary>
        /// A method for calculating the results for each player and their hands
        /// at the end of the game.
        /// Calculates wins and losses.
        /// Changes hand states of each player hands.
        /// Plays a nice little sound if one or more player wins.
        /// </summary>
        /// <param name="players"></param>
        public static void CheckResults(List<Player> players)
        {
            foreach (var player in players)
            {
                foreach (var hand in player.Hands)
                {
                    if (hand.HandState == HandState.BLACKJACK)
                    {
                        Sounds.WinSound();
                    }
                    else if (hand.HandSum() > 21)
                    {
                        hand.HandState = HandState.LOST;
                    }
                    else if (Dealer.Instance.Hand.HandSum() > 21)
                    {
                        Sounds.WinSound();
                        Dealer.Instance.Hand.HandState = HandState.BUSTED;
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
        /// <summary>
        /// A method containing the dealer AI.
        /// Hard coded according to official blackjack rules.
        /// </summary>
        public static void DealersTurn()
        {
            Utilities.log.Add(Utilities.GetCenteredPadding($"- - - - DEALER'S TURN - - - -", 80));
            Graphics.PrintLog();
            while (Dealer.Instance.Hand.HandSum() < 17)
            {
                Deck.DealCard(Dealer.Instance.Hand, Dealer.Instance);
                Dealer.Instance.LatestAction = PlayerAction.HIT;

                if (Dealer.Instance.Hand.HandSum() < 22)
                {
                    Dealer.Instance.LatestAction = PlayerAction.STAND;
                }
                else
                {
                    Dealer.Instance.Hand.HandState = HandState.BUSTED;
                }

                Utilities.UpdateDealerLog();
                Graphics.PrintLog();
                Thread.Sleep(1500);
            }
        }
    }
}
