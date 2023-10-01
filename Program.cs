namespace Blackjack
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WindowHeight = 50;
            Console.WindowWidth = 200;

            //PlayingTable board = new();
            Card aceOfSpades = new("╭─────╮", "│A .  │", "│ /.\\ │", "│(_._)│", "│  │ V│", "╰─────╯");
            //board.PrintBoard();
            ////board.PrintCards(aceOfSpades);
            //Deck.PrintAllCards();
            //Console.ReadKey();
            //Deck.ShuffleDeck();
            //Console.ReadKey();
            //Deck.PrintAllCards();
            //Console.ReadKey();

            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.ForegroundColor = ConsoleColor.Black;
            Graphics table = new Graphics();
            table.PrintBoard();
            Graphics.PrintCards(aceOfSpades);

            Console.ReadKey();

            //PlayingTable table = new PlayingTable();
            List<int> dealerHand = new List<int>();
            List<int> playerOneHand = new List<int>();
            
            BlackJack blackjack = new(new Graphics());
            Player playerOne = new Player("Kimmo");

            

            blackjack.RunGame(playerOne);
        }
    }
}