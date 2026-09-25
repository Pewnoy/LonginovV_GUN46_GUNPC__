using System;
using Gambling.Games;
using Gambling.Player;
using Gambling.SaveLoad;

namespace Gambling.Casino;

public class Casino : IGame
{
    private readonly ISaveLoadService<string> _saveService;
    private PlayerProfile? _player;
    private const int MaxBank = 100000;
    private const int MinBet = 10;
    private const int MaxBet = 10000;
    private readonly BlackjackGame _blackjack;
    private readonly DiceGame _diceGame;
    private string _selectedGame = "";
    private int _currentBet;
    private bool _leaveCasino;
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
        Console.WriteLine("====================");
        Console.WriteLine("       CASINO");
        Console.WriteLine("====================");
        Console.WriteLine();

        string data = _saveService.LoadData("Player");

        LoadPlayer(data);

        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("====================");
            Console.WriteLine("       MAIN MENU");
            Console.WriteLine("====================");
            Console.WriteLine("1 - Play");
            Console.WriteLine("2 - Profile");
            Console.WriteLine("3 - Delete profile");
            Console.WriteLine("4 - Exit");

            string? choice = Console.ReadLine();

            if (choice == "1")
            {
                ChooseGame();
                PlayGameLoop();

                if (_leaveCasino)
                {
                    break;
                }
            }
            else if (choice == "2")
            {
                ShowProfile();
            }
            else if (choice == "3")
            {
                _saveService.DeleteData("Player");
                Console.WriteLine("Profile deleted.");
                return;
            }
            else if (choice == "4")
            {
                break;
            }
            else
            {
                Console.WriteLine("Invalid choice.");
            }
        }
        SavePlayer();
        Console.WriteLine("Goodbye!");
    }
    private void LoadPlayer(string data)
    {
        if (string.IsNullOrEmpty(data))
        {
            Console.Write("Enter your name: ");
            string? name = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(name))
            {
                name = "Unknown";
            }

            _player = new PlayerProfile(name);
            SavePlayer();
        }
        else
        {
            string[] playerData = data.Split(';');
            string name = playerData[0];
            int bank = Convert.ToInt32(playerData[1]);

            int gamesPlayed = 0;
            int wins = 0;
            int losses = 0;
            int draws = 0;

            if (playerData.Length >= 6)
            {
                gamesPlayed = Convert.ToInt32(playerData[2]);
                wins = Convert.ToInt32(playerData[3]);
                losses = Convert.ToInt32(playerData[4]);
                draws = Convert.ToInt32(playerData[5]);
            }
            _player = new PlayerProfile(
                name,
                bank,
                gamesPlayed,
                wins,
                losses,
                draws);
        }
        Console.WriteLine($"Hello, {_player.Name}");
        Console.WriteLine($"Your bank: {_player.Bank}");
    }
    private void ShowProfile()
    {
        Console.WriteLine();
        Console.WriteLine("====================");
        Console.WriteLine("       PROFILE");
        Console.WriteLine("====================");
        Console.WriteLine($"Name: {_player!.Name}");
        Console.WriteLine($"Bank: {_player.Bank}");
        Console.WriteLine($"Games played: {_player.GamesPlayed}");
        Console.WriteLine($"Wins: {_player.Wins}");
        Console.WriteLine($"Losses: {_player.Losses}");
        Console.WriteLine($"Draws: {_player.Draws}");
    }
    private void ChooseGame()
    {
        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("Choose game:");
            Console.WriteLine("1 - Blackjack");
            Console.WriteLine("2 - Dice");

            string? input = Console.ReadLine();

            if (input == "1" || input == "2")
            {
                _selectedGame = input;
                return;
            }
            Console.WriteLine("Invalid game.");
        }
    }
    private void PlayGameLoop()
    {
        while (true)
        {
            PlaySelectedGame();

            if (_leaveCasino)
            {
                break;
            }
            Console.WriteLine();
            Console.WriteLine("1 - Play again");
            Console.WriteLine("2 - Change game");
            Console.WriteLine("3 - Back to main menu");

            string? choice = Console.ReadLine();

            if (choice == "1")
            {
                continue;
            }
            if (choice == "2")
            {
                ChooseGame();
                continue;
            }
            break;
        }
    }
    private void PlaySelectedGame()
    {
        if (_selectedGame != "1" && _selectedGame != "2")
        {
            return;
        }
        Console.WriteLine();
        Console.WriteLine($"Your bank: {_player!.Bank}");
        Console.WriteLine($"Minimum bet: {MinBet}");
        Console.WriteLine($"Maximum bet: {MaxBet}");
        Console.Write("Enter your bet: ");
        if (!int.TryParse(Console.ReadLine(), out int bet))
        {
            Console.WriteLine("Invalid bet.");
            return;
        }
        if (bet < MinBet)
        {
            Console.WriteLine($"Minimum bet is {MinBet}.");
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
            return;
        }
        if (bet > MaxBet)
        {
            Console.WriteLine($"Maximum bet is {MaxBet}.");
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
            return;
        }
        if (bet > _player.Bank)
        {
            Console.WriteLine("You don't have enough money.");
            return;
        }
        _currentBet = bet;
        _player.RemoveMoney(bet);
        if (_selectedGame == "1")
        {
            _blackjack.PlayGame();
        }
        else if (_selectedGame == "2")
        {
            _diceGame.PlayGame();
        }
        SavePlayer();
    }
    private void SavePlayer()
    {
        if (_player != null)
        {
            string data =
                $"{_player.Name};" +
                $"{_player.Bank};" +
                $"{_player.GamesPlayed};" +
                $"{_player.Wins};" +
                $"{_player.Losses};" +
                $"{_player.Draws}";

            _saveService.SaveData(data, "Player");
        }
    }
    private void WinMessage()
    {
        Console.WriteLine("You win!");
        _player!.AddWin();
        _player.AddMoney(_currentBet * 2);
        if (_player.Bank > MaxBank)
        {
            Console.WriteLine("You broke the casino! A new one will be built here.");
            _player.SetBank(MaxBank);
        }
        Console.WriteLine($"Your bank: {_player.Bank}");
    }

    private void LooseMessage()
    {
        Console.WriteLine("You lose!");
        _player!.AddLoss();
        Console.WriteLine($"Your bank: {_player.Bank}");
        if (_player.Bank == 0)
        {
            Console.WriteLine("You have no money left.");
            Console.WriteLine("You are kicked out of the casino.");
            _leaveCasino = true;
        }
    }
    private void DrawMessage()
    {
        Console.WriteLine("Draw!");
        _player!.AddDraw();
        _player.AddMoney(_currentBet);
        Console.WriteLine($"Your bank: {_player.Bank}");
    }
}