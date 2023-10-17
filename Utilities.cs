﻿namespace Blackjack
{
    public static class Utilities
    {
        private static ConsoleColor _cachedForegroundColor = ConsoleColor.White;
        private static ConsoleColor _cachedBackgroundColor = ConsoleColor.White;
        public static List<string> log = new List<string>()
        {
            "",
            "",
            "",
            "",
            "",
            ""
        };
        public static List<Player> GetPlayers()
        {
            List<Player> players = new();
            Console.CursorVisible = true;
            for (int i = 0; i < 3; i++)
            {
                while (true)
                {
                    Console.Clear();
                    string prompt = $"PLAYER {i + 1}, PLEASE ENTER YOUR NAME AND BUY-IN, SEPARATED BY A SPACE";
                    Console.SetCursorPosition(CenterStringToWindow(prompt), 20);
                    Console.WriteLine(prompt);
                    Console.SetCursorPosition(CenterStringToWindow(prompt), Console.CursorTop + 1);
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
            SetConsoleColors("B", "G");

            string width = "                                                                         ";
            Console.SetCursorPosition(CenterStringToWindow(width), 20);

            Console.Write($"╭───────────────────────────────────────────────────────────────────────╮");
            Console.SetCursorPosition(CenterStringToWindow(width), Console.CursorTop + 1);
            Console.Write($"│                              GAME SUMMARY                             │");
            foreach (var player in players)
            {
                Console.SetCursorPosition(CenterStringToWindow(width), Console.CursorTop + 1);
                int namePadding = 10;
                int outcomePadding = 10;
                int betPadding = 15;
                int walletPadding = 15;
                foreach (var hand in player.Hands)
                {
                    string paddedName = GetPadding(player.Name, namePadding);
                    string outcome = GetPadding($"[{Enum.GetName(hand.HandState)}]", outcomePadding);
                    string wallet = GetPadding(player.Wallet.ToString("C2"), walletPadding);

                    string betResult;
                    if (hand.HandState == HandState.BLACKJACK)
                        betResult = $"+{(hand.Bet * 2).ToString("C2")}";
                    else if (hand.HandState == HandState.WIN)
                        betResult = $"+{hand.Bet.ToString("C2")}";
                    else
                        betResult = $"-{hand.Bet.ToString("C2")}";

                    betResult = GetPadding(betResult, betPadding);


                    Console.Write($"│ {paddedName} {outcome} {betResult} REMAINING FUNDS:{wallet} │");
                }
            }
            Console.SetCursorPosition(CenterStringToWindow(width), Console.CursorTop + 1);
            Console.Write($"│                               PLAY AGAIN?                             │");
            Console.SetCursorPosition(CenterStringToWindow(width), Console.CursorTop + 1);
            Console.Write($"│                                  Y/N                                  │");
            Console.SetCursorPosition(CenterStringToWindow(width), Console.CursorTop + 1);
            Console.Write($"╰───────────────────────────────────────────────────────────────────────╯");

            SetConsoleColors("DG", "DG");
        }

        public static void PromptPlayer(Player player)
        {
            SetConsoleColors("B", "G");

            string widthString = "                                                                         ";
            Console.SetCursorPosition(CenterStringToWindow(widthString), 30);

            Console.Write($"╭───────────────────────────────────────────────────────────────────────╮");
            Console.SetCursorPosition(CenterStringToWindow(widthString), Console.CursorTop + 1);
            string paddedPlayerPrompt = GetCenteredPadding($"{player.Name}'S TURN", widthString.Length);
            Console.Write($"│{paddedPlayerPrompt}│");
            Console.SetCursorPosition(CenterStringToWindow(widthString), Console.CursorTop + 1);
            Console.Write($"│                 PRESS <SPACE> to HIT or <S> to STAND                  │");
            Console.SetCursorPosition(CenterStringToWindow(widthString), Console.CursorTop + 1);
            Console.Write($"╰───────────────────────────────────────────────────────────────────────╯");

            SetConsoleColors("DG", "DG");

        }

        public static void ErasePrompt()
        {
            SetConsoleColors("DG", "DG");

            Console.SetCursorPosition(65, 30);
            Console.Write($"╭───────────────────────────────────────────────────────────────────────╮");
            Console.SetCursorPosition(65, Console.CursorTop + 1);
            string paddedPlayerPrompt = ($"│                 PRESS <SPACE> to HIT or <S> to STAND                  │");
            Console.Write(paddedPlayerPrompt);
            Console.SetCursorPosition(65, Console.CursorTop + 1);
            Console.Write($"│                 PRESS <SPACE> to HIT or <S> to STAND                  │");
            Console.SetCursorPosition(65, Console.CursorTop + 1);
            Console.Write($"╰───────────────────────────────────────────────────────────────────────╯");

            SetConsoleColors("DG", "DG");
        }
        public static void PromptPlayerSplit(Player player)
        {
            SetConsoleColors("B", "G");

            Console.SetCursorPosition(65, 30);
            Console.Write($"╭───────────────────────────────────────────────────────────────────────╮");
            Console.SetCursorPosition(65, Console.CursorTop + 1);
            string paddedPlayerPrompt = GetCenteredPadding($"{player.Name} HAS A SPLITTABLE HAND!", 71);
            Console.Write($"│{paddedPlayerPrompt}│");
            Console.SetCursorPosition(65, Console.CursorTop + 1);
            Console.Write($"│                 Do you wish to split? PRESS <y> or <N>                │");
            Console.SetCursorPosition(65, Console.CursorTop + 1);
            Console.Write($"╰───────────────────────────────────────────────────────────────────────╯");

            SetConsoleColors("DG", "DG");
        }

        public static void SavePlayerAction(Player player, Hand hand)
        {
            string cardSymbol = hand.Cards.Last().CardSymbol;
            string lastCard = hand.Cards.Last().Title;
            int cardSum = hand.HandSum();
            string playerName = player.Name.ToUpper();

            string completePlayerHand = $"";
            for (int playerHandIndex = 0; playerHandIndex < hand.Cards.Count; playerHandIndex++)
            {
                completePlayerHand += hand.Cards[playerHandIndex].Title + hand.Cards[playerHandIndex].CardSymbol + ", ";
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
                completePlayerHand = PlayerAction.STAND.ToString() + " - SUM [" + hand.HandSum() + "]";
            }
            if (player.LatestAction == PlayerAction.BUST)
            {
                completePlayerHand = PlayerAction.BUST.ToString() + " - SUM [" + hand.HandSum() + "]";
            }
            FileManager.SaveHandInfo(completePlayerHand);
        }
        public static void SaveFirstDealInfo(List<Player> players)
        {
            string temp = "FIRST DEAL TEMP FIX LATER";

            FileManager.SaveHandInfo(temp);
        }
        public static void LogPlayerInfo(Player player, Hand currentHand)
        {
            string cardSymbol = currentHand.Cards.Last().CardSymbol;
            string lastCard = currentHand.Cards.Last().Title;
            int cardSum = currentHand.HandSum();
            string playerName = player.Name.ToUpper();

            string handInfo = $"{playerName} was dealt a [{lastCard}{cardSymbol}], their hand is now worth {cardSum}";
            log.Add(handInfo);
        }
        public static void LogDealerInfo()
        {
            string cardSymbol = Dealer.Instance.Hand.Cards.Last().CardSymbol;
            string lastCard = Dealer.Instance.Hand.Cards.Last().Title;
            int cardSum = Dealer.Instance.Hand.HandSum();

            string handInfo = $"The dealer was dealt a [{lastCard}{cardSymbol}], their hand is now worth {cardSum}";

            log.Add(handInfo);
            //FileManager.SaveHandInfo(handInfo);
        }

        public static void SetConsoleColors(string foreground, string background)
        {
            if (foreground.ToUpper() == "SETCACHED")
                _cachedForegroundColor = Console.ForegroundColor;
            else if (foreground.ToUpper() == "GETCACHED")
                Console.ForegroundColor = _cachedForegroundColor;

            else if (foreground.ToUpper() == "W")
                Console.ForegroundColor = ConsoleColor.White;
            else if (foreground.ToUpper() == "B")
                Console.ForegroundColor = ConsoleColor.Black;
            else if (foreground.ToUpper() == "G")
                Console.ForegroundColor = ConsoleColor.Gray;
            else if (foreground.ToUpper() == "Y")
                Console.ForegroundColor = ConsoleColor.Yellow;
            else if (foreground.ToUpper() == "DR")
                Console.ForegroundColor = ConsoleColor.DarkRed;
            else if (foreground.ToUpper() == "DB")
                Console.ForegroundColor = ConsoleColor.DarkBlue;
            else if (foreground.ToUpper() == "DG")
                Console.ForegroundColor = ConsoleColor.DarkGreen;

            if (background.ToUpper() == "SETCACHED")
                _cachedBackgroundColor = Console.BackgroundColor;
            else if (background.ToUpper() == "GETCACHED")
                Console.BackgroundColor = _cachedBackgroundColor;

            else if (background.ToUpper() == "W")
                Console.BackgroundColor = ConsoleColor.White;
            else if (background.ToUpper() == "B")
                Console.BackgroundColor = ConsoleColor.Black;
            else if (background.ToUpper() == "G")
                Console.BackgroundColor = ConsoleColor.Gray;
            else if (background.ToUpper() == "Y")
                Console.BackgroundColor = ConsoleColor.Yellow;
            else if (background.ToUpper() == "DR")
                Console.BackgroundColor = ConsoleColor.DarkRed;
            else if (background.ToUpper() == "DB")
                Console.BackgroundColor = ConsoleColor.DarkBlue;
            else if (background.ToUpper() == "DG")
                Console.BackgroundColor = ConsoleColor.DarkGreen;
        }
        public static int CenterStringToWindow(string input)
        {
            int centeredStringPosition = (Console.WindowWidth - input.Length) / 2;

            return centeredStringPosition;
        }
    }
}
