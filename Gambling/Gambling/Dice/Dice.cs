namespace Gambling.Dicedata;

public readonly struct Dice
{
    private readonly int _min;
    private readonly int _max;
    private static readonly Random Random = new();
    public int Number
    {
        get
        {
            Random random = new Random();
            return random.Next(_min, _max + 1);
        }
    }

    public Dice(int min, int max)
    {
        if (min < 1 || min > int.MaxValue)
        {
            throw new WrongDiceNumberException($"Wrong min number: {min}. Allowed range: 1 - {int.MaxValue}");
        }

        if (max < 1 || max > int.MaxValue || min > max)
        {
            throw new WrongDiceNumberException($"Wrong max number: {max}. Allowed range: {min} - {int.MaxValue}");
        }
        
        _min = min;
        _max = max;
    }
}