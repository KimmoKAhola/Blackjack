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
        public static void PrintCard(Card card)
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

        public static void PrintCard(List<Card> listOfCards)
        {
            // TODO startPosX 8 prints one card in a specific region!!!!
            // TODO startPosY 5 prints one card in a specific region!!!!
            //TODO Brädet är 28 kort brett och 8 kort högt.
            int startPosX = 10;
            int startPosY = 5;
            foreach (Card card in listOfCards)
            {
                var vectors = ScalingVectors();
                double[] xValues = vectors.x;
                double[] yValues = vectors.y;
                Console.SetCursorPosition((int)xValues[startPosX], (int)yValues[startPosY]);
                PrintCard(card);
                startPosX++;
            }
        }

        public static (double[] x, double[] y) ScalingVectors()
        {
            double cardHeight = 6;
            double cardWidth = 7;

            double stepsInXDirection = (windowWidth + 10) / cardWidth;
            double stepsInYDirection = (windowHeight + 10) / cardHeight;

            double[] vectorXValues = new double[(int)stepsInXDirection];
            double[] vectorYValues = new double[(int)stepsInYDirection - 1];

            for (int i = 0; i < vectorXValues.Length; i++)
            {
                vectorXValues[i] = i * cardWidth + 1;
            }

            for (int i = 0; i < vectorYValues.Length; i++)
            {
                vectorYValues[i] = i * cardHeight;
            }

            return (vectorXValues, vectorYValues);
        }

    }
}
