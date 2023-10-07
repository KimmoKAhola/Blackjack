using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    internal class FileManager
    {
        //Use this class to save the match history.
        private static string _directoryFilePath = "../../../Files";
        private static string _filePath = "../../../Files/info.txt";

        public static void CreateFile()
        {
            if(!File.Exists(_filePath))
            {
                File.Create( _filePath );
            }
        }
        public static void CreateFilesDirectory()
        {
            if (!Directory.Exists(_directoryFilePath))
            {
                Directory.CreateDirectory(_directoryFilePath);
            }
        }

        public static void GetStartTimeStamp(BlackJackGameHistory blackJackGameHistory)
        {
            using (StreamWriter writer = new StreamWriter(_filePath, append: true))
            {
                DateTime startTime = blackJackGameHistory.TimeStamp;
                writer.WriteLine(startTime);
            }
        }

        public static void GetHandInfo(string handInfo)
        {
            using (StreamWriter writer = new StreamWriter(_filePath, append: true))
            {
                writer.WriteLine(handInfo);
            }
        }

        public static void SaveGameInfo(BlackJackGameHistory blackJackGameHistory) 
        {
            using (StreamWriter writer = new StreamWriter(_filePath, append: true))
            {
                DateTime startTime = blackJackGameHistory.TimeStamp;
                writer.WriteLine(startTime);
            }
        }


    }
}
