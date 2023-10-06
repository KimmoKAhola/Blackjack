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
        Player _dealer = new Player("William");

        //Player _playerOne = new Player("Kimmo");
        public BlackJack(Graphics table)
        {
            Table = table;
        }

        //TODO Add loop so that each keypress deals a new card
        public Graphics Table { get; set; }
        //public Player[] Player { get; }
        public void RunGame(List<Player> players) //skicka in en lista med spelare sen
        {
            players[0] = _dealer;
            Table.PrintBoard();
            Deck.ShuffleDeck();
            FirstDeal(players);
            int currentPlayer = 1;
            while (currentPlayer <= players.Count())
            {
                while (true)
                {
                    bool blackJack = false;
                    //TODO These four lines should go in a separate UpdateBoard() method
                    Graphics.PrintAllPlayerCards(players);
                    Graphics.PrintPlayerInfo(players[currentPlayer]);
                    Graphics.PrintPlayerInfo(players[currentPlayer]);

                    if (GameLogic.CheckFor21(players[currentPlayer], out blackJack))
                    {
                        if (blackJack)
                        {
                            players[currentPlayer].Wallet *= 3;
                        }
                        break;
                    }

                    if (GameLogic.CheckForBust(players[currentPlayer]))
                    {
                        players[currentPlayer].Wallet = 0;
                        break;
                    }

                    char response = 'ä';
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
                }
                else if (players[0].HandSum() <= 21)
                {
                    for (int i = 1; i < players.Count(); i++)
                    {
                        if (players[i].HandSum() <= players[0].HandSum())
                        {
                            players[i].Wallet = 0;
                        }
                        else
                        {
                            players[i].Wallet *= 2;
                        }
                    }
                }
                else
                {
                    for (int i = 1; i < players.Count(); i++)
                    {
                        if (players[i].HandSum() <= 21)
                        {
                            players[i].Wallet *= 2;
                        }
                    }
                }
            }
        }

        private void FirstDeal(List<Player> players)
        {
            for (int i = 0; i < 2; i++)
            {
                for (int j = 1; j < players.Count; j++)
                {
                    Deck.DealCard(players[j]);
                }
                Deck.DealCard(_dealer);
            }
        }
    }
}
