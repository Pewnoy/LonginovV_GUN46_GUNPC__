namespace Gambling.Dicedata;

public class WrongDiceNumberException : Exception
{
    public WrongDiceNumberException(string message)
        : base(message)
    {
    }
}