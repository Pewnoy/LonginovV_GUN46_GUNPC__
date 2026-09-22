using System;
using System.Collections.Generic;
using System.Text;

namespace Gambling.SaveLoad;

public interface ISaveLoadService<T>
{
    void SaveData(T data, string identifier);

    T LoadData(string identifier);
}