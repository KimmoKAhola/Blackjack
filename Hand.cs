﻿namespace Blackjack
{
    public class Hand
    {
        /// <summary>
        /// A class containing constructor and properties for each player hand.
        /// Is used to handle bets
        /// </summary>
        public Hand()
        {
            CurrentCards = new();
            Bet = 0;
            HandState = HandState.UNDECIDED;
        }

        public List<Card> CurrentCards { get; set; }
        public HandState HandState { get; set; }
        public int Bet { get; set; }

        /// <summary>
        /// Calculates the hand sum of the current hand.
        /// Contains logic to decide ace values.
        /// </summary>
        /// <returns></returns>
        public int HandSum()
        {
            int sum = 0;
            int aceCount = 0; // To keep track of the number of Aces

            foreach (var card in CurrentCards)
            {
                if (card.Title.Contains("A"))
                {
                    aceCount++;
                    card.Value = 11;
                }
                sum += card.Value;
            }

            // Adjust Ace values if the total sum exceeds 21
            while (aceCount > 0 && sum > 21)
            {
                sum -= 10; // Change the value of an Ace from 11 to 1
                aceCount--;
            }

            return sum;
        }
    }
}
