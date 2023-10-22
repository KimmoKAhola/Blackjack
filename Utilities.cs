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
            Utilities.ToggleCursorVisibility();
            List<Player> players = new();
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
            ToggleCursorVisibility();
            return players;
        }
        public static string GetPadding(string input, int spaces)
        {
            spaces -= input.Length;
            string padding = new(' ', spaces);

            string output = input + padding;
            return output;
        }
        public static string GetCenteredPadding(string input, int spaces)
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
                $"│                                                                       │",
                $"│                               PLAY AGAIN?                             │",
                $"│                                  Y/N                                  │",
                $"╰───────────────────────────────────────────────────────────────────────╯"
            };

            PrintCenteredStringArray(startPrompt, yStartPosition);
            yStartPosition += 2;

            foreach (var player in players)
            {
                int namePadding = 70;
                int handpadding = 11;
                int outcomePadding = 13;
                int betPadding = 12;
                int walletPadding = 13;

                string paddedName = GetPadding(player.Name, namePadding);
                string nameLine = $"│ {paddedName}│";
                PrintCenteredString(nameLine, yStartPosition);
                yStartPosition++;

                foreach (var hand in player.Hands)
                {
                    if (hand.CurrentCards.Count < 1)
                        continue;

                    string outcome = GetPadding($"[{Enum.GetName(hand.HandState)}]", outcomePadding);
                    string betResult = "";
                    string wallet = "";

                    if (hand.HandState == HandState.BLACKJACK)
                    {
                        betResult = $"+{(hand.Bet * 2).ToString("C2")}";
                        wallet = GetPadding((player.Wallet + (3 * hand.Bet)).ToString("C2"), walletPadding);
                    }
                    else if (hand.HandState == HandState.WIN)
                    {
                        betResult = $"+{hand.Bet.ToString("C2")}";
                        wallet = GetPadding((player.Wallet + (2 * hand.Bet)).ToString("C2"), walletPadding);
                    }
                    else
                    {
                        betResult = $"-{hand.Bet.ToString("C2")}";
                        wallet = GetPadding(player.Wallet.ToString("C2"), walletPadding);
                    }

                    betResult = GetPadding(betResult, betPadding);

                    string handVariant = "MAIN HAND";
                    if (hand == player.Hands[1])
                        handVariant = "SPLIT HAND";
                    handVariant = GetPadding(handVariant, handpadding);

                    string playerSummary = $"│    {handVariant}{outcome}{betResult} REMAINING FUNDS:{wallet} │";
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
            promptWidth = 73;
            yPosition = 30;

            Graphics.HighlightCurrentHand(player);

            string[] prompt =
            {
                $"╭───────────────────────────────────────────────────────────────────────╮",
                $"|{GetCenteredPadding($"{player.Name}'S TURN", promptWidth-2)}|",
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
            Utilities.ToggleCursorVisibility();

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
                        Utilities.ToggleCursorVisibility();
                        return betInput;
                    }
                }
                yPosition += 2;
                PrintCenteredString(errorMessage, yPosition);
                yPosition -= 2;
            }
        }
        public static void PromptEndedHand(Player player)
        {
            SetConsoleColors("B", "G");
            int promptWidth = 73;
            int yPosition = 30;
            Hand currentHand = player.CurrentHand;
            string[] prompt = new string[4];

            if (currentHand.HandState == HandState.BLACKJACK)
            {
                prompt[0] = $"╭───────────────────────────────────────────────────────────────────────╮";
                prompt[1] = $"|{GetCenteredPadding("HAND CONCLUDED", promptWidth - 2)}|";
                prompt[2] = $"|{GetCenteredPadding($"{player.Name} got {currentHand.HandState}", promptWidth - 2)}|";
                prompt[3] = $"╰───────────────────────────────────────────────────────────────────────╯";

                PrintCenteredStringArray(prompt, yPosition);
            }
            else if (currentHand.HandState != HandState.UNDECIDED)
            {
                prompt[0] = $"╭───────────────────────────────────────────────────────────────────────╮";
                prompt[1] = $"|{GetCenteredPadding("HAND CONCLUDED", promptWidth - 2)}|";
                prompt[2] = $"|{GetCenteredPadding($"{player.Name} {currentHand.HandState} on {currentHand.HandSum()}", promptWidth - 2)}|";
                prompt[3] = $"╰───────────────────────────────────────────────────────────────────────╯";

                PrintCenteredStringArray(prompt, yPosition);
            }

            Thread.Sleep(2000);
            Utilities.ErasePrompt(promptWidth, yPosition);
        }
        public static void PromptDealerMoves(out int promptWidth, out int yPosition)
        {
            SetConsoleColors("B", "G");
            promptWidth = 73;
            yPosition = 13;
            int cardSum = Dealer.Instance.Hand.HandSum();
            HandState handState = Dealer.Instance.Hand.HandState;
            PlayerAction latestAction = Dealer.Instance.LatestAction;


            string dealerMove = "";

            if (handState == HandState.BLACKJACK)
            {
                dealerMove = $"DEALER GOT BLACKJACK";
            }
            else if (latestAction == PlayerAction.STAND)
            {
                dealerMove = $"DEALER <STANDS> on [{cardSum}]";
            }
            else if (latestAction == PlayerAction.HIT && handState != HandState.BUSTS)
            {
                dealerMove = $"DEALER <HITS> on [{cardSum}]";
            }
            else
            {
                dealerMove = $"DEALER IS BUST";
            }

            string[] prompt =
            {
                $"╭───────────────────────────────────────────────────────────────────────╮",
                $"|{GetCenteredPadding($"{dealerMove}", promptWidth-2)}|",
                $"│                                                                       │",
                $"╰───────────────────────────────────────────────────────────────────────╯"
            };

            PrintCenteredStringArray(prompt, yPosition);
            SetConsoleColors("DG", "DG");
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
            if (player.CurrentHand.HandState == HandState.BUSTS)
            {
                completePlayerHand = HandState.BUSTS.ToString() + " - SUM [" + hand.HandSum() + "]";
            }
            FileManager.SaveHandInfo(completePlayerHand);
        }
        public static void SaveFirstDealInfo(List<Player> players)
        {
            string temp = "FIRST DEAL TEMP FIX LATER";

            FileManager.SaveHandInfo(temp);
        }
        public static void UpdatePlayerLog(Player player, Hand currentHand)
        {
            string handInfo = "";
            string handVariant = "";
            string playerName = player.Name.ToUpper();
            string lastCard = currentHand.CurrentCards.Last().Title;
            string cardSymbol = currentHand.CurrentCards.Last().CardSymbol;
            int cardSum = currentHand.HandSum();
            PlayerAction latestAction = player.LatestAction;
            HandState handState = currentHand.HandState;

            if (currentHand == player.Hands[0])
                handVariant = "original";
            else
                handVariant = "split";

            if (handState == HandState.BLACKJACK)
                handInfo = $"{playerName} got [BLACKJACK] on their {handVariant} hand";
            else if (latestAction == PlayerAction.SPLIT)
                handInfo = $"{playerName} <SPLITS> their original hand, they now have two hands worth [{cardSum}]";
            else if (latestAction == PlayerAction.STAND)
                handInfo = $"{playerName} <STANDS> at [{cardSum}] on their {handVariant} hand";
            else if (latestAction == PlayerAction.HIT && handState != HandState.BUSTS)
            {
                int lastHandSum = cardSum - player.CurrentHand.CurrentCards.Last().Value;
                handInfo = $"{playerName} <HITS> at [{lastHandSum}] and is dealt a [{lastCard}{cardSymbol}], their {handVariant} hand is now worth [{cardSum}]";
            }
            else
            {
                int lastHandSum = cardSum - player.CurrentHand.CurrentCards.Last().Value;
                handInfo = $"{playerName} <HITS> at [{lastHandSum}] and is dealt a [{lastCard}{cardSymbol}], their {handVariant} hand is now [BUST]";
            }

            log.Add(handInfo);
        }
        public static void UpdateDealerLog()
        {
            string cardSymbol = Dealer.Instance.Hand.CurrentCards.Last().CardSymbol;
            string lastCard = Dealer.Instance.Hand.CurrentCards.Last().Title;
            int cardSum = Dealer.Instance.Hand.HandSum();
            PlayerAction latestAction = Dealer.Instance.LatestAction;
            HandState handState = Dealer.Instance.Hand.HandState;

            string handInfo = "";

            if (handState == HandState.BLACKJACK)
            {
                handInfo = $"DEALER got [BLACKJACK]";
            }
            else if (latestAction == PlayerAction.STAND)
            {
                handInfo = $"DEALER <STANDS> on [{cardSum}]";
            }
            else if (latestAction == PlayerAction.HIT && cardSum < 22)
            {
                int lastHandSum = cardSum - Dealer.Instance.Hand.CurrentCards.Last().Value;
                handInfo = $"DEALER <HITS> on [{lastHandSum}] and is dealt a [{lastCard}{cardSymbol}], their hand is now worth [{cardSum}]";
            }
            else
            {
                int lastHandSum = cardSum - Dealer.Instance.Hand.CurrentCards.Last().Value;
                handInfo = $"DEALER <HITS> on [{lastHandSum}] and is dealt a [{lastCard}{cardSymbol}], their hand is now [BUST]";
            }

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

        public static void ToggleCursorVisibility()
        {
            Console.CursorVisible = (Console.CursorVisible == true) ? false : true;
        }
    }
}
