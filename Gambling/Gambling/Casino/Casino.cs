using System;
using System.Collections.Generic;
using System.Text;
using System;
using Gambling.Games;
using Gambling.Player;
using Gambling.SaveLoad;

namespace Gambling.Casino;

public class Casino : IGame
{
    private readonly ISaveLoadService<string> _saveService;
    private PlayerProfile? _player;
    private readonly BlackjackGame _blackjack;
    private readonly DiceGame _diceGame;
    public Casino()
    {
        _saveService = new FileSystemSaveLoadService("Saves");
        _blackjack = new BlackjackGame(36);
        _diceGame = new DiceGame(2, 1, 6);

        _blackjack.OnWin += WinMessage;
        _blackjack.OnLoose += LooseMessage;
        _blackjack.OnDraw += DrawMessage;

        _diceGame.OnWin += WinMessage;
        _diceGame.OnLoose += LooseMessage;
        _diceGame.OnDraw += DrawMessage;
    }
    public void StartGame()
    {
        Console.WriteLine("Welcome to Casino!");
        LoadPlayer();
        Console.WriteLine($"Hello, {_player!.Name}");
        Console.WriteLine($"Your bank: {_player.Bank}");
        ChooseGame();
        SavePlayer();

        Console.WriteLine("Goodbye!");
    }
    private void LoadPlayer()
    {
        string data = _saveService.LoadData("Player");
        if (string.IsNullOrEmpty(data))
        {
            Console.Write("Enter your name: ");
            string? name = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(name))
            {
                name = "Unknown";
            }
            _player = new PlayerProfile(name);
        }
        else
        {
            string[] playerData = data.Split(';');
            _player = new PlayerProfile( playerData[0], Convert.ToInt32(playerData[1]));
        }
    }
    private void ChooseGame()
    {
        Console.WriteLine("Choose game:");
        Console.WriteLine("1 - Blackjack");
        Console.WriteLine("2 - Dice");

        string? input = Console.ReadLine();
        Console.WriteLine($"Your bank: {_player!.Bank}");
        Console.Write("Enter your bet: ");

        int bet = Convert.ToInt32(Console.ReadLine());
        _currentBet = bet;
        if (bet <= 0 || bet > _player.Bank)
        {
            Console.WriteLine("Invalid bet.");

            return;
        }
        _player.RemoveMoney(bet);
        if (input == "1")
        {
            _blackjack.PlayGame();
        }
        else if (input == "2")
        {
            _diceGame.PlayGame();
        }
    }
    private void SavePlayer()
    {
        if (_player != null)
        {
            string data = $"{_player.Name};{_player.Bank}";
            _saveService.SaveData(data, "Player");
        }
    }
    private int _currentBet;
    private void WinMessage()
    {
        Console.WriteLine("You win!");
        _player!.AddMoney(_currentBet * 2);
        Console.WriteLine($"Your bank: {_player.Bank}");
    }

    private void LooseMessage()
    {
        Console.WriteLine("You lose!");
        Console.WriteLine($"Your bank: {_player!.Bank}");
    }

    private void DrawMessage()
    {
        Console.WriteLine("Draw!");
        _player!.AddMoney(_currentBet);
        Console.WriteLine($"Your bank: {_player.Bank}");
    }
}