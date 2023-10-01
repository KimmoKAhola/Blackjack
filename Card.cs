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
        ///
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

        public Card(string title, int value)
        {
            Title = title;
            Value = value;
        }

        public string One { get; set; }
        public string Two { get; set; }
        public string Three { get; set; }
        public string Four { get; set; }
        public string Five { get; set; }
        public string Six { get; set; }
        public int Number { get; set; }
        public string Title { get; set; }
        public int Value { get; set; }

        private static int _numberOfRows = 6;
        //private string[] aceOfSpadesArray = new string[_numberOfRows];

        /// <summary>
        /// Prints a card at a 
        /// currently at the windowWidth / 2, and windowHeight / 2.
        /// </summary>
        /// <param name="card"></param>
        public void PrintCard(double[] xValues, double[] yValues)
        {
            Console.BackgroundColor = ConsoleColor.White;
            string[] cardArray = new string[] { One, Two, Three, Four, Five, Six };

            // y position is the height value
            // the xposition has to be chosen accordingly
            for (int xPosition = 0; xPosition < 3; xPosition++)
            {
                Console.SetCursorPosition((int)xValues[xPosition], (int)yValues[3]);
                for (int yPosition = 0; yPosition < _numberOfRows; yPosition++)
                {
                    Console.SetCursorPosition((int)xValues[xPosition], Console.CursorTop + 1);
                    Console.Write(cardArray[yPosition]);
                }
            }

            Console.BackgroundColor = ConsoleColor.DarkGreen;

        }

    }
}
