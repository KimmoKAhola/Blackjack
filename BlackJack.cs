using System;
using System.Collections.Generic;
using System.Linq;
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
            ShowDebugWallets(players);
            Deck.FirstDeal(players, Dealer);
            int currentPlayer = 0;
            while (currentPlayer < players.Count)
            {
                while (true)
                {

                    Graphics.UpdateBoard(players, currentPlayer);
                    Graphics.PrintPlayerTitleAndSum(players[currentPlayer]);

                    if (GameLogic.CheckForBlackJack(players[currentPlayer]))
                    {
                        players[currentPlayer].GameState = GameState.BlackJack;
                        break;
                    }

                    if (GameLogic.CheckForBust(players[currentPlayer]))
                    {
                        players[currentPlayer].GameState = GameState.Loss;
                        break;
                    }

                    char response;
                    Console.Write("Want another card broski?");
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
                if (Dealer.HandSum() < 17)
                {
                    Deck.DealCard(Dealer);
                }
                Graphics.UpdateBoard(Dealer);
                Thread.Sleep(1000);
            }

            CheckResults(players);

            foreach (Player player in players)
            {
                player.UpdateWallet();
            }
            //TODO add a prompt here. Continue? J/N
            char response2 = Console.ReadKey().KeyChar;
            if (response2 != ' ')
            {
                Environment.Exit(0); //TODO remove later.
            }
            RunGame(players);
        }
        private void CheckResults(List<Player> players)
        {
            foreach (var player in players)
            {
                if (player.GameState == GameState.BlackJack)
                {
                    //Player got blackjack in first deal
                }
                else if (player.HandSum() > Dealer.HandSum() && player.GameState != GameState.Loss)
                {
                    player.GameState = GameState.Win;
                }
                else if (player.GameState != GameState.Loss && Dealer.HandSum() > 21)
                {
                    player.GameState = GameState.Win;
                }
                else
                    player.GameState = GameState.Loss;
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
                    Console.Write($"Bet: ");
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
            Graphics.AnimateCardsInAllDirections(Deck.AnimationCards[0], 2);
            Thread.Sleep(500);
            GetPlayerBets(players);
            for (int i = 0; i < 2; i++)
            {
                Graphics.EraseAPrintedCard(192 - 7 / 2 * i, 18);
                Graphics.EraseAPrintedCard(107 + 7 / 2 * i, 39);
                Graphics.EraseAPrintedCard(13 + 7 / 2 * i, 18);
            }
        }
    }
}

