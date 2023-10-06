using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    public static class GameLogic
    {

        public static bool WinCondition()
        {

            return false;
        }

        public static bool CheckFor21(Player currentPlayer, out bool blackJack)
        {
            blackJack = false;
            //This is a win condition
            if (currentPlayer.HandSum() == 21)
            {
                //This is blackjack after first deal
                if (currentPlayer.PlayerHand.Count == 2)
                {
                    blackJack = true;
                }
                return true;
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
