using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    /// <summary>
    /// A class for saving all the game information to different files on the computer.
    /// </summary>
    public static class FileManager
    {
        private static string _directoryFilePath = "../../../MatchHistory";
        private static string _filePath = $"../../../MatchHistory/RENAME_ME.txt";


        public static void CreateFile()
        {
            if(!File.Exists(_filePath))
            {
                FileStream fs = File.Create( _filePath);
                fs.Close();
            }
        }

        public static void CreateDirectory()
        {
            if (!Directory.Exists(_directoryFilePath))
            {
                Directory.CreateDirectory(_directoryFilePath);
            }
        }

        public static void SaveStartTimeStamp(int gameId)
        {
            using StreamWriter writer = new(_filePath, append: true);
            DateTime startTime = DateTime.Now;
            writer.WriteLine($"Round start time: {startTime}---match id [{gameId}]");
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
    }
}
