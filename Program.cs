namespace Blackjack
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WindowHeight = 50;
            Console.WindowWidth = 200;
            Console.CursorVisible = false;

            BlackJack blackjack = new(new Graphics());
            Player playerOne = new Player("Kimmo");

            blackjack.RunGame(playerOne);
        }
    }
}