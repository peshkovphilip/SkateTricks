using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class Game
{
    public static void SaveGame(PlayerData _playerData)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/progress.dat", FileMode.OpenOrCreate);
        Debug.Log("save game");
        bf.Serialize(file, _playerData);
        file.Close();
    }
    
    public static PlayerData LoadGame()
    {
        if (File.Exists(Application.persistentDataPath + "/progress.dat"))
        {
            Debug.Log("load game");
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/progress.dat", FileMode.Open);
            PlayerData _playerData = (PlayerData) bf.Deserialize(file);
            file.Close();
            return _playerData;
        }
        else
        {
            return new PlayerData();
        }
    }

    public static bool RestartGame = false;

}
