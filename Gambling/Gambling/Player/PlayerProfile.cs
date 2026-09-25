namespace Gambling.Player;

public class PlayerProfile
{
    public string Name { get; private set; }
    public int Bank { get; private set; }
    public int GamesPlayed { get; private set; }
    public int Wins { get; private set; }
    public int Losses { get; private set; }
    public int Draws { get; private set; }
    public PlayerProfile(string name)
    {
        Name = name;
        Bank = 1000;
    }
    public PlayerProfile(
        string name,
        int bank,
        int gamesPlayed = 0,
        int wins = 0,
        int losses = 0,
        int draws = 0)
    {
        Name = name;
        Bank = bank >= 0 ? bank : 0;
        GamesPlayed = gamesPlayed;
        Wins = wins;
        Losses = losses;
        Draws = draws;
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
    public void AddWin()
    {
        GamesPlayed++;
        Wins++;
    }
    public void AddLoss()
    {
        GamesPlayed++;
        Losses++;
    }
    public void AddDraw()
    {
        GamesPlayed++;
        Draws++;
    }
}