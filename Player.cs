using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    public class Player
    {
        private static int playerCounter = 0;
        public Player(string name)
        {
            Name = name;
            PlayerNumber = playerCounter++;
            PlayerHand = new List<Card>();
            //PlayerHand = playerHand;
        }

        public string Name { get; set; }
        public int PlayerNumber { get; set; }
        public List<Card> PlayerHand { get; set; }


        //public void PlayerInfo()
        //{
        //    Console.WriteLine($"The player {Name}, with id {PlayerNumber}, has the hand ");
        //    Console.Write("[");
        //    foreach (var card in PlayerHand)
        //    {
        //        Console.Write($"{card.Title} {card.Value}, ");
        //    }
        //    Console.Write("]\n");
        //}
        public string PlayerInfo()
        {
            string info = $"The player {Name}, with id {PlayerNumber}, has the hand";
            Console.WriteLine(info);
            foreach (Card card in PlayerHand)
            {
                Console.Write($"[{card.Title} {card.Value}] ");
            }
            return info;
        }
    }
}
