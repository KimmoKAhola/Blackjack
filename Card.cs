using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    public class Card
    {
        /// <summary>
        /// One card consists of 6 strings and an
        /// int value which is unique for each card.
        /// ex. ace of spades can be int = 1
        /// </summary>
        public Card(string one, string two, string three, string four, string five, string six, int number)
        {
            One = one;
            Two = two;
            Three = three;
            Four = four;
            Five = five;
            Six = six;
            Number = 1; // Hårdkodat nu
        }
        public Card(string one, string two, string three, string four, string five, string six)
        {
            //One to Six = new array[6]
            One = one;
            Two = two;
            Three = three;
            Four = four;
            Five = five;
            Six = six;
            Number = 1; // Hårdkodat nu
        }

        public Card(int number, string title)
        {
            Number = number;
            Title = title;
        }

        public string One { get; set; }
        public string Two { get; set; }
        public string Three { get; set; }
        public string Four { get; set; }
        public string Five { get; set; }
        public string Six { get; set; }
        public int Number { get; set; }
        public string Title { get; set; }

        private static int _numberOfRows = 6;
        //private string[] aceOfSpadesArray = new string[_numberOfRows];

        /// <summary>
        /// Prints a card at a hard coded position
        /// currently at the windowWidth / 2, and windowHeight / 2.
        /// </summary>
        /// <param name="card"></param>
        public void PrintCard(Card card)
        {
            string[] cardArray = new string[] {One, Two, Three, Four, Five, Six };
            Console.SetCursorPosition(GameBoard.windowWidth / 2, GameBoard.windowHeight / 2);
            for (int i = 0; i < _numberOfRows; i++)
            {
                Console.SetCursorPosition(GameBoard.windowWidth/2, Console.CursorTop + 1);
                Console.Write(cardArray[i]);
            }
        }

    }
}
