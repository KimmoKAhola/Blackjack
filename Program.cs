namespace Blackjack
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WindowHeight = 50;
            Console.WindowWidth = 200;

            GameBoard board = new();
            Card aceOfSpades = new(" _____", "|A .  |", "| /.\\ |", "|(_._)|", "|  |  |", "|____V|");
            board.PrintBoard();
            board.PrintCards(aceOfSpades);
        }
    }
}