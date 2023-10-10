using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    public class BlackJack
    {
        private static int _gameId = 0;

        public int GameId { get { return _gameId; } }
        public Dealer Dealer { get; set; }
        public void RunGame(List<Player> players) //skicka in en lista med spelare sen
        {
            InitializeNewGame(players);
            //Console.CursorVisible = true;
            FileManager.SaveStartTimeStamp(GameId);
            Graphics.PrintBoard();
            GameSetup(players);

            int currentPlayer = 0;
            while (currentPlayer < players.Count)
            {
                while (true)
                {

                    Graphics.UpdateBoard(players, currentPlayer);
                    Graphics.PrintPlayerTitleAndSum(players[currentPlayer]);

                    if (GameLogic.CheckForBlackJack(players[currentPlayer]))
                    {
                        players[currentPlayer].GameState = GameState.BLACKJACK;
                        break;
                    }

                    if (GameLogic.CheckForBust(players[currentPlayer]))
                    {
                        //players[currentPlayer].GameState = GameState.LOSS;
                        break;
                    }

                    char response;
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    response = Console.ReadKey().KeyChar;
                    Console.ForegroundColor = ConsoleColor.Yellow;

                    if (response != ' ')
                        break;

                    Deck.DealCard(players[currentPlayer]);
                }
                Graphics.PrintPlayerTitleAndSum(players[currentPlayer]);
                currentPlayer++;
            }

            //Erase the dealer's card
            Graphics.EraseAPrintedCard(107, 0);
            Graphics.UpdateBoard(Dealer);
            //Dramatic pause
            Thread.Sleep(1000);
            // DEALER LOOP
            while (Dealer.HandSum() < 17)
            {
                Deck.DealCard(Dealer);

                Graphics.UpdateBoard(Dealer);
                Thread.Sleep(1000);
            }

            CheckResults(players);

            foreach (Player player in players)
            {
                player.UpdateWallet();
            }

            Utilities.DisplayGameSummary(players);

            char response2 = Console.ReadKey().KeyChar;
            if (response2 == 'n' && response2 == 'N')
            {
                Environment.Exit(0); //TODO remove later.
            }
            RunGame(players);
        }
        private void CheckResults(List<Player> players)
        {
            foreach (var player in players)
            {
                if (player.GameState == GameState.BLACKJACK)
                {
                    //Player got blackjack in first deal
                    FunMethod();
                }
                else if (player.HandSum() > 21)
                {
                    player.GameState = GameState.LOSS;
                }
                else if (Dealer.HandSum() > 21)
                {
                    FunMethod();
                    player.GameState = GameState.WIN;
                }
                else if (player.HandSum() > Dealer.HandSum())
                {
                    player.GameState = GameState.WIN;
                    FunMethod();
                }
                else
                    player.GameState = GameState.LOSS;
            }
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
            Dealer = new();
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
            Graphics.AnimateCardsInAllDirections(Deck.AnimationCards[0], 2, players);
            Deck.FirstDeal(players, Dealer);
        }

        private void FunMethod()
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

