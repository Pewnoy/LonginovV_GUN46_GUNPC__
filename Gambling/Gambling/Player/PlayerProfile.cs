using System;
using System.Collections.Generic;
using System.Text;

namespace Gambling.Player;

public class PlayerProfile
{
    public string Name { get; private set; }

    public int Bank { get; private set; }

    public PlayerProfile(string name)
    {
        Name = name;
        Bank = 1000;
    }
}