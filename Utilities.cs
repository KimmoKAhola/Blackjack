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
                    Console.WriteLine($"PLAYER {i + 1}, PLEASE ENTER YOUR NAME AND BUY-IN, SEPARATED BY A SPACE");
                    Console.SetCursorPosition(75, 21);
                    Console.Write($"INPUT: ");

                    string input = Console.ReadLine();
                    string[] values = input.Split(' ');
                    string name = values[0];

                    if (values.Length >= 2 && int.TryParse(values[1], out int buyIn))
                    {
                        players.Add(new(name.ToUpper(), buyIn));
                        break;
                    }
                }

            }

            return players;
        }

        private static string GetPadding(string input, int spaces)
        {
            spaces -= input.Length;
            string padding = new(' ', spaces);

            string output = input + padding;
            return output;
        }

        public static void DisplayGameSummary(List<Player> players)
        {
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(75, 20);
            Console.Write($"╭───────────────────────────────────────────────────────────────────────╮");
            Console.SetCursorPosition(75, Console.CursorTop + 1);
            Console.Write($"│                              GAME SUMMARY                             │");
            foreach (var player in players)
            {
                Console.SetCursorPosition(75, Console.CursorTop + 1);
                int namePadding = 10;
                int outcomePadding = 10;
                int betPadding = 15;
                int walletPadding = 15;
                string paddedName = GetPadding(player.Name, namePadding);
                string outcome = GetPadding($"[{Enum.GetName(player.GameState)}]", outcomePadding);
                string wallet = GetPadding(player.Wallet.ToString("C2"), walletPadding);

                string betResult;
                if (player.GameState == GameState.BLACKJACK)
                    betResult = $"+{(player.Bet * 2).ToString("C2")}";
                else if (player.GameState == GameState.WIN)
                    betResult = $"+{player.Bet.ToString("C2")}";
                else
                    betResult = $"-{player.Bet.ToString("C2")}";

                betResult = GetPadding(betResult, betPadding);


                Console.Write($"│ {paddedName} {outcome} {betResult} REMAINING FUNDS:{wallet} │");
            }
            Console.SetCursorPosition(75, Console.CursorTop + 1);
            Console.Write($"│                               PLAY AGAIN?                             │");
            Console.SetCursorPosition(75, Console.CursorTop + 1);
            Console.Write($"│                                  Y/N                                  │");
            Console.SetCursorPosition(75, Console.CursorTop + 1);
            Console.Write($"╰───────────────────────────────────────────────────────────────────────╯");

            Console.ResetColor();
        }
    }
}
