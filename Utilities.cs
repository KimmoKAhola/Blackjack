namespace Blackjack
{
    /// <summary>
    /// It's a mess and it was William's idea ;)
    /// this is not SOLID!
    /// </summary>
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
            SetConsoleColors("", "DC");
            List<Player> players = new();
            Console.CursorVisible = true;
            for (int i = 0; i < 3; i++)
            {
                while (true)
                {
                    Console.Clear();
                    string[] prompt =
                    {
                        $"PLAYER {i + 1}, PLEASE ENTER YOUR NAME AND BUY-IN, SEPARATED BY A SPACE",
                        $"INPUT: "
                    };

                    PrintCenteredAlignedStringArray(prompt, 20);

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
            int yStartPosition = 20;
            int width = 73;
            string[] startPrompt =
            {
                $"╭───────────────────────────────────────────────────────────────────────╮",
                $"│                              GAME SUMMARY                             │"
            };
            string[] endPrompt =
            {
                $"│                               PLAY AGAIN?                             │",
                $"│                                  Y/N                                  │",
                $"╰───────────────────────────────────────────────────────────────────────╯"
            };

            PrintCenteredStringArray(startPrompt, yStartPosition);
            yStartPosition += 2;

            foreach (var player in players)
            {
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

                    string playerSummary = $"│ {paddedName} {outcome} {betResult} REMAINING FUNDS:{wallet} │";
                    PrintCenteredString(playerSummary, yStartPosition);
                    yStartPosition++;
                }
            }
            PrintCenteredStringArray(endPrompt, yStartPosition);

            SetConsoleColors("DG", "DG");
        }

        public static void PromptPlayerMove(Player player, out int promptWidth, out int yPosition)
        {
            SetConsoleColors("B", "G");
            promptWidth = 71;
            yPosition = 30;

            string[] prompt =
            {
                $"╭───────────────────────────────────────────────────────────────────────╮",
                $"|{GetCenteredPadding($"{player.Name}'S TURN", promptWidth)}|",
                $"│                 PRESS <SPACE> to HIT or <S> to STAND                  │",
                $"╰───────────────────────────────────────────────────────────────────────╯"
            };

            PrintCenteredStringArray(prompt, yPosition);
            SetConsoleColors("DG", "DG");
        }
        public static void PromptPlayerSplit(Player player, out int promptWidth, out int yPosition)
        {
            SetConsoleColors("B", "G");
            promptWidth = 71;
            yPosition = 30;

            string[] prompt =
            {
                $"╭───────────────────────────────────────────────────────────────────────╮",
                $"|{GetCenteredPadding($"{player.Name} HAS A SPLITTABLE HAND!", 71)}|",
                $"│                 Do you wish to split? PRESS <y> or <N>                │",
                $"╰───────────────────────────────────────────────────────────────────────╯"
            };

            PrintCenteredStringArray(prompt, 30);
            SetConsoleColors("DG", "DG");
        }
        public static int PromptPlayerBet(Player player, ref int cachedPromptWidth)
        {
            Utilities.SetConsoleColors("Y", "DG");

            int yPosition = 30;
            string prompt = $"Player {player.Name}, please enter your bet";
            string betPrompt = $"BET: ";
            string errorMessage = $"Invalid input, please try again";

            if (cachedPromptWidth < prompt.Length)
                cachedPromptWidth = prompt.Length;

            string clearLine = new(' ', cachedPromptWidth);

            string[] clearLines = { clearLine, clearLine };
            string[] promptLines = { prompt, betPrompt };

            while (true)
            {
                PrintCenteredAlignedStringArray(clearLines, yPosition);
                PrintCenteredAlignedStringArray(promptLines, yPosition);

                if (int.TryParse(Console.ReadLine(), out int betInput))
                {
                    if (betInput <= player.Wallet)
                    {
                        string[] erasePrompt = { clearLine, clearLine, clearLine };
                        PrintCenteredAlignedStringArray(erasePrompt, yPosition);
                        return betInput;
                    }
                }
                yPosition += 2;
                PrintCenteredString(errorMessage, yPosition);
                yPosition -= 2;
            }
        }
        public static void ErasePrompt(int promptWindowWidth, int yPosition)
        {
            SetConsoleColors("DG", "DG");

            string eraseLine = new(' ', promptWindowWidth);

            for (int i = 0; i < 4; i++)
            {
                PrintCenteredString(eraseLine, yPosition);
                yPosition++;
            }
        }

        public static void PrintCenteredStringArray(string[] promptLines, int yPosition)
        {
            foreach (var line in promptLines)
            {
                CenterStringCursorToWindow(line, yPosition);
                Console.Write(line);
                yPosition++;
            }
        }
        public static void PrintCenteredString(string promptLine, int yPosition)
        {
            CenterStringCursorToWindow(promptLine, yPosition);
            Console.Write(promptLine);
        }
        public static void PrintCenteredAlignedStringArray(string[] promptLines, int yPosition)
        {
            CenterStringCursorToWindow(promptLines[0], yPosition);
            int xPosition = Console.CursorLeft;
            foreach (var line in promptLines)
            {
                Console.SetCursorPosition(xPosition, yPosition);
                Console.Write(line);
                yPosition++;
            }
        }
        public static void CenterStringCursorToWindow(string input, int yPosition)
        {
            int centeredStringPosition = (Console.WindowWidth - input.Length) / 2;
            Console.SetCursorPosition(centeredStringPosition, yPosition);
        }

        public static void SavePlayerAction(Player player)
        {
            Hand hand = player.CurrentHand;
            string cardSymbol = hand.CurrentCards.Last().CardSymbol;
            string lastCard = hand.CurrentCards.Last().Title;
            int cardSum = hand.HandSum();
            string playerName = player.Name.ToUpper();

            string completePlayerHand = $"";
            for (int playerHandIndex = 0; playerHandIndex < hand.CurrentCards.Count; playerHandIndex++)
            {
                completePlayerHand += hand.CurrentCards[playerHandIndex].Title + hand.CurrentCards[playerHandIndex].CardSymbol + ", ";
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
            if (player.CurrentHand.HandState == HandState.BUST)
            {
                completePlayerHand = HandState.BUST.ToString() + " - SUM [" + hand.HandSum() + "]";
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
            string cardSymbol = currentHand.CurrentCards.Last().CardSymbol;
            string lastCard = currentHand.CurrentCards.Last().Title;
            int cardSum = currentHand.HandSum();
            string playerName = player.Name.ToUpper();

            string handInfo = $"{playerName} was dealt a [{lastCard}{cardSymbol}], their hand is now worth {cardSum}";
            log.Add(handInfo);
        }
        public static void LogDealerInfo()
        {
            string cardSymbol = Dealer.Instance.Hand.CurrentCards.Last().CardSymbol;
            string lastCard = Dealer.Instance.Hand.CurrentCards.Last().Title;
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
            else if (foreground.ToUpper() == "GR")
                Console.ForegroundColor = ConsoleColor.Green;
            else if (foreground.ToUpper() == "Y")
                Console.ForegroundColor = ConsoleColor.Yellow;
            else if (foreground.ToUpper() == "C")
                Console.ForegroundColor = ConsoleColor.Cyan;
            else if (foreground.ToUpper() == "DR")
                Console.ForegroundColor = ConsoleColor.DarkRed;
            else if (foreground.ToUpper() == "DB")
                Console.ForegroundColor = ConsoleColor.DarkBlue;
            else if (foreground.ToUpper() == "DG")
                Console.ForegroundColor = ConsoleColor.DarkGreen;
            else if (foreground.ToUpper() == "DC")
                Console.ForegroundColor = ConsoleColor.DarkCyan;

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
            else if (background.ToUpper() == "GR")
                Console.BackgroundColor = ConsoleColor.Green;
            else if (background.ToUpper() == "Y")
                Console.BackgroundColor = ConsoleColor.Yellow;
            else if (background.ToUpper() == "C")
                Console.BackgroundColor = ConsoleColor.Cyan;
            else if (background.ToUpper() == "DR")
                Console.BackgroundColor = ConsoleColor.DarkRed;
            else if (background.ToUpper() == "DB")
                Console.BackgroundColor = ConsoleColor.DarkBlue;
            else if (background.ToUpper() == "DG")
                Console.BackgroundColor = ConsoleColor.DarkGreen;
            else if (background.ToUpper() == "DC")
                Console.BackgroundColor = ConsoleColor.DarkCyan;
        }
    }
}
