using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem
{
    public static void Save(GameObject player, UpgradeMenu upgrades, Director director)
    {
        BinaryFormatter bf = new BinaryFormatter();
        string path = Application.persistentDataPath + "/GameState.fvn";
        FileStream stream = new FileStream(path, FileMode.Create);

        SaveData data = new SaveData(player, upgrades, director);

        bf.Serialize(stream, data);
        stream.Close();

        Debug.Log($"data saved to {path}");
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
}
