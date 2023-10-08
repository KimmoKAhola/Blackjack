using System.Numerics;
using System.Text;

namespace Blackjack
{
    /// <summary>
    /// Creates a playing board for the black jack table.
    ///
    /// </summary>
    public class Graphics
    {
        private const int windowWidth = 195;
        private const int windowHeight = 45;
        private readonly static int _cardWidth = 7;
        private readonly static int _cardHeight = 6;
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
        public void PrintBoard()
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
            int counter = 0;
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
                    startPosX = vectors.x.Length / 2 + 1; //- player.PlayerHand.Count / 2;
                    startPosY = 0;
                    break;
                case 1: //player one on the right side
                    startPosX = vectors.x.Length - participant.Hand.Count;
                    startPosY = vectors.y.Length / 2 - 1;
                    break;
                case 2: // player two on the bottom
                    startPosX = vectors.x.Length / 2 + 1 - participant.Hand.Count / 2;
                    startPosY = vectors.y.Length - 1;
                    break;
                case 3: // player three on the left side
                    startPosX = (int)vectors.x[0];
                    startPosY = vectors.y.Length / 2 - 1;
                    break;
                default:
                    //TODO fix error handling later.
                    startPosX = vectors.x.Length / 2 - participant.Hand.Count / 2;
                    startPosY = 5;
                    break;
            }

            foreach (Card card in listOfCards)
            {
                double[] xValues = vectors.x;
                double[] yValues = vectors.y;
                Console.SetCursorPosition((int)xValues[startPosX], (int)yValues[startPosY]);
                PrintASingleCard(card);

                startPosX++;

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
        public static void PrintAllPlayerCards(Dealer dealer)
        {
            Console.SetCursorPosition(0, 0);
            PrintSinglePlayerCards(dealer);
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
        /// <summary>
        /// This method animates a single card over a distance
        /// over the screen. Currently the starting position
        /// for the animation is hard coded roughly in the middle.
        /// </summary>
        /// <param name="card"></param>
        /// <param name="distance"></param>
        public static void AnimateACardFromLeftToRight(Card card, int distance)
        {
            //TODO Use this later on to express the starting positions as functions of a card.
            //var vectors = ScalingVectors();  
            //Console.CursorVisible = true; // For debugging

            int startingXPosition = 100; // Hard coded values
            int startingYPosition = 18;
            int updatedXPosition;
            int animationSpeed = 5; // Change this to play around with the animation speed. Values between 1-3 and 5 are "ok".


            Console.SetCursorPosition(startingXPosition, startingYPosition);

            //Create a card array with blue strings. No graphic is needed.
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            string[] cardArray = new string[6];
            for (int i = 0; i < _cardWidth - 1; i++)
            {
                cardArray[i] = card.CardGraphicWhileMoving.Substring(i * _cardWidth, _cardWidth);
            }

            string cardCornerTopLeft = "╭";
            string cardEdge = "│";
            string cardCornerBottomLeft = "╰";
            string cardCornerTopRight = "╮";
            string cardCornerBottomRight = "╯";
            for (int i = 0; i < distance; i++)
            {
                // distance is how far we want to animate the card to the right
                for (int yPosition = 0; yPosition < _cardWidth - 1; yPosition++)
                {
                    Console.SetCursorPosition(startingXPosition, Console.CursorTop + 1); // start with a cursorposition at 25
                    Console.Write(cardArray[yPosition]);
                }
                Console.SetCursorPosition(startingXPosition, startingYPosition);
                for (int width = 0; width < _cardWidth; width++) // Bredd för ett kort
                {
                    for (int xPosition = 0; xPosition < _cardWidth - 1; xPosition++)
                    {
                        //This loop overwrites the leftmost column with dark green, our table color.
                        Console.BackgroundColor = ConsoleColor.DarkGreen;
                        Console.SetCursorPosition(startingXPosition, Console.CursorTop + 1);
                        if (xPosition == 0)
                        {
                            Console.Write(" ");
                            Console.BackgroundColor = ConsoleColor.DarkBlue;
                            Console.Write(cardCornerTopLeft);
                        }
                        else if (xPosition == _cardWidth - 2)
                        {
                            Console.Write(" ");
                            Console.BackgroundColor = ConsoleColor.DarkBlue;
                            Console.Write(cardCornerBottomLeft);
                        }
                        else
                        {
                            Console.Write(" ");
                            Console.BackgroundColor = ConsoleColor.DarkBlue;
                            Console.Write(cardEdge);
                        }
                    }
                    Console.SetCursorPosition(startingXPosition + _cardWidth - 1, startingYPosition + 1);
                    updatedXPosition = startingXPosition + _cardWidth - 1;
                    for (int yPosition = 0; yPosition < _cardHeight; yPosition++)
                    {
                        Console.BackgroundColor = ConsoleColor.DarkBlue;
                        if (yPosition == 0)
                        {
                            Console.Write("─");
                            Console.Write(cardCornerTopRight);
                        }
                        else if (yPosition == _cardWidth - 2)
                        {
                            Console.Write("─");
                            Console.Write(cardCornerBottomRight);
                        }
                        else
                        {
                            Console.Write(" ");
                            Console.Write(cardEdge);
                        }
                        Console.SetCursorPosition(updatedXPosition, Console.CursorTop + 1);
                    }
                    Thread.Sleep(animationSpeed);
                    Console.SetCursorPosition(startingXPosition++, startingYPosition); // Update x Position
                }
            }
        }
        public static void AnimateACardFromTopToBottom(Card card, int distance)
        {
            int startingXPosition = 100; // Hard coded values
            int startingYPosition = 18; // 18 as start value originally.
            int animationSpeed = 2; // Change this to play around with the animation speed. Values between 1-3 and 5 are "ok".
            Console.CursorVisible = true;
            //Create a card array with blue strings. No graphic is needed.

            string[] cardArray = new string[6];
            for (int i = 0; i < _cardWidth - 1; i++)
            {
                cardArray[i] = card.CardGraphicWhileMoving.Substring(i * _cardWidth, _cardWidth);
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
                Thread.Sleep(animationSpeed);
            }
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

        //TODO does not work currently, but almost.
        public static void LogPlayerInfo(Player player)
        {
            string cardSymbol = player.Hand.Last().CardSymbol;
            string lastCard = player.Hand.Last().Title;
            int cardSum = player.HandSum();
            string playerName = player.Name.ToUpper();

            string handInfo = $"{playerName} was dealt a [{lastCard}{cardSymbol}], their hand is now worth {cardSum}";
            _log.Add(handInfo);
            FileManager.SaveHandInfo(handInfo);
        }
        public static void LogPlayerInfo(Dealer dealer)
        {
            string cardSymbol = dealer.Hand.Last().CardSymbol;
            string lastCard = dealer.Hand.Last().Title;
            int cardSum = dealer.HandSum();

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
        public static void UpdateBoard(Dealer dealer)
        {
            Graphics.PrintAllPlayerCards(dealer);
            Graphics.LogPlayerInfo(dealer);
            Graphics.UpdateLog();
        }

        public static void PrintAStackOfCards(Card card)
        {
            var vectors = ScalingVectors();
            int startingXPosition = (int)vectors.x[vectors.x.Length / 2 - 1]; // Hard coded values
            int startingYPosition = (int)vectors.y[vectors.y.Length / 2 - 1];
            //int updatedXPosition, updatedYPosition;
            int numberOfCardsInStack = 6;

            Console.SetCursorPosition(startingXPosition, startingYPosition);

            //Create a card array with blue strings. No graphic is needed.
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            string[] cardArray = new string[6];
            for (int i = 0; i < _cardWidth - 1; i++)
            {
                cardArray[i] = card.CardGraphicWhileStationary.Substring(i * _cardWidth, _cardWidth);
            }

            for (int i = 0; i < numberOfCardsInStack; i++)
            {
                Console.BackgroundColor = ConsoleColor.DarkBlue;
                for (int yPosition = 0; yPosition < _cardWidth - 1; yPosition++)
                {
                    Console.SetCursorPosition(Console.CursorLeft - _cardWidth, Console.CursorTop + 1); // start with a cursorposition at 25
                    Console.Write(cardArray[yPosition]);
                }
                Console.SetCursorPosition(startingXPosition++, startingYPosition);
            }
        }
    }
}