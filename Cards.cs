using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    internal class Cards
    {
        /// <summary>
        /// One card consists of 6 strings and an
        /// int value which is unique for each card.
        /// ex. ace of spades can be int = 1
        /// </summary>
        public Cards(string one, string two, string three, string four, string five, string six, int cardNumber)
        {
            One = one;
            Two = two;
            Three = three;
            Four = four;
            Five = five;
            Six = six;
            cardNumber = 1; // Hårdkodat nu
        }

        public string One { get; set; }
        public string Two { get; set; }
        public string Three { get; set; }
        public string Four { get; set; }
        public string Five { get; set; }
        public string Six { get; set; }

    }
}
