using System.Media;
namespace Blackjack
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //test
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WindowHeight = 50;
            Console.WindowWidth = 200;
            Console.CursorVisible = false;
            FileManager.CreateDirectory();
            FileManager.CreateFile();
            
            BlackJack blackjack = new();

            blackjack.RunGame(Utilities.GetPlayers());
        }
    }
}