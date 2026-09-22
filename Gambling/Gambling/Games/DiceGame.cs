using System;
using System.Collections.Generic;
using System.Text;
using Gambling.DiceData;

namespace Gambling.Games;

public class DiceGame : CasinoGameBase
{
    private readonly List<Dice> _dice;
    private readonly int _diceCount;
    public DiceGame(int diceCount, int minValue, int maxValue)
    {
        if (diceCount < 1)
        {
            throw new ArgumentException("Dice count must be greater than zero.");
        }
        _diceCount = diceCount;
        _dice = new List<Dice>();
        CreateDice(minValue, maxValue);
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
    }

    private void CreateDice(int minValue, int maxValue)
    {
        for (int i = 0; i < _diceCount; i++)
        {
            _dice.Add(new Dice(minValue, maxValue));
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