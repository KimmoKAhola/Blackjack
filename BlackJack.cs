using System.Media;

namespace Blackjack
{
    public class BlackJack
    {
        private static int _gameId = 0;
        public int GameId { get { return _gameId; } }
        public void RunGame(List<Player> players) //skicka in en lista med spelare sen
        {
            InitializeNewGame(players);
            //Console.CursorVisible = true;
            FileManager.SaveStartTimeStamp(GameId);
            Graphics.PrintBoard();
            GameSetup(players);

            int currentPlayer = 0;
            int first = 0;
            while (currentPlayer < players.Count)
            {
                while (true)
                {
                    Utilities.SavePlayerAction(players[currentPlayer]); //first deal
                    Graphics.UpdateBoard(players, currentPlayer);
                    Graphics.PrintPlayerTitleAndSum(players[currentPlayer]);

                    if (GameLogic.CheckForBlackJack(players[currentPlayer]))
                    {
                        players[currentPlayer].GameState = GameState.BLACKJACK;
                        players[currentPlayer].LatestAction = PlayerAction.BLACKJACK;
                        Utilities.SavePlayerAction(players[currentPlayer]);
                        break;
                    }

                    if (GameLogic.CheckForBust(players[currentPlayer]))
                    {
                        players[currentPlayer].LatestAction = PlayerAction.BUST;
                        Utilities.SavePlayerAction(players[currentPlayer]);
                        break;
                    }

                    char response;
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    response = Console.ReadKey().KeyChar;
                    Console.ForegroundColor = ConsoleColor.Yellow;

                    if (response != ' ')
                    {
                        players[currentPlayer].LatestAction = PlayerAction.STAND;
                        Utilities.SavePlayerAction(players[currentPlayer]);
                        break;
                    }

                    Deck.DealCard(players[currentPlayer]);
                    players[currentPlayer].LatestAction = PlayerAction.HIT;
                    Utilities.SavePlayerAction(players[currentPlayer]);
                }
                Graphics.PrintPlayerTitleAndSum(players[currentPlayer]);
                currentPlayer++;
                first++;
            }

            //Erase the dealer's card //TODO here we have an error when erasing cards.
            Graphics.EraseAPrintedCard(107, 0);
            Graphics.UpdateBoard();
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

        private void GetPlayerBets(List<Player> players)
        {
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.ForegroundColor = ConsoleColor.Yellow;

            foreach (var player in players)
            {
                Console.SetCursorPosition(80, 30);
                Console.Write($"Player {player.Name}, please enter your bet");
                Console.SetCursorPosition(80, 32);
                Console.Write($"                                ");


                while (true)
                {
                    Console.SetCursorPosition(80, 31);
                    Console.Write($"BET: ");
                    if (int.TryParse(Console.ReadLine(), out int bet))
                    {
                        if (bet <= player.Wallet)
                        {
                            player.Bet = bet;
                            player.Wallet -= player.Bet;
                            Console.SetCursorPosition(80, 31);
                            Console.Write($"                                      ");
                            break;
                        }
                    }
                    Console.SetCursorPosition(80, 31);
                    Console.Write($"                                      ");
                    Console.SetCursorPosition(80, 32);
                    Console.Write($"Invalid input, please try again!");
                }
            }
            Console.SetCursorPosition(80, 30);
            Console.Write($"                                           ");
        }

        private void InitializeNewGame(List<Player> players)
        {
            int gameId = _gameId++;
            foreach (Player player in players)
            {
                player.Hand.Clear();
                if (player.Wallet == 0)
                {
                    // Fix error handling here.
                }
            }
            Console.Clear();
        }

        private void ShowDebugWallets(List<Player> players)
        {
            Console.ResetColor();
            int cachedX = Console.CursorLeft;
            int cachedY = Console.CursorTop;

            int line = 47;
            foreach (Player player in players)
            {
                Console.SetCursorPosition(1, line);
                Console.Write($"{player.Name}: Bet:{player.Bet} Wallet:{player.Wallet}");
                line++;
            }

            Console.SetCursorPosition(cachedX, cachedY);
        }

        private void GameSetup(List<Player> players)
        {
            Thread.Sleep(1500);
            Graphics.AnimateDeckShuffle(Deck.AnimationCards[0]);
            Deck.ShuffleDeck();
            Thread.Sleep(500);
            ShowDebugWallets(players);
            GetPlayerBets(players);
            Deck.FirstDeal(players);
            Utilities.SaveFirstDealInfo(players);
            Graphics.AnimateCardsInAllDirections(Deck.AnimationCards[0], 2, players);
        }

        public static void FunMethod()
        {
            string soundFilePath = "../../../Files/KACHING.WAV";
            if (OperatingSystem.IsWindows())
            {
                SoundPlayer soundPlayer = new SoundPlayer(soundFilePath);
                soundPlayer.Load();
                soundPlayer.Play();
            }
        }
    }
}

