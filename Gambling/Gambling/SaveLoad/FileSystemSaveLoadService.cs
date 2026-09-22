using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Gambling.SaveLoad;

public class FileSystemSaveLoadService : ISaveLoadService<string>
{
    private readonly string _path;
    public FileSystemSaveLoadService(string path)
    {
        _path = path;

        if (!Directory.Exists(_path))
        {
            Directory.CreateDirectory(_path);
        }
    }

    public void SaveData(string data, string identifier)
    {
        string filePath = Path.Combine(_path, identifier + ".txt");
        File.WriteAllText(filePath, data);
    }

    public string LoadData(string identifier)
    {
        string filePath = Path.Combine(_path, identifier + ".txt");
        if (!File.Exists(filePath))
        {
            return string.Empty;
        }

        return File.ReadAllText(filePath);
    }
    public void DeleteData(string identifier)
    {
        string filePath = Path.Combine(_path, identifier + ".txt");

        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
    }
}