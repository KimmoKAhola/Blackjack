namespace Blackjack
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WindowHeight = 50;
            Console.WindowWidth = 200;
            FileManager.CreateDirectory();
            FileManager.CreateFile();

            BlackJack blackjack = new();

            blackjack.RunGame(Utilities.GetPlayers());
        }
    }
}