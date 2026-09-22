using System;
using System.Collections.Generic;
using System.Text;

namespace Gambling.DiceData;

public class WrongDiceNumberException : Exception
{
    public WrongDiceNumberException(string message)
        : base(message)
    {
    }
}