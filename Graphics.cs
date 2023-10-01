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
        /// Prints a card at a specified position inside our playing table
        ///
        /// </summary>
        public static void PrintCards(Card card)
        {
            var vectors = ScalingVectors();
            card.PrintCard(vectors.x, vectors.y);
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
