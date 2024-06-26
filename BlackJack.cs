﻿namespace Blackjack
{
    public class Blackjack
    {
        private static int _gameId = 0;
        public static int GameId { get { return _gameId; } }
        public static void RunGame(List<Player> players)
        {
            InitializeNewGame(players);
            FileManager.SaveStartTimeStamp(GameId);
            Console.Clear();
            Graphics.PrintBoard();
            GameSetup(players);

            if (!GameLogic.CheckForDealerBlackJack())
            {
                foreach (var player in players)
                {
                    GameLogic.PlayersTurn(player);
                }
            }

            Thread.Sleep(1000);//Dramatic pause
            GameLogic.DealersTurn();
            GameLogic.CheckResults(players);
            Utilities.PrintGameSummary(players);

            foreach (Player player in players)
            {
                player.UpdateWallet();
            }

            while (true)
            {
                char response = char.ToUpper(Console.ReadKey(false).KeyChar);
                if (response == 'Y')
                {
                    break;
                }
                else if (response == 'N')
                {
                    Environment.Exit(0);
                }
            }
        }
        private static void GetPlayerBets(List<Player> players)
        {
            int cachedPromptWidth = 0;

            foreach (var player in players)
            {
                int bet = Utilities.PromptPlayerBet(player, ref cachedPromptWidth);

                player.Hands[0].Bet = bet;
                player.Wallet -= player.Hands[0].Bet;

                //ShowDebugWallets(players); //Currently only for debugging
            }
            Utilities.ToggleCursorVisibility();
        }
        private static void InitializeNewGame(List<Player> players)
        {
            int gameId = _gameId++;
            Dealer.Instance.Hand.CurrentCards.Clear();
            Dealer.Instance.Hand.HandState = HandState.UNDECIDED;

            foreach (Player player in players)
            {
                player.Hands[0].CurrentCards.Clear();
                player.Hands[1].CurrentCards.Clear();
                player.Hands[0].HandState = HandState.UNDECIDED;
                player.Hands[1].HandState = HandState.UNDECIDED;
                player.CurrentHand = player.Hands[0];
                player.LatestAction = 0;
            }
            Console.Clear();
        }

        private static void ShowDebugWallets(List<Player> players)
        {
            Utilities.SetConsoleColors("SETCACHED", "SETCACHED");

            Console.ResetColor();
            int cachedX = Console.CursorLeft;
            int cachedY = Console.CursorTop;

            int line = 47;
            foreach (Player player in players)
            {
                Console.SetCursorPosition(1, line);
                Console.Write($"{player.Name}: Bet:{player.Hands[0].Bet} Wallet:{player.Wallet}");
                line++;
            }

            Console.SetCursorPosition(cachedX, cachedY);
            Utilities.SetConsoleColors("GETCACHED", "GETCACHED");
        }

        private static void GameSetup(List<Player> players)
        {
            Thread.Sleep(1500);
            Utilities.log.Add("");
            Utilities.log.Add(Utilities.GetCenteredPadding("────────── NEW GAME ──────────", 80));
            Utilities.log.Add("");
            Graphics.PrintLog();
            Graphics.AnimateDeckShuffle(Deck.AllCards[0]);
            Deck.ShuffleDeck();
            Thread.Sleep(500);
            // ShowDebugWallets(players); Displays the player's funds on the left side of the screen
            GetPlayerBets(players);
            Utilities.ToggleCursorVisibility();
            Deck.FirstDeal(players);
            Utilities.SaveFirstDealInfo(players);
        }
    }
}

