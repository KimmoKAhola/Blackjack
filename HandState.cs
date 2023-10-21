namespace Blackjack
{
    /// <summary>
    /// Enum which has states for each hand at the table.
    /// UNDECIDED, STOP, LOST, BUST, WIN, BLACKJACK
    /// </summary>
    public enum HandState
    {
        UNDECIDED = 1,
        STANDS,
        LOST,
        BUSTS,
        WIN,
        BLACKJACK
    }
}
