using System;
using System.Collections.Generic;
using Gambling.Dicedata;

namespace Gambling.Games;

public class DiceGame : CasinoGameBase
{
    private readonly List<Dice> _dice;
    private readonly int _diceCount;
    private readonly int _minValue;
    private readonly int _maxValue;
    public DiceGame(int diceCount, int minValue, int maxValue)
    {
        if (diceCount < 1)
        {
            throw new ArgumentException("Dice count must be greater than zero.");
        }
        _diceCount = diceCount;
        _minValue = minValue;
        _maxValue = maxValue;
        _dice = new List<Dice>();

        FactoryMethod();
    }

    public override void PlayGame()
    {
        int playerScore = RollDice();
        int computerScore = RollDice();

        Console.WriteLine($"Player score: {playerScore}");
        Console.WriteLine($"Computer score: {computerScore}");

        CheckResult(playerScore, computerScore);
    }
    protected override void FactoryMethod()
    {
        for (int i = 0; i < _diceCount; i++)
        {
            _dice.Add(new Dice(_minValue, _maxValue));
        }
    }
    private int RollDice()
    {
        int result = 0;
        foreach (Dice dice in _dice)
        {
            result += dice.Number;
        }

        return result;
    }

    private void CheckResult(int playerScore, int computerScore)
    {
        if (playerScore > computerScore)
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