using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    public class Card
    {
        public Card()
        {
            CardGraphicWhileMoving = allCardGraphics[52];
            LatestCardPosition = (101, 18);
        }
        /// <summary>
        /// Card constructor.
        /// Reads in title, value and graphics
        /// from a separate text file/card enum.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="value"></param>
        /// <param name="cardGraphic"></param>
        public Card(string title, int value, string cardGraphic, string cardSymbol)
        {
            Title = title;
            Value = value;
            CardGraphic = cardGraphic;
            IsRed = false;
            CardSymbol = cardSymbol;
            CardGraphicWhileMoving = allCardGraphics[52];
        }

        public static string[] allCardGraphics = File.ReadAllLines("../../../Files/CardAsciiGraphics.txt");
        public string Title { get; set; }
        public int Value { get; set; }
        public string CardGraphic { get; set; }
        public string CardGraphicWhileMoving { get; }
        public bool IsRed {get; set;}
        public string CardSymbol { get; set; }
        public (int LatestXPosition, int LatestYPosition) LatestCardPosition { get; set; }
    }
}
