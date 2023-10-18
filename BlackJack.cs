namespace Blackjack
{
    public class BlackJack
    {
        private static int _gameId = 0;
        public static int GameId { get { return _gameId; } }
        public void RunGame(List<Player> players)
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

            Graphics.UpdateDealerBoard();
            //Dramatic pause
            Thread.Sleep(1000);
            GameLogic.DealersTurn();
            GameLogic.CheckResults(players);

            foreach (Player player in players)
            {
                player.UpdateWallet();
            }

            Utilities.DisplayGameSummary(players);

            char response2 = Console.ReadKey().KeyChar;
            if (response2 == 'n' || response2 == 'N')
            {
                Environment.Exit(0); //TODO remove later.
            }
            RunGame(players);
        }
        private static void GetPlayerBets(List<Player> players)
        {
            Utilities.SetConsoleColors("Y", "DG");
            int promptLength = 0;
            int betLength = 0;
            int bet = 0;

            foreach (var player in players)
            {
                int yPosition = 30;
                string prompt = $"Player {player.Name}, please enter your bet";

                if (promptLength < prompt.Length)
                {
                    promptLength = prompt.Length;
                }
                if (betLength < bet.ToString().Length)
                {
                    betLength = bet.ToString().Length;
                }

                while (true)
                {
                    string betPrompt = $"BET: ";
                    string errorMessage = $"Invalid input, please try again";
                    string promptClearLine = new(' ', promptLength);
                    string betClearLine = new(' ', betLength + betPrompt.Length);

                    string[] clearLines = { promptClearLine, betClearLine };
                    string[] promptLines = { prompt, betPrompt };

                    Utilities.PrintCenteredAlignedStringArray(clearLines, yPosition);
                    Utilities.PrintCenteredAlignedStringArray(promptLines, yPosition);

                    if (int.TryParse(Console.ReadLine(), out bet))
                    {
                        if (bet <= player.Wallet)
                        {
                            player.Hands[0].Bet = bet;
                            player.Wallet -= player.Hands[0].Bet;

                            ShowDebugWallets(players);
                            break;
                        }
                    }
                    yPosition++;
                    Utilities.PrintCenteredString(errorMessage, yPosition);
                    yPosition--;
                }
            }
        }
        private static void InitializeNewGame(List<Player> players)
        {
            int gameId = _gameId++;
            foreach (Player player in players)
            {

                player.Hands[0].CurrentCards.Clear();
                if (player.Wallet == 0)
                {
                    // Fix error handling here.
                }
                if (player.Hands.Count < 1)
                {
                    player.Hands.RemoveAt(1);
                }

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
            Graphics.AnimateDeckShuffle(Deck.AllCards[0]);
            Deck.ShuffleDeck();
            Thread.Sleep(500);
            ShowDebugWallets(players);
            GetPlayerBets(players);
            Deck.FirstDeal(players);
            Utilities.SaveFirstDealInfo(players);
        }
    }
}

