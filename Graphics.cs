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
        //Card aceOfSpades = new("_____", "|A.   |", "| /.\\ |", "|(_._)|", "|  |  |", "|____V|");

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
            var vectors = ScalingVectors();
            double[] xValues = vectors.x;
            double[] yValues = vectors.y;

            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            string[] cardArray = new string[6];
            for (int i = 0; i < _cardWidth - 1; i++)
            {
                cardArray[i] = card.CardGraphic.Substring(i * _cardWidth, _cardWidth);
            }

            // y position is the height value
            // the xposition has to be chosen accordingly
            for (int xPosition = 0; xPosition < 1; xPosition++)
            {
                Console.SetCursorPosition((int)xValues[xPosition], (int)yValues[3]);
                for (int yPosition = 0; yPosition < _cardWidth - 1; yPosition++)
                {
                    Console.SetCursorPosition((int)xValues[xPosition], Console.CursorTop + 1);
                    Console.Write(cardArray[yPosition]);
                }
            }
            Console.BackgroundColor = ConsoleColor.DarkGreen;
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
