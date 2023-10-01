using System.Xml.Linq;

namespace Blackjack
{
    /// <summary>
    /// Creates a playing board for the black jack table.
    ///
    /// </summary>
    public class Graphics
    {
        public const int windowWidth = 190;
        public const int windowHeight = 40;

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

        private readonly static int _cardWidth = 7;
        /// <summary>
        /// Prints a card at a certain position which is decided
        /// by two scaling vectors.
        /// </summary>
        /// <param name="card"></param>
        private static void PrintCard(Card card)
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
        private static void PrintCard(Player player)
        {
            var vectors = ScalingVectors();
            // TODO startPosX 8 prints one card in a specific region!!!!
            // TODO startPosY 5 prints one card in a specific region!!!!
            //TODO Brädet är 28 kort brett och 8 kort högt.
            int playerRegion = player.PlayerNumber;
            List<Card> listOfCards = player.PlayerHand;
            int startPosX, startPosY;

            //This switch case decides where to print the cards. The region values are hard coded in a switch case.
            switch (playerRegion)
            {
                case 0: //dealer on the top
                    startPosX = vectors.x.Length / 2 - player.PlayerHand.Count / 2;
                    startPosY = 0;
                    break;
                case 1: //player one on the right side
                    startPosX = vectors.x.Length - 2 - player.PlayerHand.Count;
                    startPosY = vectors.y.Length / 3;
                    break;
                case 2: // player two on the bottom
                    startPosX = vectors.x.Length / 2 - player.PlayerHand.Count / 2;
                    startPosY = vectors.y.Length - 4;
                    break;
                case 3: // player three on the left side
                    startPosX = 0 + player.PlayerHand.Count;
                    startPosY = vectors.y.Length / 3;
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
                PrintCard(card);
                startPosX++;
            }
            Console.ForegroundColor = ConsoleColor.Yellow;
        }


        public static void PrintCard(List<Player> players)
        {
            foreach (Player player in players)
            {
                PrintCard(player);
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

            double stepsInXDirection = (windowWidth + 10) / cardWidth * 2;
            double stepsInYDirection = (windowHeight + 10) / cardHeight * 2;

            double[] vectorXValues = new double[(int)stepsInXDirection];
            double[] vectorYValues = new double[(int)stepsInYDirection - 1];

            for (int i = 0; i < vectorXValues.Length; i++)
            {
                vectorXValues[i] = i * cardWidth / 2 + 1;
            }

            for (int i = 0; i < vectorYValues.Length; i++)
            {
                vectorYValues[i] = i * cardHeight / 2;
            }

            return (vectorXValues, vectorYValues);
        }

        //TODO does not work currently, but almost.
        public static void PrintPlayerInfo(Player player)
        {
            var vectors = ScalingVectors();


            int startPosX = vectors.x.Length - 20;
            int startPosY = 0;

            Console.SetCursorPosition(startPosX, startPosY);
            List<string> infoList = new();


            string info = $"The player {player.Name}, with id {player.PlayerNumber}, has the hand";

            infoList.Add(info);
            foreach (Card card in player.PlayerHand)
            {
                infoList.Add(($"[{card.Title} {card.Value}] "));
            }

            foreach (var item in infoList)
            {
                Console.SetCursorPosition((int)vectors.x[startPosX], Console.CursorTop + 1);
                Console.Write(item);
            }
            if (infoList.Count > 4)
            {
                infoList.RemoveAt(0);
                Console.SetCursorPosition(startPosX, startPosY);
            }
        }
    }
}
