using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    public static class Utilities
    {
        public static List<Player> GetPlayers()
        {
            List<Player> players = new();

            for (int i = 0; i < 3; i++)
            {
                while (true)
                {
                    Console.Clear();
                    Console.SetCursorPosition(75, 20);
                    Console.WriteLine($"Player {i + 1}, Please enter your name and buy-in, seperated by a space");
                    Console.SetCursorPosition(75, 21);
                    Console.Write($"Input: ");

                    string input = Console.ReadLine();
                    string[] values = input.Split(' ');
                    string name = values[0];

                    if (values.Length >= 2 && int.TryParse(values[1], out int buyIn))
                    {
                        players.Add(new(name, buyIn));
                        break;
                    }
                }

            }

            return players;
        }
    }
}
