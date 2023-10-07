using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    internal class FileManager
    {
        //Use this class to save the match history.
        private static string _directoryFilePath = "../../../Files";
        public static void CreateFilesDirectory()
        {
            if (!Directory.Exists(_directoryFilePath))
            {
                Directory.CreateDirectory(_directoryFilePath);
            }
        }

        public static void SaveGameInfo(BlackJackGameHistory blackJackGameHistory) 
        {

            Console.ReadKey();
            using (StreamWriter writer = new StreamWriter(_directoryFilePath, append: false))
            {
                writer.Write("");
            }
        }


    }
}
