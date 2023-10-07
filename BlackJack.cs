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
        BlackJackGameHistory gameHistory;
        public Dealer Dealer { get; set; }
        public Graphics Table { get; set; }
        public BlackJackGameHistory GameHistory { get; set; }
        //public List<BlackJackGameHistory> GameHistoryList { get; set; }
        public void RunGame(List<Player> players) //skicka in en lista med spelare sen
        {
            InitializeNewGame(players);
            //Blackjack game id 1 [tidpunkt för start]: spelare: .... bettade .... saldo....
            //Vilka kort som gavs till vilken spelare [K, E] > [K, E, 10] > [K, E, 10, 5]
            //Vilka kort som gavs till dealern
            //GameState för varje spelare och eventuell vinst
            //Sluttid
            FileManager.SaveGameInfo(new BlackJackGameHistory(players));
            Table.PrintBoard();
            
            // TODO WHAT THE FUCK IS THIS!?
            Thread.Sleep(500);
            int co = 2;
            while (co > 0)
            {
                Graphics.PrintAStackOfCards(Deck.AnimationCards[0]);
                Graphics.ShuffleAnimationForASingleCard(Deck.AnimationCards[0], 12);
                co--;
            }

            GetPlayerBets(players);
            ShowDebugWallets(players);
            Deck.ShuffleDeck();
            Deck.FirstDeal(players, Dealer);
            Graphics.PrintAllPlayerCards(players);

            Console.ReadKey();
            int currentPlayer = 0;
            while (currentPlayer < players.Count)
            {
                while (true)
                {

                    Graphics.UpdateBoard(players, currentPlayer);

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
                    response = Console.ReadKey().KeyChar;
                    if (response != ' ')
                        break;

                    Deck.DealCard(players[currentPlayer]);
                }
                currentPlayer++;
            }

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

            foreach (Player player in players )
            {
                player.UpdateWallet();
            }

            Console.ReadKey();
            GameHistory = new(players);
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
            Dealer = new();
            Table = new();
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
    }
}
