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

        private static bool _firstDeal = true;
        //Player _playerOne = new Player("Kimmo");
        public BlackJack(Graphics table)
        {
            Table = table;
        }

        public Graphics Table { get; set; }
        public Player Player { get; }
        public void RunGame(Player playerOne) //skicka in en lista med spelare sen
        {
            Table.PrintBoard();
            Deck.ShuffleDeck();
            Deck.DealCard(_dealer, _firstDeal);
            Deck.DealCard(playerOne, _firstDeal);


            Graphics.PrintCard(_dealer);
            Graphics.PrintCard(playerOne);
            
            _dealer.PlayerInfo();
            playerOne.PlayerInfo();
            Console.ReadKey();
        }
    }
}
