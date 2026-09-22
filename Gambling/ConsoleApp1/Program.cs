using Gambling.SaveLoad;
string path = "Saves";
FileSystemSaveLoadService saveService = new FileSystemSaveLoadService(path);
saveService.SaveData("Victor;1000", "Player");
string data = saveService.LoadData("Player");
Console.WriteLine(data);