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
        private readonly static int _cardFlipDelay = 500;
        private static (int _animationStartingXPosition, int _animationStartingYPosition) _cardAnimationStartingPosition = (101, 18);
        private static (double[] _x, double[] _y) vectors = ScalingVectors();
        private static (int _xPosition, int _yPosition) _playerOneRegion = ((int)vectors._x[vectors._x.Length - 1] - _cardWidth, (int)vectors._y[vectors._y.Length / 2 - 1]);
        private static (int _xPosition, int _yPosition) _playerTwoRegion = ((int)vectors._x[vectors._x.Length / 2 + 1], (int)vectors._y[vectors._y.Length - 1]);
        private static (int _xPosition, int _yPosition) _playerThreeRegion = ((int)vectors._x[0], (int)vectors._y[vectors._y.Length / 2 - 1]);
        private static (int _xPosition, int _yPosition) _dealerRegion = ((int)vectors._x[vectors._x.Length / 2], (int)vectors._y[0]);


        public static void AnimateACardFromBottomToTop(Hand hand)
        {
            (int startingXPosition, int startingYPosition) = hand.Cards.Last().LatestCardPosition;
            startingXPosition += _cardWidth / 2 * hand.Cards.Count;
            int distance = _cardAnimationStartingPosition._animationStartingYPosition - _dealerRegion._yPosition - _cardHeight;

            Console.ForegroundColor = ConsoleColor.White;
            string[] cardArray = new string[6];
            for (int i = 0; i < _cardWidth - 1; i++)
            {
                cardArray[i] = hand.Cards.Last().CardGraphicWhileMoving.Substring(i * _cardWidth, _cardWidth);
            }
            string greenString = new(' ', _cardWidth);

            Console.SetCursorPosition(startingXPosition, startingYPosition);
            for (int i = 0; i < distance; i++)
            {
                Console.BackgroundColor = ConsoleColor.DarkBlue;
                for (int yPosition = _cardHeight - 1; yPosition >= 0; yPosition--)
                {
                    Console.SetCursorPosition(startingXPosition, Console.CursorTop - 1);
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
                Thread.Sleep(_verticalAnimationSpeed);
            }
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Thread.Sleep(_cardFlipDelay);
            hand.Cards.Last().LatestCardPosition = (Console.CursorLeft, Console.CursorTop - _cardHeight);
            PrintASingleCard(hand.Cards.Last());
        }
        public static void AnimateACardFromLeftToRight(Hand hand)
        {
            (int startingXPosition, int startingYPosition) = hand.Cards.Last().LatestCardPosition;

            int distance = _playerOneRegion._xPosition - startingXPosition - (hand.Cards.Count * _cardWidth / 2);
            Console.SetCursorPosition(startingXPosition, startingYPosition);

            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.White;
            string[] cardArray = new string[6];
            for (int i = 0; i < _cardWidth - 1; i++)
            {
                cardArray[i] = hand.Cards[0].CardGraphicWhileMoving.Substring(i * _cardWidth, _cardWidth);
            }

            for (int i = 0; i < distance; i++)
            {
                for (int yPosition = 0; yPosition < _cardWidth; yPosition++)
                {
                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                    if (yPosition < _cardWidth - 1)
                    {
                        Console.SetCursorPosition(startingXPosition, Console.CursorTop + 1);
                        Console.Write(cardArray[yPosition]);
                    }
                    else
                    {
                        if (i != 0)
                        {
                            int oldXPosition = startingXPosition - 1;
                            for (int j = 0; j < _cardHeight; j++)
                            {
                                Console.BackgroundColor = ConsoleColor.DarkGreen;
                                Console.SetCursorPosition(oldXPosition, Console.CursorTop);
                                Console.Write(" ");
                                Console.SetCursorPosition(oldXPosition, Console.CursorTop - 1);
                            }
                        }
                    }
                }
                Console.SetCursorPosition(startingXPosition++, startingYPosition);
                Thread.Sleep(_horizontalAnimationSpeed);
            }
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Thread.Sleep(_cardFlipDelay);
            hand.Cards.Last().LatestCardPosition = (Console.CursorLeft, Console.CursorTop);
            PrintASingleCard(hand.Cards.Last());
        }
        public static void AnimateACardFromRightToLeft(Hand hand)
        {
            (int startingXPosition, int startingYPosition) = (hand.Cards.Last().LatestCardPosition.LatestXPosition - _cardWidth * 2, hand.Cards.Last().LatestCardPosition.LatestYPosition);
            int distance = startingXPosition - _playerThreeRegion._xPosition - _cardWidth / 2 * hand.Cards.Count;
            Console.SetCursorPosition(startingXPosition, startingYPosition);

            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.White;
            string[] cardArray = new string[6];
            for (int i = 0; i < _cardWidth - 1; i++)
            {
                cardArray[i] = hand.Cards.Last().CardGraphicWhileMoving.Substring(i * _cardWidth, _cardWidth);
            }

            for (int i = 0; i < distance; i++)
            {
                for (int yPosition = 0; yPosition < _cardWidth; yPosition++)
                {
                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                    if (yPosition < _cardWidth - 1)
                    {
                        Console.SetCursorPosition(startingXPosition, Console.CursorTop + 1);
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
                Thread.Sleep(_horizontalAnimationSpeed);
            }
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Thread.Sleep(_cardFlipDelay);
            hand.Cards.Last().LatestCardPosition = (Console.CursorLeft, Console.CursorTop);
            PrintASingleCard(hand.Cards.Last());
        }
        public static void AnimateACardFromTopToBottom(Hand hand)
        {
            (int startingXPosition, int startingYPosition) = (hand.Cards.Last().LatestCardPosition.LatestXPosition - _cardWidth, hand.Cards.Last().LatestCardPosition.LatestYPosition);
            startingXPosition += _cardWidth / 2 * hand.Cards.Count;

            int distance = _playerTwoRegion._yPosition - startingYPosition;
            Console.ForegroundColor = ConsoleColor.White;
            string[] cardArray = new string[6];
            for (int i = 0; i < _cardWidth - 1; i++)
            {
                cardArray[i] = hand.Cards[0].CardGraphicWhileMoving.Substring(i * _cardWidth, _cardWidth);
            }
            string greenString = new(' ', _cardWidth);

            Console.SetCursorPosition(startingXPosition, startingYPosition);
            for (int i = 0; i < distance; i++)
            {
                Console.BackgroundColor = ConsoleColor.DarkBlue;
                for (int yPosition = 0; yPosition < _cardWidth - 1; yPosition++)
                {
                    Console.SetCursorPosition(startingXPosition, Console.CursorTop + 1);
                    Console.Write(cardArray[yPosition]);
                }
                int oldTopCursorPosition = Console.CursorTop - _cardHeight;

                Console.SetCursorPosition(startingXPosition, oldTopCursorPosition);
                Console.BackgroundColor = ConsoleColor.DarkGreen;
                Console.Write(greenString);

                Console.SetCursorPosition(startingXPosition, ++startingYPosition);
                Thread.Sleep(_verticalAnimationSpeed);
            }
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            //hand.Cards[0].LatestCardPosition = (hand.Cards[0].LatestCardPosition.LatestXPosition + _cardWidth / 2, hand.Cards[0].LatestCardPosition.LatestYPosition);
            Thread.Sleep(_cardFlipDelay);
            hand.Cards.Last().LatestCardPosition = (Console.CursorLeft, Console.CursorTop - 1);
            PrintASingleCard(hand.Cards.Last());
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
                //AnimateACardFromLeftToRight(card, 80, 18, 15, _shuffleAnimationSpeed);
                //AnimateACardFromLeftToRight(card);
                //AnimateACardFromRightToLeft(card, 110, 18, 15, _shuffleAnimationSpeed);
                //AnimateACardFromRightToLeft(card);
                co--;
            }
            EraseAPrintedCard(77, 18);
            EraseAPrintedCard(78, 18);
            EraseAPrintedCard(124, 18);
            EraseAPrintedCard(125, 18);
            int numberOfCardsInStack = 8;
            PrintAStackOfCards(card, _cardAnimationStartingPosition._animationStartingXPosition - _cardWidth, _cardAnimationStartingPosition._animationStartingYPosition, numberOfCardsInStack);
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
        private static void PrintASingleCard(Card card)
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = card.IsRed ? ConsoleColor.Red : ConsoleColor.Black;
            string[] cardArray = new string[6];
            for (int i = 0; i < _cardWidth - 1; i++)
            {
                cardArray[i] = card.CardGraphic.Substring(i * _cardWidth, _cardWidth);
            }
            Console.SetCursorPosition(card.LatestCardPosition.LatestXPosition + _cardWidth, card.LatestCardPosition.LatestYPosition);
            for (int yPosition = 0; yPosition < _cardWidth - 1; yPosition++)
            {
                Console.SetCursorPosition(Console.CursorLeft - _cardWidth, Console.CursorTop + 1);
                Console.Write(cardArray[yPosition]);
            }
            Console.BackgroundColor = ConsoleColor.DarkGreen;
        }
        public static void PrintAStackOfCards(Card card, int startingXPosition, int startingYPosition, int numberOfCardsInStack)
        {
            //x = 100 for rough middle.
            //y = 18 for rough middle position
            Console.SetCursorPosition(startingXPosition, startingYPosition);

            Console.BackgroundColor = ConsoleColor.DarkBlue;
            string[] cardArray = new string[6];
            for (int i = 0; i < _cardWidth - 1; i++)
            {
                cardArray[i] = card.CardGraphicWhileMoving.Substring(i * _cardWidth, _cardWidth);
            }

            for (int i = 0; i <= numberOfCardsInStack; i++)
            {
                Console.BackgroundColor = ConsoleColor.DarkBlue;
                for (int yPosition = 0; yPosition < _cardWidth - 1; yPosition++)
                {
                    Console.SetCursorPosition(Console.CursorLeft - _cardWidth, Console.CursorTop + 1);
                    Console.Write(cardArray[yPosition]);
                }
                Console.SetCursorPosition(startingXPosition++, startingYPosition);
            }
            Console.BackgroundColor = ConsoleColor.DarkGreen;
        }
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
        public static void PrintLog()
        {
            var vectors = ScalingVectors();
            int startPosX = 1;
            int startPosY = 1;
            Console.SetCursorPosition((int)vectors.x[startPosX], (int)vectors.y[startPosY]);
            int cursorLeft = Console.CursorLeft;
            int lastInTheList = Utilities.log.Count() - 1;

            Console.Write("╭────────────────────────────────────────────────────────────────────────────────╮");

            for (int i = lastInTheList; i >= (lastInTheList - 5); i--)
            {
                Console.SetCursorPosition(cursorLeft, Console.CursorTop + 1);
                int spaces = 80 - Utilities.log[i].Length;
                string padding = new string(' ', spaces);
                Console.Write($"│{Utilities.log[i]}{padding}│");
            }
            Console.SetCursorPosition(cursorLeft, Console.CursorTop + 1);
            Console.Write("╰────────────────────────────────────────────────────────────────────────────────╯");

        }
        public static void PrintPlayerTitleAndSum(Participant participant)
        {
            int startXPos = 0;
            int startYPos = 0;
            double chanceOfSuccess = Deck.CalculateChanceOfSuccess(participant.Hands[0].HandSum());
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
                        startXPos = _playerThreeRegion._xPosition + 1;
                        startYPos = _playerThreeRegion._yPosition - 2;
                        break;

                    default:
                        break;
                }

                string playerHeader = $"{player.Name}'s hand: {participant.Hands[0].HandSum()}";
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

                string dealerHeader = $"Dealer's hand: {participant.Hands[0].HandSum()}";
                string greenString = new(' ', dealerHeader.Length);

                Console.SetCursorPosition(startXPos, startYPos);
                Console.WriteLine(greenString);
                Console.SetCursorPosition(startXPos, startYPos);
                Console.Write(dealerHeader);
            }
            Console.SetCursorPosition(1, 1);
        }
        public static (double[] x, double[] y) ScalingVectors()
        {
            double cardHeight = _cardHeight;
            double cardWidth = _cardWidth;

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
        public static void UpdateDealerBoard()
        {
            //Graphics.PrintASingleCard();
            Utilities.LogDealerInfo();
            Graphics.PrintLog();
        }
        public static void UpdateBoard()
        {
            //Graphics.PrintSinglePlayerCards(player);
            //Graphics.PrintASingleCard(player.Hands[0]);
            Graphics.PrintLog();
        }
    }
}