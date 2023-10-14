namespace Blackjack
{
    /// <summary>
    /// Creates a playing board for the black jack table.
    ///
    /// </summary>
    public static class Graphics
    {
        private const int windowWidth = 195;
        private const int windowHeight = 45;
        private readonly static int _cardWidth = 7;
        private readonly static int _cardHeight = 6;
        private readonly static int _horizontalAnimationSpeed = 5; // 5 seems to work
        private readonly static int _verticalAnimationSpeed = 20; // 20 seems to work
        private readonly static int _shuffleAnimationSpeed = 3; // shuffleanimationspeed
        private static (double[] _x, double[] _y) vectors = ScalingVectors();
        private static (int _xPosition, int _yPosition) _playerOneRegion = ((int)vectors._x[vectors._x.Length - 1] - _cardWidth, (int)vectors._y[vectors._y.Length / 2 - 1]);
        private static (int _xPosition, int _yPosition) _playerTwoRegion = ((int)vectors._x[vectors._x.Length / 2 + 1], (int)vectors._y[vectors._y.Length - 1]);
        private static (int _xPosition, int _yPosition) _playerThreeRegion = ((int)vectors._x[0] + _cardWidth, (int)vectors._y[vectors._y.Length / 2 - 1]);
        private static (int _xPosition, int _yPosition) _dealerRegion = ((int)vectors._x[vectors._x.Length / 2], (int)vectors._y[0]);
        private static List<string> _log = new List<string>()
        {
            "",
            "",
            "",
            "",
            "",
            ""
        };

        /// <summary>
        /// Prints out a square with rounded corners.
        /// Hard coded values for window height and width
        /// 50 and 200 on the console
        /// and 40, 190 on the playing board.
        /// </summary>
        public static void PrintBoard()
        {
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.ForegroundColor = ConsoleColor.White;
            char line = '─';
            string playingBoard = "╭" + new string(line, windowWidth) + "╮";
            char playingBoardBorder = '│';
            for (int i = 0; i < windowHeight; i++)
            {
                playingBoard += "\n" + playingBoardBorder + new string(' ', windowWidth) + playingBoardBorder;
            }
            playingBoard += "\n" + "╰" + new string(line, windowWidth) + "╯";
            Console.WriteLine(playingBoard);
        }
        /// <summary>
        /// Prints a card at a certain position which is decided
        /// by two scaling vectors.
        /// </summary>
        /// <param name="card"></param>
        private static void PrintASingleCard(Card card)
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = card.IsRed ? ConsoleColor.Red : ConsoleColor.Black;
            string[] cardArray = new string[6];
            for (int i = 0; i < _cardWidth - 1; i++)
            {
                cardArray[i] = card.CardGraphic.Substring(i * _cardWidth, _cardWidth);
            }

            for (int yPosition = 0; yPosition < _cardWidth - 1; yPosition++)
            {
                Console.SetCursorPosition(Console.CursorLeft - _cardWidth, Console.CursorTop + 1);
                Console.Write(cardArray[yPosition]);
            }
            Console.BackgroundColor = ConsoleColor.DarkGreen;
        }
        /// <summary>
        /// A method that prints out the whole player hand.
        /// Utilizes the PrintCard method to print out one card at a time while looping through
        /// the list of cards.
        /// </summary>
        /// <param name="participant"></param>
        private static void PrintSinglePlayerCards(Participant participant)
        {
            var vectors = ScalingVectors();
            // TODO startPosX 8 prints one card in a specific region!!!!
            // TODO startPosY 5 prints one card in a specific region!!!!
            // TODO Brädet är 28 kort brett och 8 kort högt.
            int playerRegion = 0;
            if (participant is Player)
            {
                Player player = (Player)participant;
                playerRegion = player.PlayerNumber;
            }
            List<Card> listOfCards = participant.Hand;
            int startPosX, startPosY;

            //This switch case decides where to print the cards. The region values are hard coded in a switch case.
            // The middle can be found by vectors.x.Length / 2 + 1. The +1 is because we have an odd size on the window width (27 cards wide)
            switch (playerRegion)
            {
                case 0: //dealer on the top
                    startPosX = _dealerRegion._xPosition + _cardWidth / 2; //103, 106
                    startPosY = _dealerRegion._yPosition;
                    break;
                case 1: //player one on the right side
                    startPosX = _playerOneRegion._xPosition - participant.Hand.Count;
                    startPosY = _playerOneRegion._yPosition;
                    break;
                case 2: // player two on the bottom
                    startPosX = _playerTwoRegion._xPosition - participant.Hand.Count / 2;
                    startPosY = _playerTwoRegion._yPosition;
                    break;
                case 3: // player three on the left side
                    startPosX = _playerThreeRegion._xPosition;
                    startPosY = _playerThreeRegion._yPosition;
                    break;
                default:
                    //TODO fix error handling later.
                    startPosX = vectors.x.Length / 2 - participant.Hand.Count / 2;
                    startPosY = 5;
                    break;
            }

            foreach (Card card in listOfCards)
            {
                //double[] xValues = vectors.x;
                //double[] yValues = vectors.y;
                Console.SetCursorPosition(startPosX, startPosY);
                PrintASingleCard(card);

                startPosX += (int)(_cardWidth / 2);

            }
            Console.ForegroundColor = ConsoleColor.Yellow;
        }
        public static void PrintAllPlayerCards(List<Player> players)
        {
            Console.SetCursorPosition(0, 0);
            foreach (Player player in players)
            {
                PrintSinglePlayerCards(player);
            }
        }
        public static void PrintAllDealerCards()
        {
            Console.SetCursorPosition(0, 0);
            PrintSinglePlayerCards(Dealer.Instance);
        }
        /// <summary>
        /// Divides the playing board into different subparts.
        /// These subparts are then used to decide where to draw the card graphics.
        /// The subparts are a function of windowSize / cardSize in both
        /// x and y direction.
        /// </summary>
        /// <returns></returns>
        public static (double[] x, double[] y) ScalingVectors()
        {
            double cardHeight = 6;
            double cardWidth = 7;

            double stepsInXDirection = (windowWidth + cardWidth / 2) / cardWidth * 2;
            double stepsInYDirection = (windowHeight - cardHeight / 2) / cardHeight * 2;

            double[] vectorXValues = new double[(int)stepsInXDirection];
            double[] vectorYValues = new double[(int)stepsInYDirection];

            for (int i = 0; i < vectorXValues.Length; i++)
            {
                vectorXValues[i] = i * cardWidth / 2 + 2;
            }

            for (int i = 0; i < vectorYValues.Length; i++)
            {
                vectorYValues[i] = i * cardHeight / 2;
            }

            return (vectorXValues, vectorYValues);
        }
        public static void AnimateACardFromTopToBottom(Card card, int startingXPosition, int startingYPosition, int distance, int verticalAnimationSpeed)
        {
            string[] cardArray = new string[6];
            for (int i = 0; i < _cardWidth - 1; i++)
            {
                cardArray[i] = card.CardGraphicWhileStationary.Substring(i * _cardWidth, _cardWidth);
            }
            string greenString = new(' ', _cardWidth);

            Console.SetCursorPosition(startingXPosition, startingYPosition);
            for (int i = 0; i < distance; i++)
            {
                Console.BackgroundColor = ConsoleColor.DarkBlue;
                for (int yPosition = 0; yPosition < _cardWidth - 1; yPosition++)
                {
                    Console.SetCursorPosition(startingXPosition, Console.CursorTop + 1); // start with a cursorposition at 25
                    Console.Write(cardArray[yPosition]);
                }
                int oldTopCursorPosition = Console.CursorTop - _cardHeight;

                Console.SetCursorPosition(startingXPosition, oldTopCursorPosition);
                Console.BackgroundColor = ConsoleColor.DarkGreen;
                Console.Write(greenString);

                Console.SetCursorPosition(startingXPosition, ++startingYPosition);
                Thread.Sleep(verticalAnimationSpeed);
            }
            Console.BackgroundColor = ConsoleColor.DarkGreen;
        }
        public static void AnimateACardFromBottomToTop(Card card, int startingXPosition, int startingYPosition, int distance, int verticalAnimationSpeed)
        {
            string[] cardArray = new string[6];
            for (int i = 0; i < _cardWidth - 1; i++)
            {
                cardArray[i] = card.CardGraphicWhileStationary.Substring(i * _cardWidth, _cardWidth);
            }
            string greenString = new(' ', _cardWidth);

            Console.SetCursorPosition(startingXPosition, startingYPosition);
            for (int i = 0; i < distance; i++)
            {
                Console.BackgroundColor = ConsoleColor.DarkBlue;
                for (int yPosition = _cardHeight - 1; yPosition >= 0; yPosition--)
                {
                    Console.SetCursorPosition(startingXPosition, Console.CursorTop - 1); // start with a cursorposition at 25
                    Console.Write(cardArray[yPosition]);
                }
                int oldTopCursorPosition = Console.CursorTop + _cardHeight;
                if (i >= 1)
                {
                    Console.SetCursorPosition(startingXPosition, oldTopCursorPosition);
                    Console.BackgroundColor = ConsoleColor.DarkGreen;
                    Console.Write(greenString);
                }

                Console.SetCursorPosition(startingXPosition, --startingYPosition);
                Thread.Sleep(verticalAnimationSpeed);
            }
            Console.BackgroundColor = ConsoleColor.DarkGreen;
        }
        public static void AnimateACardFromRightToLeft(Card card, int startingXPosition, int startingYPosition, int distance, int horizontalAimationSpeed)
        {
            //int startingXPosition = 80; // Hard coded values
            //int startingYPosition = 18;

            Console.SetCursorPosition(startingXPosition, startingYPosition);

            //Create a card array with blue strings. No graphic is needed.
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            string[] cardArray = new string[6];
            for (int i = 0; i < _cardWidth - 1; i++)
            {
                cardArray[i] = card.CardGraphicWhileStationary.Substring(i * _cardWidth, _cardWidth);
            }

            for (int i = 0; i < distance; i++)
            {
                for (int yPosition = 0; yPosition < _cardWidth; yPosition++)
                {
                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                    if (yPosition < _cardWidth - 1)
                    {
                        Console.SetCursorPosition(startingXPosition, Console.CursorTop + 1); // start with a cursorposition at 25
                        Console.Write(cardArray[yPosition]);
                    }
                    else
                    {
                        if (i != 0)
                        {
                            for (int j = 0; j < _cardHeight; j++)
                            {
                                Console.BackgroundColor = ConsoleColor.DarkGreen;
                                Console.Write(" ");
                                Console.SetCursorPosition(startingXPosition + _cardWidth, Console.CursorTop - 1);
                            }
                        }
                    }
                }

                Console.SetCursorPosition(startingXPosition--, startingYPosition);

                Thread.Sleep(horizontalAimationSpeed);
            }
            Console.BackgroundColor = ConsoleColor.DarkGreen;
        }
        public static void AnimateACardFromLeftToRight(Card card, int startingXPosition, int startingYPosition, int distance, int horizontalAnimationSpeed)
        {
            //int startingXPosition = 100; // Hard coded values
            //int startingYPosition = 18;
            Console.SetCursorPosition(startingXPosition, startingYPosition);

            //Create a card array with blue strings. No graphic is needed.
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            string[] cardArray = new string[6];
            for (int i = 0; i < _cardWidth - 1; i++)
            {
                cardArray[i] = card.CardGraphicWhileStationary.Substring(i * _cardWidth, _cardWidth);
            }

            for (int i = 0; i < distance; i++)
            {
                for (int yPosition = 0; yPosition < _cardWidth; yPosition++)
                {
                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                    if (yPosition < _cardWidth - 1)
                    {
                        Console.SetCursorPosition(startingXPosition, Console.CursorTop + 1); // start with a cursorposition at 25
                        Console.Write(cardArray[yPosition]);
                    }
                    else
                    {
                        if (i != 0)
                        {
                            int oldX = startingXPosition - 1;
                            //int updY = 
                            for (int j = 0; j < _cardHeight; j++)
                            {
                                Console.BackgroundColor = ConsoleColor.DarkGreen;
                                Console.SetCursorPosition(oldX, Console.CursorTop);
                                Console.Write(" ");
                                Console.SetCursorPosition(oldX, Console.CursorTop - 1);
                            }
                        }
                    }
                }

                Console.SetCursorPosition(startingXPosition++, startingYPosition);

                Thread.Sleep(horizontalAnimationSpeed);
            }
            Console.BackgroundColor = ConsoleColor.DarkGreen;
        }
        public static void UpdateLog()
        {
            var vectors = ScalingVectors();
            int startPosX = 1;
            int startPosY = 1;
            Console.SetCursorPosition((int)vectors.x[startPosX], (int)vectors.y[startPosY]);
            int cursorLeft = Console.CursorLeft;
            int lastInTheList = _log.Count() - 1;

            Console.Write("╭────────────────────────────────────────────────────────────────────────────────╮");

            for (int i = lastInTheList; i >= (lastInTheList - 5); i--)
            {
                Console.SetCursorPosition(cursorLeft, Console.CursorTop + 1);
                int spaces = 80 - _log[i].Length;
                string padding = new string(' ', spaces);
                Console.Write($"│{_log[i]}{padding}│");
            }
            Console.SetCursorPosition(cursorLeft, Console.CursorTop + 1);
            Console.Write("╰────────────────────────────────────────────────────────────────────────────────╯");

        }
        public static void LogPlayerInfo(Player player)
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
            completePlayerHand = completePlayerHand.TrimEnd(',', ' ');
            completePlayerHand = "[" + completePlayerHand + "]";
            string handInfo = $"{playerName} was dealt a [{lastCard}{cardSymbol}], their hand is now worth {cardSum}";
            _log.Add(handInfo);
            completePlayerHand = playerName + "\n" + completePlayerHand;
            FileManager.SaveHandInfo(completePlayerHand);
        }
        public static void LogDealerInfo()
        {
            string cardSymbol = Dealer.Instance.Hand.Last().CardSymbol;
            string lastCard = Dealer.Instance.Hand.Last().Title;
            int cardSum = Dealer.Instance.HandSum();

            string handInfo = $"The dealer was dealt a [{lastCard}{cardSymbol}], their hand is now worth {cardSum}";

            _log.Add(handInfo);
            FileManager.SaveHandInfo(handInfo);
        }
        public static void UpdateBoard(List<Player> players, int currentPlayer)
        {
            Graphics.PrintAllPlayerCards(players);
            Graphics.LogPlayerInfo(players[currentPlayer]);
            Graphics.UpdateLog();
        }
        public static void UpdateBoard()
        {
            Graphics.PrintAllDealerCards();
            Graphics.LogDealerInfo();
            Graphics.UpdateLog();
        }
        public static void PrintAStackOfCards(Card card, int startingXPosition, int startingYPosition, int numberOfCardsInStack)
        {
            //x = 100 for rough middle.
            //y = 18 for rough middle position
            Console.SetCursorPosition(startingXPosition, startingYPosition);

            //Create a card array with blue strings. No graphic is needed.
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            string[] cardArray = new string[6];
            for (int i = 0; i < _cardWidth - 1; i++)
            {
                cardArray[i] = card.CardGraphicWhileStationary.Substring(i * _cardWidth, _cardWidth);
            }

            for (int i = 0; i <= numberOfCardsInStack; i++)
            {
                Console.BackgroundColor = ConsoleColor.DarkBlue;
                for (int yPosition = 0; yPosition < _cardWidth - 1; yPosition++)
                {
                    Console.SetCursorPosition(Console.CursorLeft - _cardWidth, Console.CursorTop + 1); // start with a cursorposition at 25
                    Console.Write(cardArray[yPosition]);
                }
                Console.SetCursorPosition(startingXPosition++, startingYPosition);
            }
            Console.BackgroundColor = ConsoleColor.DarkGreen;
        }
        public static void AnimateCardsInAllDirections(Card card, int numberOfCardsDealt, List<Player> players)
        {
            //Loopa igenom alla
            //om bet > 0
            //ge x antal kort till den spelaren

            for (int i = 0; i < numberOfCardsDealt; i++)
            {
                foreach (Player player in players)
                {
                    if (player.Bet > 0)
                    {
                        if (player.PlayerNumber == 1)
                            AnimateACardFromLeftToRight(card, 111 - _cardWidth / 2 * i, 18, 75, _horizontalAnimationSpeed); //Player 1
                        if (player.PlayerNumber == 2)
                            AnimateACardFromTopToBottom(card, 100 + _cardWidth / 2 * i, 25, 15, _verticalAnimationSpeed); //Player 2
                        if (player.PlayerNumber == 3)
                            AnimateACardFromRightToLeft(card, 80 + _cardWidth / 2 * i, 18, 75, _horizontalAnimationSpeed); //Player 3
                    }
                }
                //_dealerRegion._xPosition + _cardWidth/2
                AnimateACardFromBottomToTop(card, _dealerRegion._xPosition - _cardWidth / 2 + _cardWidth / 2 * i, 16, 10, _verticalAnimationSpeed); //Dealer
            }
            int tempCursorPositionX = Console.CursorLeft + (int)(_cardWidth);
            int tempCursorPositionY = Console.CursorTop - _cardHeight;
            Console.SetCursorPosition(tempCursorPositionX, tempCursorPositionY);
            PrintASingleCard(Dealer.Instance.Hand[1]); //TODO This should be the dealers second card
            Thread.Sleep(500);
            for (int i = 0; i < numberOfCardsDealt; i++)
            {
                EraseAPrintedCard(192 - _cardWidth / 2 * i, 18);
                EraseAPrintedCard(107 + _cardWidth / 2 * i, 39);
                EraseAPrintedCard(13 + _cardWidth / 2 * i, 18);
                //EraseAPrintedCard(107 + _cardWidth / 2 * i, 0); //Issue #31 solved here
            }
        }
        public static void PrintPlayerTitleAndSum(Participant participant)
        {
            int startXPos = 0;
            int startYPos = 0;
            double chanceOfSuccess = Deck.CalculateChanceOfSuccess(participant.HandSum());
            if (participant is Player)
            {
                //20
                Player player = (Player)participant;
                switch (player.PlayerNumber)
                {
                    case 1:
                        startXPos = _playerOneRegion._xPosition - 15;
                        startYPos = _playerOneRegion._yPosition - 2;
                        break;
                    case 2:
                        startXPos = _playerTwoRegion._xPosition - 15;
                        startYPos = _playerTwoRegion._yPosition - 2;
                        break;
                    case 3:
                        startXPos = _playerThreeRegion._xPosition - 7;
                        startYPos = _playerThreeRegion._yPosition - 2;
                        break;

                    default:
                        break;
                }

                string playerHeader = $"{player.Name}'s hand: {participant.HandSum()}";
                string headerGreenString = new(' ', playerHeader.Length);
                string chanceString = $"CHANCE OF SUCCESS: ~{(chanceOfSuccess * 100):F0}%";

                string chanceGreenString = new(' ', 24);

                Console.SetCursorPosition(startXPos, startYPos);
                Console.Write(headerGreenString);
                Console.SetCursorPosition(startXPos, startYPos);
                Console.Write(playerHeader);

                Console.SetCursorPosition(startXPos, startYPos + 1);
                Console.Write(chanceGreenString);
                Console.SetCursorPosition(startXPos, startYPos + 1);
                Console.Write(chanceString);
            }
            else if (participant is Dealer)
            {
                startXPos = _dealerRegion._xPosition - 10;
                startYPos = _dealerRegion._yPosition + 1 + _cardHeight;

                string dealerHeader = $"Dealer's hand: {participant.HandSum()}";
                string greenString = new(' ', dealerHeader.Length);

                Console.SetCursorPosition(startXPos, startYPos);
                Console.WriteLine(greenString);
                Console.SetCursorPosition(startXPos, startYPos);
                Console.Write(dealerHeader);
            }
            Console.SetCursorPosition(1, 1);
        }
        public static void AnimateDeckShuffle(Card card)
        {
            Thread.Sleep(2000);
            PrintAStackOfCards(card, 77, 18, 2);
            PrintAStackOfCards(card, 124, 18, 2);
            Thread.Sleep(2000);

            int co = 10; // 10
            while (co > 0)
            {
                AnimateACardFromLeftToRight(card, 80, 18, 15, _shuffleAnimationSpeed);
                AnimateACardFromRightToLeft(card, 110, 18, 15, _shuffleAnimationSpeed);
                co--;
            }
            EraseAPrintedCard(77, 18);
            EraseAPrintedCard(78, 18);
            EraseAPrintedCard(124, 18);
            EraseAPrintedCard(125, 18);
            int numberOfCardsInStack = 8;
            int cardStackXStartingPosition = 96 - _cardWidth / 2 + numberOfCardsInStack;
            PrintAStackOfCards(card, cardStackXStartingPosition, 18, numberOfCardsInStack);
            Console.BackgroundColor = ConsoleColor.DarkGreen;
        }
        public static void PrintAStationaryCard(Card card, int startingXPosition, int startingYPosition)
        {
            //int startingXPosition = (int)vectors._x[vectors._x.Length / 2 - 1];
            //int startingYPosition = (int)vectors._y[vectors._y.Length / 2 - 1];

            Console.SetCursorPosition(startingXPosition, startingYPosition);

            Console.BackgroundColor = ConsoleColor.DarkBlue;
            string[] cardArray = new string[6];
            for (int i = 0; i < _cardWidth - 1; i++)
            {
                cardArray[i] = card.CardGraphicWhileStationary.Substring(i * _cardWidth, _cardWidth);
            }

            Console.BackgroundColor = ConsoleColor.DarkBlue;
            for (int yPosition = 0; yPosition < _cardWidth - 1; yPosition++)
            {
                Console.SetCursorPosition(Console.CursorLeft - _cardWidth, Console.CursorTop + 1); // start with a cursorposition at 25
                Console.Write(cardArray[yPosition]);
            }
            Console.BackgroundColor = ConsoleColor.DarkGreen;
        }
        public static void EraseAPrintedCard(int startingXPosition, int startingYPosition)
        {
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            string[] cardArray = new string[6];
            for (int i = 0; i < _cardWidth - 1; i++)
            {
                cardArray[i] = new string(' ', _cardWidth);
            }
            Console.SetCursorPosition(startingXPosition, startingYPosition);
            for (int yPosition = 0; yPosition < _cardWidth - 1; yPosition++)
            {
                Console.SetCursorPosition(Console.CursorLeft - _cardWidth, Console.CursorTop + 1);
                Console.Write(cardArray[yPosition]);
            }
        }
    }
}