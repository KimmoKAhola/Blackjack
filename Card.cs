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
        public Card(string title, int value)
        {
            Title = title;
            Value = value;
        }
        public Card(string title, int value, string cardInfo)
        {
            Title = title;
            Value = value;
            CardInfo = cardInfo;
        }
        public string Title { get; set; }
        public int Value { get; set; }
        public string CardInfo { get; set; }
        private static int _cardWidth = 7;
        /// <summary>
        /// Prints a card at a 
        /// currently at the windowWidth / 2, and windowHeight / 2.
        /// </summary>
        /// <param name="card"></param>
        public void PrintCard(double[] xValues, double[] yValues)
        {
            Console.BackgroundColor = ConsoleColor.White;
            string[] cardArray = new string[6];
            for (int i = 0; i < _cardWidth-1; i++)
            {
                cardArray[i] = CardInfo.Substring(i*_cardWidth, _cardWidth);
            }

            // y position is the height value
            // the xposition has to be chosen accordingly
            for (int xPosition = 0; xPosition < 1; xPosition++)
            {
                Console.SetCursorPosition((int)xValues[xPosition], (int)yValues[3]);
                for (int yPosition = 0; yPosition < _cardWidth-1; yPosition++)
                {
                    Console.SetCursorPosition((int)xValues[xPosition], Console.CursorTop + 1);
                    Console.Write(cardArray[yPosition]);
                }
            }
            Console.BackgroundColor = ConsoleColor.DarkGreen;
        }
    }
}
