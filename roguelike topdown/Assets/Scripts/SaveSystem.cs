using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem
{
    public static void Save(GameObject player, UpgradeMenu upgrades, Director director, GameOverMenu gameOver)
    {
        BinaryFormatter bf = new BinaryFormatter();
        string path = Application.persistentDataPath + "/GameState.fvn";
        FileStream stream = new FileStream(path, FileMode.Create);

        SaveData data = new SaveData(player, upgrades, director, gameOver);

        bf.Serialize(stream, data);
        stream.Close();

        Debug.Log($"data saved to {path}");
        Debug.Log(data);
    }   

    public static SaveData Load()
    {
        string path = Application.persistentDataPath + "/GameState.fvn";
        if(File.Exists(path))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SaveData data = bf.Deserialize(stream) as SaveData;
            stream.Close();
            Debug.Log($"data loaded from {path}");
            return data;
        }
        else
        {
            Debug.LogError($"Save file not found in {path}");
            return null;
        }
    }

    public static void Wipe()
    {
        string path = Application.persistentDataPath + "/GameState.fvn";
        if(File.Exists(path))
        {
            File.Delete(path);
            Debug.Log($"data wiped from {path}");
        }
    }
}
