using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    public static class Utilities
    {
        public static List<Player> GetPlayers()
        {
            List<Player> players = new();
            Console.CursorVisible = true;
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
                        if (buyIn > 0)
                        {
                            players.Add(new(name.ToUpper(), buyIn));
                            break;
                        }
                    }
                }

            }
            Console.CursorVisible = false;
            return players;
        }

        private static string GetPadding(string input, int spaces)
        {
            spaces -= input.Length;
            string padding = new(' ', spaces);

            string output = input + padding;
            return output;
        }
        private static string GetCenteredPadding(string input, int spaces)
        {
            spaces -= input.Length;
            string firstPadding = new(' ', spaces / 2);

            if (input.Length % 2 == 0)
            {
                spaces++;
            }

            string secondPadding = new(' ', spaces / 2);

            string output = firstPadding + input + secondPadding;
            return output;
        }


        public static void DisplayGameSummary(List<Player> players)
        {
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(65, 20);
            Console.Write($"╭───────────────────────────────────────────────────────────────────────╮");
            Console.SetCursorPosition(65, Console.CursorTop + 1);
            Console.Write($"│                              GAME SUMMARY                             │");
            foreach (var player in players)
            {
                Console.SetCursorPosition(65, Console.CursorTop + 1);
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

            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.ForegroundColor = ConsoleColor.DarkGreen;
        }

        public static void PromptPlayer(Player player)
        {
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(65, 30);
            Console.Write($"╭───────────────────────────────────────────────────────────────────────╮");
            Console.SetCursorPosition(65, Console.CursorTop + 1);
            string paddedPlayerPrompt = GetCenteredPadding($"{player.Name}'S TURN", 71);
            Console.Write($"│{paddedPlayerPrompt}│");
            Console.SetCursorPosition(65, Console.CursorTop + 1);
            Console.Write($"│                 PRESS <SPACE> to HIT or <S> to STAND                  │");
            Console.SetCursorPosition(65, Console.CursorTop + 1);
            Console.Write($"╰───────────────────────────────────────────────────────────────────────╯");

            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.ForegroundColor = ConsoleColor.DarkGreen;
        }

        public static void SavePlayerAction(Player player)
        {
            string cardSymbol = player.Hand.Last().CardSymbol;
            string lastCard = player.Hand.Last().Title;
            int cardSum = player.HandSum();
            string playerName = player.Name.ToUpper();

            string completePlayerHand = $"";
            for (int playerHandIndex = 0; playerHandIndex < player.Hand.Count; playerHandIndex++)
            {
                completePlayerHand += player.Hand[playerHandIndex].Title + player.Hand[playerHandIndex].CardSymbol + ", ";
            }
            completePlayerHand = "[" + completePlayerHand.TrimEnd(',', ' ') + "]";

            if (player.LatestAction == 0)
            {
                completePlayerHand = playerName + "'s starting hand\n" + completePlayerHand; //This is the first deal
            }
            if (player.LatestAction == PlayerAction.HIT)
            {
                completePlayerHand = player.LatestAction.ToString() + $" > {lastCard}{cardSymbol}" + "\n" + completePlayerHand;
            }
            if (player.LatestAction == PlayerAction.STAND)
            {
                completePlayerHand = PlayerAction.STAND.ToString() + " - SUM [" + player.HandSum() + "]";
            }
            if (player.LatestAction == PlayerAction.BUST)
            {
                completePlayerHand = PlayerAction.BUST.ToString() + " - SUM [" + player.HandSum() + "]";
            }
            FileManager.SaveHandInfo(completePlayerHand);
        }

        public static void SaveFirstDealInfo(List<Player> players)
        {
            string temp = "FIRST DEAL TEMP FIX LATER";

            FileManager.SaveHandInfo(temp);
        }
    }
}
