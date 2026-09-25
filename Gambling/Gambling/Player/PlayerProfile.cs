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
    public PlayerProfile(string name, int bank)
    {
        Name = name;
        Bank = bank >= 0 ? bank : 0;
    }
    public void AddMoney(int amount)
    {
        if (amount > 0)
        {
            Bank += amount;
        }
    }
    public void RemoveMoney(int amount)
    {
        if (amount > 0 && amount <= Bank)
        {
            Bank -= amount;
        }
    }

    public void SetBank(int amount)
    {
        if (amount >= 0)
        {
            Bank = amount;
        }
    }
}