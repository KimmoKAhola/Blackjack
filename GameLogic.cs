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
        public static void CheckResults(List<Player> players, Dealer dealer)
        {
            foreach (var player in players)
            {
                if (player.GameState == GameState.BLACKJACK)
                {
                    //Player got blackjack in first deal
                    BlackJack.FunMethod();
                }
                else if (player.HandSum() > 21)
                {
                    player.GameState = GameState.LOSS;
                }
                else if (dealer.HandSum() > 21)
                {
                    BlackJack.FunMethod();
                    player.GameState = GameState.WIN;
                }
                else if (player.HandSum() > dealer.HandSum())
                {
                    player.GameState = GameState.WIN;
                    BlackJack.FunMethod();
                }
                else
                    player.GameState = GameState.LOSS;
            }
        }
        public static void DealersTurn(Dealer dealer)
        {
            while (dealer.HandSum() < 17)
            {
                Deck.DealCard(dealer);

                Graphics.UpdateBoard(dealer);
                Thread.Sleep(1000);
            }
        }
    }

}
