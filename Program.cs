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

            Graphics table = new Graphics();
            table.PrintBoard();
            Graphics.PrintCard(Deck.AllCards[5]);

            Console.ReadKey();
            
            BlackJack blackjack = new(new Graphics());
            Player playerOne = new Player("Kimmo");

            blackjack.RunGame(playerOne);
        }
    }
}