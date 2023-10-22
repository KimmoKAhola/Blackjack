namespace Blackjack
{
    public static class App
    {
        /// <summary>
        /// A class for initializing our application.
        /// Starts the game, loads in files and initializes the blackjack board.
        /// </summary>
        public static void RunApplication()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            if (OperatingSystem.IsWindows())
            {
                Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
                Utilities.ToggleCursorVisibility();
            }
            else
            {
                Console.WriteLine("Only supported on windows!");
                Environment.Exit(0);
            }
            FileManager.CreateDirectory();
            FileManager.CreateFile();
            BlackJack blackjack = new();
            List<Player> players = Utilities.GetPlayers();
            bool developerMode = false; //! DEVELOPER MODE, set to true to use!
            while (true)
            {
                if (developerMode)
                {
                    Player kimmo = new("Kimmo", 10000);
                    Player william = new("William", 10000);
                    Player mille = new("Mille", 10000);
                    List<Player> debugPlayers = new() { kimmo, william, mille };
                    blackjack.RunGame(debugPlayers);
                }
                else
                {
                    blackjack.RunGame(players);
                }
            }
        }
    }
}
