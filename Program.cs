﻿namespace Blackjack
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WindowHeight = 50;
            Console.WindowWidth = 200;
            Console.CursorVisible = false;

            BlackJack blackjack = new(new Graphics());

            List<Player> allPlayers = new();
            allPlayers.Add(null);
            allPlayers.Add(new Player("Mille"));
            allPlayers.Add(new Player("Kimmo"));

            blackjack.RunGame(allPlayers);
        }
    }
}