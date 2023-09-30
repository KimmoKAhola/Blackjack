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
        public BlackJack(PlayingTable table)
        {
            Table = table;
        }

        public PlayingTable Table { get; set; }
        public Player Player { get; }
        public void RunGame(Player playerOne) //skicka in en lista med spelare sen
        {
            //Table.PrintBoard();
            Deck.ShuffleDeck();
            Deck.DealCard();
            Console.ReadKey();
            Deck.DealCard();
            Console.ReadKey();
            Deck.DealCard();
            Console.ReadKey();
            Deck.DealCard();
            Console.ReadKey();
            Deck.DealCard();
            Console.ReadKey();
            Deck.DealCard();
            Console.ReadKey();

            _dealer.PlayerInfo();
            playerOne.PlayerInfo();
            Console.ReadKey();



        }
    }
}
