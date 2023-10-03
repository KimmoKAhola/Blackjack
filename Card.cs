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
        /// <summary>
        /// Card constructor.
        /// Reads in title, value and graphics
        /// from a separate text file/card enum.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="value"></param>
        /// <param name="cardGraphic"></param>
        public Card(string title, int value, string cardGraphic)
        {
            Title = title;
            Value = value;
            CardGraphic = cardGraphic;
            IsRed = _isRed;
        }
        private bool _isRed;
        public static string[] allCardGraphics = File.ReadAllLines("../../../Files/CardAsciiGraphics.txt");
        public string Title { get; set; }
        public int Value { get; set; }
        public string CardGraphic { get; set; }
        public bool IsRed
        {
            get => _isRed;
            set => _isRed = (Title.Contains("Hearts") || Title.Contains("Diamond"));
        }
    }
}
