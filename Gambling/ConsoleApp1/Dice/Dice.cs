using System;
using System.Collections.Generic;
using System.Text;

namespace Gambling.Dice;
public class WrongDiceNumberException : Exception
{
    public WrongDiceNumberException(string message)
        : base(message)
    {
    }
}