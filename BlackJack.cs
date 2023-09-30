using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    public class BlackJack
    {
        public BlackJack(PlayingTable table)
        {
            Table = table;
        }

        public PlayingTable Table { get; set; }

        public void RunGame()
        {
            Table.PrintBoard();
            Deck.ShuffleDeck();
        }
    }
}
