using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    public static class App
    {
        public static void RunApplication()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            if (OperatingSystem.IsWindows())
            {
                Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
            }
            else
            {
                Console.WriteLine("Only supported on windows!");
                Environment.Exit(0);
            }
            FileManager.CreateDirectory();
            FileManager.CreateFile();
            BlackJack blackjack = new();
            blackjack.RunGame(Utilities.GetPlayers());
        }
    }
}
