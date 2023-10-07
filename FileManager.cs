using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    public static class FileManager
    {
        //Use this class to save the match history.
        private static string _directoryFilePath = "../../../MatchHistory";
        private static string _filePath = $"../../../MatchHistory/RENAME_ME.txt";


        public static void CreateFile()
        {
            if(!File.Exists(_filePath))
            {
                File.Create( _filePath);
            }
        }

        public static void CreateDirectory()
        {
            if (!Directory.Exists(_directoryFilePath))
            {
                Directory.CreateDirectory(_directoryFilePath);
            }
        }

        public static void SaveStartTimeStamp(BlackJackGameHistory blackJackGameHistory)
        {
            using StreamWriter writer = new(_filePath, append: true);
            DateTime startTime = blackJackGameHistory.TimeStamp;
            writer.WriteLine(startTime);
        }

        public static void SaveHandInfo(string handInfo)
        {
            using StreamWriter writer = new(_filePath, append: true);
            writer.WriteLine(handInfo);
        }
        public static void SaveFirstDealInfo(string firstDealInfo)
        {
            using StreamWriter writer = new(_filePath, append: true);
            writer.WriteLine(firstDealInfo);
        }
        public static void SavePlayerWallet(string wallet)
        {
            using StreamWriter writer = new(_filePath, append: true);
            writer.WriteLine(wallet);
        }

        public static void SaveGameInfo(int gameId, BlackJackGameHistory blackJackGameHistory) 
        {
            using StreamWriter writer = new(_filePath, append: true);
            DateTime startTime = blackJackGameHistory.TimeStamp;
            writer.WriteLine($"{startTime}, Game ID [{gameId}]");
        }


    }
}
