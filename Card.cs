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
        public bool IsRed {
            get {  return _isRed; }
            set
            {
                if(Title.Contains("Hearts") || Title.Contains("Diamond"))
                {
                    _isRed = true;
                }
            }
        }
    }
}
