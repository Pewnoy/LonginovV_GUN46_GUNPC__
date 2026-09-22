using System;
using System.Collections.Generic;
using System.Text;

namespace Gambling.DiceGame;

public class WrongDiceNumberException : Exception
{
    public WrongDiceNumberException(string message)
        : base(message)
    {
    }
}