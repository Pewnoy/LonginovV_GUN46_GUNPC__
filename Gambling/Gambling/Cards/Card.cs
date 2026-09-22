namespace Gambling.Cards;
public readonly struct Card
{
    public CardSuit Suit { get; }
    public CardValue Value { get; }
    public Card(CardSuit suit, CardValue value)
    {
        Suit = suit;
        Value = value;
    }
}