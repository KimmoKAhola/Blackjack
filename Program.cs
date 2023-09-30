namespace Blackjack
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            GameBoard board = new GameBoard();
            board.PrintBoard();
        }
    }
}