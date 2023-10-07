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
        Dealer _dealer = new Dealer();
        public BlackJack(Graphics table)
        {
            Table = table;
        }

        //TODO Add loop so that each keypress deals a new card
        public Graphics Table { get; set; }
        public void RunGame(List<Player> players) //skicka in en lista med spelare sen
        {
            Table.PrintBoard();

            Thread.Sleep(500);
            int co = 2;
            while (co > 0)
            {
                Graphics.PrintAStackOfCards(Deck.AnimationCards[0]);
                Graphics.ShuffleAnimationForASingleCard(Deck.AnimationCards[0], 12);
                co--;
            }
            Deck.ShuffleDeck();
            Deck.FirstDeal(players, _dealer);
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

            while (players[0].HandSum() < 17)
            {
                if (players[0].HandSum() < 17)
                {
                    Deck.DealCard(players[0]);
                    Graphics.PrintAllPlayerCards(_dealer);
                    Graphics.PrintAllPlayerCards(players);
                }


                //else if (players[0].HandSum() <= 21)
                //{
                //    for (int i = 1; i < players.Count; i++)
                //    {
                //        if (players[i].HandSum() <= players[0].HandSum())
                //        {
                //            players[i].Wallet = 0;
                //        }
                //        else
                //        {
                //            players[i].Wallet *= 2;
                //        }
                //    }
                //}
                //else
                //{
                //    for (int i = 1; i < players.Count; i++)
                //    {
                //        if (players[i].HandSum() <= 21)
                //        {
                //            players[i].Wallet *= 2;
                //        }
                //    }
                //}
            }

            CheckResults(players);
        }
        private static void CheckResults(List<Player> players)
        {

        }
    }
}
