using System.Reflection.Metadata;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;

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
        private static List<string> Log = new List<string>()
        {
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
        /// <param name="player"></param>
        private static void PrintSinglePlayerCards(Player player)
        {
            int counter = 0;
            var vectors = ScalingVectors();
            // TODO startPosX 8 prints one card in a specific region!!!!
            // TODO startPosY 5 prints one card in a specific region!!!!
            // TODO Brädet är 28 kort brett och 8 kort högt.
            int playerRegion = player.PlayerNumber;
            List<Card> listOfCards = player.PlayerHand;
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
                    startPosX = vectors.x.Length - player.PlayerHand.Count;
                    startPosY = vectors.y.Length / 2 - 1;
                    break;
                case 2: // player two on the bottom
                    startPosX = vectors.x.Length / 2 + 1 - player.PlayerHand.Count / 2;
                    startPosY = vectors.y.Length - 1;
                    break;
                case 3: // player three on the left side
                    startPosX = (int)vectors.x[0];
                    startPosY = vectors.y.Length / 2 - 1;
                    break;
                default:
                    //TODO fix error handling later.
                    startPosX = vectors.x.Length / 2 - player.PlayerHand.Count / 2;
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
        public static void ShuffleAnimationForASingleCard(Card card, int distance)
        {
            //TODO Use this later on to express the starting positions as functions of a card.
            //var vectors = ScalingVectors();  
            //Console.CursorVisible = true; // For debugging

            int startingXPosition = 100; // Hard coded values
            int startingYPosition = 18;
            int updatedXPosition;
            int animationSpeed = 5; // Change this to play around with the animation speed. Values between 1 and 500 are "ok".


            Console.SetCursorPosition(startingXPosition, startingYPosition);

            //Create a card array with blue strings. No graphic is needed.
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            string[] cardArray = new string[6];
            for (int i = 0; i < _cardWidth - 1; i++)
            {
                cardArray[i] = card.CardGraphic.Substring(i * _cardWidth, _cardWidth);
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

        public static void UpdateLog()
        {
            var vectors = ScalingVectors();
            int startPosX = 1;
            int startPosY = 1;
            Console.SetCursorPosition((int)vectors.x[startPosX], (int)vectors.y[startPosY]);
            int cursorLeft = Console.CursorLeft;

            if (Log.Count > 4)
            {
                Log.RemoveAt(0);
            }
            Console.Write("╭────────────────────────────────────────────────────────────────────────────────╮");

            for (int i = 3; i >= 0; i--)
            {
                Console.SetCursorPosition(cursorLeft, Console.CursorTop + 1);
                int spaces = 80 - Log[i].Length;
                string padding = new string(' ', spaces);
                Console.Write($"│{Log[i]}{padding}│");
            }
            Console.SetCursorPosition(cursorLeft, Console.CursorTop + 1);
            Console.Write("╰────────────────────────────────────────────────────────────────────────────────╯");

        }

        //TODO does not work currently, but almost.
        public static void LogPlayerInfo(Player player)
        {
            string handInfo = $"{player.Name} got dealt a {player.PlayerHand.Last().Title}, their hand is now worth {player.HandSum()}";

            Log.Add(handInfo);


            //var vectors = ScalingVectors();
            //
            //int startPosX = vectors.x.Length - 20;
            //int startPosY = 0;
            //
            //Console.SetCursorPosition((int)vectors.x[startPosX], (int)vectors.y[startPosY]);
            //List<string> infoList = new();
            //
            ////TODO change so that HandSum checks for aces in the hand.
            //string info = $"The player {player.Name}, with id {player.PlayerNumber}, has the hand with value {player.HandSum()}"; 
            //
            //infoList.Add(info);
            //foreach (Card card in player.PlayerHand)
            //{
            //    infoList.Add(($"[{card.Title} {card.Value}] "));
            //}
            //
            //foreach (var item in infoList)
            //{
            //    Console.SetCursorPosition((int)vectors.x[startPosX], Console.CursorTop + 1);
            //    Console.Write(item);
            //}
        }

        public static void PrintAStackOfCards(Card card)
        {
            var vectors = ScalingVectors();
            int startingXPosition = (int)vectors.x[vectors.x.Length / 2-1]; // Hard coded values
            int startingYPosition = (int)vectors.y[vectors.y.Length / 2-1];
            //int updatedXPosition, updatedYPosition;
            int numberOfCardsInStack = 6;

            Console.SetCursorPosition(startingXPosition,startingYPosition);

            //Create a card array with blue strings. No graphic is needed.
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            string[] cardArray = new string[6];
            for (int i = 0; i < _cardWidth - 1; i++)
            {
                cardArray[i] = card.CardGraphic.Substring(i * _cardWidth, _cardWidth);
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