namespace Blackjack
{
    /// <summary>
    /// Creates all the graphics for the blackjack game.
    /// Contains methods for animations.
    /// Contains a method for scaling the playing window.
    /// Contains methods for printing information on the board.
    /// Contains several private variables deciding where and how fast cards are animated.
    /// Contains player regions on the board.
    /// </summary>
    public static class Graphics
    {
        static int _consoleWindowWidth = Console.LargestWindowWidth;
        static int _consoleWindowHeigth = Console.LargestWindowHeight;
        private readonly static int _windowWidth = _consoleWindowWidth - 5;
        private readonly static int _windowHeight = _consoleWindowHeigth - 5;

        public readonly static int _cardWidth = 7;
        public readonly static int _cardHeight = 6;
        private readonly static int _horizontalAnimationSpeed = 5; // 5 seems to work
        private readonly static int _verticalAnimationSpeed = 20; // 20 seems to work
        private readonly static int _shuffleAnimationSpeed = 3; // shuffleanimationspeed
        private readonly static int _cardFlipDelay = 500;
        private static (int _animationStartingXPosition, int _animationStartingYPosition) _cardAnimationStartingPosition = (101, 18);
        private static (double[] _x, double[] _y) vectors = ScalingVectors();
        private static string[] _movingCardGraphicArray = CreateMovingCardGraphicArray();
        private static (int _xPosition, int _yPosition) _playerOneRegion = ((int)vectors._x[^1] - _cardWidth, (int)vectors._y[vectors._y.Length / 2 - 1]);
        private static (int _xPosition, int _yPosition) _playerTwoRegion = ((int)vectors._x[vectors._x.Length / 2 + 1], (int)vectors._y[^1]);
        private static (int _xPosition, int _yPosition) _playerThreeRegion = ((int)vectors._x[0], (int)vectors._y[vectors._y.Length / 2 - 1]);
        private static (int _xPosition, int _yPosition) _dealerRegion = ((int)vectors._x[vectors._x.Length / 2], (int)vectors._y[0]);

        /// <summary>
        /// Animates a single card to the dealer.
        /// Has the dealer hand as an input.
        /// </summary>
        /// <param name="hand"></param>
        public static void AnimateACardFromBottomToTop(Hand hand)
        {
            (int startingXPosition, int startingYPosition) = hand.CurrentCards.Last().LatestCardPosition;
            startingXPosition += _cardWidth / 2 * hand.CurrentCards.Count;
            int distance = _cardAnimationStartingPosition._animationStartingYPosition - _dealerRegion._yPosition - _cardHeight;

            Utilities.SetConsoleColors("W", "");
            string greenString = new(' ', _cardWidth);

            Console.SetCursorPosition(startingXPosition, startingYPosition);
            for (int i = 0; i < distance; i++)
            {
                Utilities.SetConsoleColors("", "DB");
                for (int yPosition = _cardHeight - 1; yPosition >= 0; yPosition--)
                {
                    Console.SetCursorPosition(startingXPosition, Console.CursorTop - 1);
                    Console.Write(_movingCardGraphicArray[yPosition]);
                }
                int oldTopCursorPosition = Console.CursorTop + _cardHeight;
                if (i >= 1)
                {
                    Console.SetCursorPosition(startingXPosition, oldTopCursorPosition);
                    Utilities.SetConsoleColors("", "DG");
                    Console.Write(greenString);
                }

                Console.SetCursorPosition(startingXPosition, --startingYPosition);
                Thread.Sleep(_verticalAnimationSpeed);
            }
            Utilities.SetConsoleColors("", "DG");

            Thread.Sleep(_cardFlipDelay);
            hand.CurrentCards.Last().LatestCardPosition = (Console.CursorLeft, Console.CursorTop - _cardHeight);
            if (hand.CurrentCards.Count >= 2)
                PrintASingleCard(hand.CurrentCards.Last());
        }
        /// <summary>
        /// Animates a single card at a time to player 1.
        /// Has a player as an input and changes the card animation values depending on the active hand
        /// </summary>
        /// <param name="player"></param>
        public static void AnimateACardFromLeftToRight(Player player)
        {
            Hand hand = player.CurrentHand;
            (int startingXPosition, int startingYPosition) = hand.CurrentCards.Last().LatestCardPosition;
            startingXPosition = startingXPosition;
            int distance = _playerOneRegion._xPosition - startingXPosition - (hand.CurrentCards.Count * _cardWidth / 2);
            if (player.CurrentHand == player.Hands[1])
            {
                startingYPosition += (int)(3 * _cardHeight / 2);
            }
            Console.SetCursorPosition(startingXPosition, startingYPosition);

            Utilities.SetConsoleColors("W", "DB");

            for (int i = 0; i < distance; i++)
            {
                for (int yPosition = 0; yPosition < _cardWidth; yPosition++)
                {
                    Utilities.SetConsoleColors("", "DB");

                    if (yPosition < _cardWidth - 1)
                    {
                        Console.SetCursorPosition(startingXPosition, Console.CursorTop + 1);
                        Console.Write(_movingCardGraphicArray[yPosition]);
                    }
                    else
                    {
                        if (i != 0)
                        {
                            int oldXPosition = startingXPosition - 1;
                            for (int j = 0; j < _cardHeight; j++)
                            {
                                Utilities.SetConsoleColors("", "DG");

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
            Utilities.SetConsoleColors("", "DG");

            Thread.Sleep(_cardFlipDelay);
            hand.CurrentCards.Last().LatestCardPosition = (Console.CursorLeft, Console.CursorTop);
            PrintASingleCard(hand.CurrentCards.Last());
        }
        /// <summary>
        /// Animates a single card at a time to player 3.
        /// Has a player as an input and changes the card animation values depending on the active hand
        /// </summary>
        /// <param name="player"></param>
        public static void AnimateACardFromRightToLeft(Player player)
        {
            Hand hand = player.CurrentHand;
            (int startingXPosition, int startingYPosition) = (hand.CurrentCards.Last().LatestCardPosition.LatestXPosition - _cardWidth * 2, hand.CurrentCards.Last().LatestCardPosition.LatestYPosition);
            startingXPosition -= _cardWidth;
            int distance = startingXPosition - _playerThreeRegion._xPosition - _cardWidth / 2 * hand.CurrentCards.Count;
            if (player.CurrentHand == player.Hands[1])
            {
                startingYPosition += (int)(3 * _cardHeight / 2);
            }
            Console.SetCursorPosition(startingXPosition, startingYPosition);

            Utilities.SetConsoleColors("W", "DB");


            for (int i = 0; i < distance; i++)
            {
                for (int yPosition = 0; yPosition < _cardWidth; yPosition++)
                {
                    Utilities.SetConsoleColors("", "DB");
                    if (yPosition < _cardWidth - 1)
                    {
                        Console.SetCursorPosition(startingXPosition, Console.CursorTop + 1);
                        Console.Write(_movingCardGraphicArray[yPosition]);
                    }
                    else
                    {
                        if (i != 0)
                        {
                            for (int j = 0; j < _cardHeight; j++)
                            {
                                Utilities.SetConsoleColors("", "DG");
                                Console.Write(" ");
                                Console.SetCursorPosition(startingXPosition + _cardWidth, Console.CursorTop - 1);
                            }
                        }
                    }
                }
                Console.SetCursorPosition(startingXPosition--, startingYPosition);
                Thread.Sleep(_horizontalAnimationSpeed);
            }
            Utilities.SetConsoleColors("", "DG");
            Thread.Sleep(_cardFlipDelay);
            hand.CurrentCards.Last().LatestCardPosition = (Console.CursorLeft, Console.CursorTop);
            PrintASingleCard(hand.CurrentCards.Last());
        }
        /// <summary>
        /// Animates a single card at a time to player 2.
        /// Has a player as an input and changes the card animation values depending on the active hand
        /// </summary>
        /// <param name="player"></param>
        public static void AnimateACardFromTopToBottom(Player player)
        {
            Hand hand = player.CurrentHand;
            (int startingXPosition, int startingYPosition) = (hand.CurrentCards.Last().LatestCardPosition.LatestXPosition - _cardWidth, hand.CurrentCards.Last().LatestCardPosition.LatestYPosition);
            startingXPosition += _cardWidth / 2 * hand.CurrentCards.Count;

            if (player.CurrentHand == player.Hands[1])
            {
                startingXPosition += (int)(8 * _cardHeight / 2);
            }

            int distance = _playerTwoRegion._yPosition - startingYPosition;
            Utilities.SetConsoleColors("W", "");

            string greenString = new(' ', _cardWidth);

            Console.SetCursorPosition(startingXPosition, startingYPosition);
            for (int i = 0; i < distance; i++)
            {
                Utilities.SetConsoleColors("", "DB");
                for (int yPosition = 0; yPosition < _cardWidth - 1; yPosition++)
                {
                    Console.SetCursorPosition(startingXPosition, Console.CursorTop + 1);
                    Console.Write(_movingCardGraphicArray[yPosition]);
                }
                int oldTopCursorPosition = Console.CursorTop - _cardHeight;

                Console.SetCursorPosition(startingXPosition, oldTopCursorPosition);
                Utilities.SetConsoleColors("", "DG");
                Console.Write(greenString);

                Console.SetCursorPosition(startingXPosition, ++startingYPosition);
                Thread.Sleep(_verticalAnimationSpeed);
            }
            Utilities.SetConsoleColors("", "DG");
            Thread.Sleep(_cardFlipDelay);
            hand.CurrentCards.Last().LatestCardPosition = (Console.CursorLeft, Console.CursorTop - 1);
            PrintASingleCard(hand.CurrentCards.Last());
        }
        /// <summary>
        /// Animates the deck shuffle at the start of each round.
        /// </summary>
        /// <param name="card"></param>
        public static void AnimateDeckShuffle(Card card)
        {
            //PrintAStackOfCards(card, 77, 18, 2);
            //PrintAStackOfCards(card, 124, 18, 2);

            int co = 10;
            while (co > 0)
            {
                //AnimateACardFromLeftToRight(card, 80, 18, 15, _shuffleAnimationSpeed);
                //AnimateACardFromLeftToRight(card);
                //AnimateACardFromRightToLeft(card, 110, 18, 15, _shuffleAnimationSpeed);
                //AnimateACardFromRightToLeft(card);
                co--;
            }

            int numberOfCardsInStack = 3; // 8 is standard
            PrintAStackOfCards(card, _cardAnimationStartingPosition._animationStartingXPosition - _cardWidth, _cardAnimationStartingPosition._animationStartingYPosition, numberOfCardsInStack);
            Utilities.SetConsoleColors("", "DG");
        }
        /// <summary>
        /// Creates an array for the card. This is used when printing a card's face-down graphics.
        /// </summary>
        /// <returns></returns>
        private static string[] CreateMovingCardGraphicArray()
        {
            string[] cardArray = new string[6];
            for (int i = 0; i < _cardWidth - 1; i++)
            {
                cardArray[i] = Deck.AllCards[0].CardGraphicWhileMoving.Substring(i * _cardWidth, _cardWidth);
            }
            return cardArray;
        }
        /// <summary>
        /// Creates an array for a card. This is used when printing a card's face-up graphics.
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        private static string[] CreateStationaryCardGraphicArray(Card card)
        {
            string[] cardArray = new string[6];
            for (int i = 0; i < _cardWidth - 1; i++)
            {
                cardArray[i] = card.CardGraphic.Substring(i * _cardWidth, _cardWidth);
            }
            return cardArray;
        }
        /// <summary>
        /// Erases a printed card at a certain position. Erases a card by printing over its position with green color.
        /// </summary>
        /// <param name="startingXPosition"></param>
        /// <param name="startingYPosition"></param>
        public static void EraseAPrintedCard(int startingXPosition, int startingYPosition)
        {
            Utilities.SetConsoleColors("", "DG");
            Console.SetCursorPosition(startingXPosition, startingYPosition);
            for (int yPosition = 0; yPosition < _cardWidth - 1; yPosition++)
            {
                Console.SetCursorPosition(Console.CursorLeft - _cardWidth, Console.CursorTop + 1);
                Console.Write(_movingCardGraphicArray[yPosition]);
            }
        }
        /// <summary>
        /// Prints a single card at a given position. The card's position depends on the current player.
        /// </summary>
        /// <param name="card"></param>
        private static void PrintASingleCard(Card card)
        {
            Utilities.SetConsoleColors("", "W");
            Console.ForegroundColor = card.IsRed ? ConsoleColor.Red : ConsoleColor.Black;
            string[] cardArray = CreateStationaryCardGraphicArray(card);
            Console.SetCursorPosition(card.LatestCardPosition.LatestXPosition + _cardWidth, card.LatestCardPosition.LatestYPosition);
            for (int yPosition = 0; yPosition < _cardWidth - 1; yPosition++)
            {
                Console.SetCursorPosition(Console.CursorLeft - _cardWidth, Console.CursorTop + 1);
                Console.Write(cardArray[yPosition]);
            }
            Utilities.SetConsoleColors("Y", "DG");
        }
        /// <summary>
        /// Prints a stack of cards. This is used when doing the shuffling animation at the start of each round.
        /// </summary>
        /// <param name="card"></param>
        /// <param name="startingXPosition"></param>
        /// <param name="startingYPosition"></param>
        /// <param name="numberOfCardsInStack"></param>
        public static void PrintAStackOfCards(Card card, int startingXPosition, int startingYPosition, int numberOfCardsInStack)
        {
            Console.SetCursorPosition(startingXPosition, startingYPosition);
            Utilities.SetConsoleColors("W", "DB");

            string[] cardArray = new string[6];
            for (int i = 0; i < _cardWidth - 1; i++)
            {
                cardArray[i] = card.CardGraphicWhileMoving.Substring(i * _cardWidth, _cardWidth);
            }

            for (int i = 0; i <= numberOfCardsInStack; i++)
            {
                for (int yPosition = 0; yPosition < _cardWidth - 1; yPosition++)
                {
                    Console.SetCursorPosition(Console.CursorLeft - _cardWidth, Console.CursorTop + 1);
                    Console.Write(cardArray[yPosition]);
                }
                Console.SetCursorPosition(startingXPosition++, startingYPosition);
            }
            Utilities.SetConsoleColors("Y", "DG");
        }
        /// <summary>
        /// A method for printing a player's split hand.
        /// Does this by erasing the player's original 2 card hand and printing the cards with a distance between them to
        /// show the user that the hand has been split into 2 separate hands.
        /// </summary>
        /// <param name="player"></param>
        public static void PrintASplitHand(Player player)
        {
            Hand hand = player.CurrentHand;
            Hand secondHand = player.Hands[1];
            (int startingXPosition, int startingYPosition) = hand.CurrentCards.Last().LatestCardPosition;
            Console.SetCursorPosition(startingXPosition, startingYPosition);
            foreach (var currentHand in player.Hands)
            {
                switch (player.PlayerNumber)
                {
                    case 1:
                        if (currentHand == player.Hands[1])
                        {
                            secondHand.CurrentCards.Last().LatestCardPosition = (startingXPosition, startingYPosition + (int)(3 * _cardHeight / 2));
                            PrintASingleCard(secondHand.CurrentCards.Last());
                        }
                        else
                        {
                            PrintASingleCard(hand.CurrentCards.Last());
                        }
                        break;
                    case 2:
                        if (currentHand == player.Hands[1])
                        {
                            secondHand.CurrentCards.Last().LatestCardPosition = (startingXPosition + (int)(7 * _cardWidth / 2), startingYPosition);
                            PrintASingleCard(secondHand.CurrentCards.Last());
                        }
                        else
                        {
                            PrintASingleCard(hand.CurrentCards.Last());
                        }
                        break;
                    case 3:
                        if (currentHand == player.Hands[1])
                        {
                            secondHand.CurrentCards.Last().LatestCardPosition = (startingXPosition, startingYPosition + (int)(3 * _cardHeight / 2));
                            PrintASingleCard(secondHand.CurrentCards.Last());
                        }
                        else
                        {
                            PrintASingleCard(hand.CurrentCards.Last());
                        }
                        break; ;
                }
            }
        }
        /// <summary>
        /// Prints a green playing board with a thin, white border along the edges.
        /// </summary>
        public static void PrintBoard()
        {
            Utilities.SetConsoleColors("W", "DG");
            char line = '─';
            string playingBoard = "╭" + new string(line, _windowWidth) + "╮";
            char playingBoardBorder = '│';
            for (int i = 0; i < _windowHeight; i++)
            {
                playingBoard += "\n" + playingBoardBorder + new string(' ', _windowWidth) + playingBoardBorder;
            }
            playingBoard += "\n" + "╰" + new string(line, _windowWidth) + "╯";
            Console.WriteLine(playingBoard);
        }
        /// <summary>
        /// Prints the game log at the top left part of the playing board.
        /// Uses info from a list containing each participant move and player info, such as hand sum.
        /// </summary>
        public static void PrintLog()
        {
            Utilities.SetConsoleColors("Y", "DG");
            int startPosX = 1;
            int startPosY = 0;
            Console.SetCursorPosition((int)vectors._x[startPosX], (int)vectors._x[startPosY]);
            int cursorLeft = Console.CursorLeft;
            int lastInTheList = Utilities.log.Count - 1;

            Console.Write("╭─────────────────────────────────────────────────────────────────────────────────────╮");

            for (int i = lastInTheList; i >= (lastInTheList - 5); i--)
            {
                Console.SetCursorPosition(cursorLeft, Console.CursorTop + 1);
                int spaces = 85 - Utilities.log[i].Length;
                string padding = new(' ', spaces);
                Console.Write($"│{Utilities.log[i]}{padding}│");
            }
            Console.SetCursorPosition(cursorLeft, Console.CursorTop + 1);
            Console.Write("╰─────────────────────────────────────────────────────────────────────────────────────╯");

        }
        /// <summary>
        /// Prints the player's name as a header by their hand(s).
        /// </summary>
        /// <param name="participant"></param>
        public static void PrintPlayerHeaders(Player player)
        {
            int headerStartXPos = 0;
            int headerStartYPos = 0;

            switch (player.PlayerNumber)
            {
                case 1:
                    headerStartXPos = _playerOneRegion._xPosition + 3 - player.Name.Length;
                    headerStartYPos = _playerOneRegion._yPosition - 7;
                    break;
                case 2:
                    headerStartXPos = _playerTwoRegion._xPosition - 29 - player.Name.Length;
                    headerStartYPos = _playerTwoRegion._yPosition + 3;
                    break;
                case 3:
                    headerStartXPos = _playerThreeRegion._xPosition + 4;
                    headerStartYPos = _playerThreeRegion._yPosition - 7;
                    break;
            }

            string playerHeader = $"{player.Name.ToUpper()}";

            Console.SetCursorPosition(headerStartXPos, headerStartYPos);
            Console.Write(playerHeader);

            Console.SetCursorPosition(1, 1);

        }
        /// <summary>
        /// Prints the sum of a participant's specific current.
        /// </summary>
        /// <param name="participant"></param>
        public static void PrintHandStatus(Player player, Hand hand)
        {
            int statusStartXPos = 0;
            int statusStartYPos = 0;
            string handStatus = "";

            switch (hand.HandState)
            {
                case HandState.UNDECIDED:
                    handStatus = $"HAND SUM: {player.CurrentHand.HandSum()}";
                    break;
                case HandState.STANDS:
                    handStatus = $"STAND ON: {player.CurrentHand.HandSum()}";
                    break;
                case HandState.BUSTS:
                    handStatus = $"BUST ON: {player.CurrentHand.HandSum()}";
                    break;
                case HandState.BLACKJACK:
                    handStatus = $"BLACKJACK";
                    break;
                default:
                    break;
            }

            switch (player.PlayerNumber)
            {
                case 1:
                    statusStartXPos = hand.CurrentCards[0].LatestCardPosition.LatestXPosition - 4;
                    statusStartYPos = hand.CurrentCards[0].LatestCardPosition.LatestYPosition + 8;
                    break;
                case 2:
                    statusStartXPos = hand.CurrentCards[0].LatestCardPosition.LatestXPosition;
                    statusStartYPos = hand.CurrentCards[0].LatestCardPosition.LatestYPosition + 8;
                    break;
                case 3:
                    statusStartXPos = hand.CurrentCards[0].LatestCardPosition.LatestXPosition;
                    statusStartYPos = hand.CurrentCards[0].LatestCardPosition.LatestYPosition + 8;
                    break;
            }

            string clearLine = new(' ', 12);

            Console.SetCursorPosition(statusStartXPos, statusStartYPos);
            Console.Write(clearLine);
            Console.SetCursorPosition(statusStartXPos, statusStartYPos);
            Console.Write(handStatus);
        }
        public static void HighlightCurrentHand(Player player)
        {
            Utilities.SetConsoleColors("SETCACHED", "SETCACHED");
            Utilities.SetConsoleColors("Y", "DG");
            char topLeftCorner = '╭';
            char topRightCorner = '╮';
            char bottomLeftCorner = '╰';
            char bottomRightCorner = '╯';
            char verticalLine = '│';
            char horizontalLine = '─';

            int yPosition = player.CurrentHand.CurrentCards[0].LatestCardPosition.LatestYPosition;
            int leftX = 0;
            int rightX = 0;

            switch (player.PlayerNumber)
            {
                case 1:
                    leftX = player.CurrentHand.CurrentCards.Last().LatestCardPosition.LatestXPosition - 1;
                    rightX = player.CurrentHand.CurrentCards[0].LatestCardPosition.LatestXPosition + 7;

                    break;
                case 2:
                    leftX = player.CurrentHand.CurrentCards[0].LatestCardPosition.LatestXPosition - 1;
                    rightX = player.CurrentHand.CurrentCards.Last().LatestCardPosition.LatestXPosition + 7;
                    break;
                case 3:
                    leftX = player.CurrentHand.CurrentCards[0].LatestCardPosition.LatestXPosition - 1;
                    rightX = player.CurrentHand.CurrentCards.Last().LatestCardPosition.LatestXPosition + 7;
                    break;

                default:
                    break;
            }

            int horizontalEdgeWidth = rightX - leftX - 1;
            string horizontalEdge = new(horizontalLine, horizontalEdgeWidth);
            string topEdge = $"{topLeftCorner}{horizontalEdge}{topRightCorner}";
            string bottomEdge = $"{bottomLeftCorner}{horizontalEdge}{bottomRightCorner}";

            Console.SetCursorPosition(leftX, yPosition);
            Console.Write(topEdge);

            for (int i = 0; i < 6; i++)
            {
                yPosition++;
                Console.SetCursorPosition(leftX, yPosition);
                Console.Write(verticalLine);
                Console.SetCursorPosition(rightX, yPosition);
                Console.Write(verticalLine);
            }

            yPosition++;
            Console.SetCursorPosition(leftX, yPosition);
            Console.Write(bottomEdge);

            Utilities.SetConsoleColors("GETCACHED", "GETCACHED");
        }
        /// <summary>
        /// Prints the player's title and sum close to the position of the player's active hand.
        /// </summary>
        /// <param name="participant"></param>
        public static void PrintPlayerTitleAndSum(Participant participant)
        {
            int titleStartXPos = 0;
            int titleStartYPos = 0;
            double chanceOfSuccess = Deck.CalculateChanceOfSuccess(participant.Hands[0].HandSum());
            if (participant is Player)
            {
                Player player = (Player)participant;
                switch (player.PlayerNumber)
                {
                    case 1:
                        titleStartXPos = _playerOneRegion._xPosition - 8;
                        titleStartYPos = _playerOneRegion._yPosition - 7;
                        break;
                    case 2:
                        titleStartXPos = _playerTwoRegion._xPosition - 8;
                        titleStartYPos = _playerTwoRegion._yPosition - 7;
                        break;
                    case 3:
                        titleStartXPos = _playerThreeRegion._xPosition + 5;
                        titleStartYPos = _playerThreeRegion._yPosition - 7;
                        break;

                    default:
                        break;
                }

                string playerHeader = $"{player.Name.ToUpper()}";
                string headerGreenString = new(' ', playerHeader.Length);
                string chanceString = $"CHANCE OF SUCCESS: ~{(chanceOfSuccess * 100):F0}%";

                string chanceGreenString = new(' ', 24);

                Console.SetCursorPosition(titleStartXPos, titleStartYPos);
                //Console.Write(headerGreenString);
                Console.SetCursorPosition(titleStartXPos, titleStartYPos);
                Console.Write(playerHeader);

                Console.SetCursorPosition(titleStartXPos, titleStartYPos + 1);
                //Console.Write(chanceGreenString); //Un-comment to display a hand's chance of success
                Console.SetCursorPosition(titleStartXPos, titleStartYPos + 1);
                //Console.Write(chanceString); //Un-comment to display a hand's chance of success
            }
            else if (participant is Dealer)
            {
                titleStartXPos = _dealerRegion._xPosition - 10;
                titleStartYPos = _dealerRegion._yPosition + 1 + _cardHeight;

                string dealerHeader = $"Dealer's hand: {participant.Hands[0].HandSum()}";
                string greenString = new(' ', dealerHeader.Length);

                Console.SetCursorPosition(titleStartXPos, titleStartYPos);
                Console.WriteLine(greenString);
                Console.SetCursorPosition(titleStartXPos, titleStartYPos);
                Console.Write(dealerHeader);
            }
            Console.SetCursorPosition(1, 1);
        }
        /// <summary>
        /// Scaling vectors. Used to calculate each distance on the playing board relative to an x or y value based on
        /// a card's width (x) or height (y). This method makes it possible to scale the playing board up or down at the start of each game.
        /// </summary>
        /// <returns></returns>
        public static (double[] x, double[] y) ScalingVectors()
        {
            double cardHeight = _cardHeight;
            double cardWidth = _cardWidth;

            double stepsInXDirection = (_windowWidth + cardWidth / 2) / cardWidth * 2;
            double stepsInYDirection = (_windowHeight - cardHeight / 2) / cardHeight * 2;

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
    }
}