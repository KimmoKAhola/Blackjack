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

        private static bool firstDeal = true;
        //Player _playerOne = new Player("Kimmo");
        public BlackJack(PlayingTable table)
        {
            Table = table;
        }

        public PlayingTable Table { get; set; }
        public Player Player { get; }
        public void RunGame(Player playerOne) //skicka in en lista med spelare sen
        {
            //Table.PrintBoard();

            Deck.PrintAllCards();
            Console.ReadKey();
            Console.Clear();

            Deck.ShuffleDeck();
            Deck.PrintAllCards();
            Console.ReadKey();
            Console.Clear();


            Deck.DealCard(_dealer, firstDeal);
            //Console.ReadKey();
            Deck.DealCard(playerOne, firstDeal); ;

            _dealer.PlayerInfo();
            Console.ReadKey();

            playerOne.PlayerInfo();
            Console.ReadKey();

            Console.Clear();
            Deck.PrintAllCards();
            Console.ReadKey();
        }
    }
}
