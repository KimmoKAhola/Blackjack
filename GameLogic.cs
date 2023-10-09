using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Blackjack
{
    public static class GameLogic
    {

        public static bool WinCondition()
        {

            return false;
        }

        public static bool CheckForBlackJack(Player currentPlayer)
        {
            //This is a win condition
            if (currentPlayer.HandSum() == 21)
            {
                //This is blackjack after first deal
                if (currentPlayer.Hand.Count == 2)
                {
                    return true;
                }

            }
            return false;
        }

        public static bool CheckForBust(Player currentPlayer)
        {
            //This is if the players hand exceeds 21 
            if (currentPlayer.HandSum() > 21)
            {
                return true;
            }
            return false;
        }
    }
}
