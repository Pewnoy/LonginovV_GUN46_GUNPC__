using System;
using System.Collections.Generic;
using System.Text;
using Gambling.Cards;

namespace Gambling.Games;

public class BlackjackGame : CasinoGameBase
{
    private readonly Queue<Card> Deck;
    private readonly int _cardCount;
    public BlackjackGame(int cardCount)
    {
        if (cardCount < 4)
        {
            throw new ArgumentException("Card count must be at least 4.");
        }

        _cardCount = cardCount;
        Deck = new Queue<Card>();
    }

    public override void PlayGame()
    {
        Shuffle();

        List<Card> playerCards = new();
        List<Card> computerCards = new();

        playerCards.Add(Deck.Dequeue());
        playerCards.Add(Deck.Dequeue());

        computerCards.Add(Deck.Dequeue());
        computerCards.Add(Deck.Dequeue());

        int playerScore = CalculateScore(playerCards);
        int computerScore = CalculateScore(computerCards);

        Console.WriteLine("Player cards:");

        foreach (Card card in playerCards)
        {
            Console.WriteLine($"{card.Value} of {card.Suit}");
        }

        Console.WriteLine($"Player score: {playerScore}");
        Console.WriteLine("Computer cards:");

        foreach (Card card in computerCards)
        {
            Console.WriteLine($"{card.Value} of {card.Suit}");
        }

        Console.WriteLine($"Computer score: {computerScore}");

        CheckResult(playerScore, computerScore);
    }

    protected override void FactoryMethod()
    {
        List<Card> cards = new();

        foreach (CardSuit suit in Enum.GetValues<CardSuit>())
        {
            foreach (CardValue value in Enum.GetValues<CardValue>())
            {
                cards.Add(new Card(suit, value));
            }
        }

        Shuffle(cards);
    }

    private void Shuffle()
    {
        Random random = new();

        List<Card> cards = new(Deck);

        Deck.Clear();

        while (cards.Count > 0)
        {
            int index = random.Next(cards.Count);
            Deck.Enqueue(cards[index]);
            cards.RemoveAt(index);
        }
    }

    private void Shuffle(List<Card> cards)
    {
        foreach (Card card in cards)
        {
            Deck.Enqueue(card);
        }
    }

    private int CalculateScore(List<Card> cards)
    {
        int score = 0;
        foreach (Card card in cards)
        {
            score += (int)card.Value;
        }

        return score;
    }

    private void CheckResult(int playerScore, int computerScore)
    {
        if (playerScore > 21 && computerScore > 21)
        {
            OnDrawInvoke();
        }
        else if (playerScore > 21)
        {
            OnLooseInvoke();
        }
        else if (computerScore > 21 || playerScore > computerScore)
        {
            OnWinInvoke();
        }
        else if (playerScore < computerScore)
        {
            OnLooseInvoke();
        }
        else
        {
            OnDrawInvoke();
        }
    }
}