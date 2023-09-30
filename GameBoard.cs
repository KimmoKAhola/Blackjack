using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    /// <summary>
    /// Creates a playing board for the black jack table.
    ///
    /// </summary>
    internal class GameBoard
    {
        public const int windowWidth = 190;
        public const int windowHeight = 40;
        //Card aceOfSpades = new("_____", "|A.   |", "| /.\\ |", "|(_._)|", "|  |  |", "|____V|");

        /// <summary>
        /// Prints out a square with rounded corners.
        /// Hard coded values for window height and width
        /// 50 and 200 on the console
        /// and 40, 190 on the playing board.
        /// </summary>
        public void PrintBoard()
        {
            char line = '─';
            string playingBoard = "╭" + new string(line, windowWidth) + "╮";
            char playingBoardBorder = '│';
            for (int i = 0; i < windowHeight; i++)
            {
                playingBoard += "\n" + playingBoardBorder + new string(' ', windowWidth) + playingBoardBorder;
            }
            playingBoard += "\n" + "╰" + new string(line, windowWidth) + "╯";
            Console.WriteLine(playingBoard);
        }
        /// <summary>
        /// Prints a card at a specified position inside our playing table
        ///
        /// </summary>
        public void PrintCards(Card card)
        {
            card.PrintCard(card);
        }

    }
}
