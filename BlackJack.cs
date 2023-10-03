using System;
using System.Collections.Generic;
using System.Linq;
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
        public Player[] Player { get; }
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
                    //TODO These four lines should go in a separate UpdateBoard() method
                    Graphics.PrintAllPlayerCards(players);
                    Graphics.PrintPlayerInfo(players[currentPlayer]);
                    Graphics.PrintPlayerInfo(players[currentPlayer]);


                    char response = 'ä';
                    Console.Write("Want another card broski?");
                    response = Console.ReadKey().KeyChar;
                    if (response != ' ')
                        break;

                    Deck.DealCard(players[currentPlayer]);
                }
                currentPlayer++;
            }
        }

        private void FirstDeal(List<Player> players)
        {
            foreach (var player in players)
            {
                Deck.DealCard(player);
                Deck.DealCard(player);
            }
        }
    }
}
