using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveManager : ManagerBase<SaveManager>
{
    private string filePath;
    
    public override void Awake()
    {
        base.Awake();

        filePath = Application.persistentDataPath + "/blood&stone.save";
    }

    public void SaveGame(SaveData saveData)
    {
        FileStream fileStream = new FileStream(filePath, FileMode.Create);
        BinaryFormatter formatter = new BinaryFormatter();
        formatter.Serialize(fileStream, saveData);
        fileStream.Close();
    }

    public SaveData LoadGame()
    {
        if (File.Exists(filePath)) // If a file exists at this path (i.e. is there a save game file)
        {
            FileStream fileStream = new FileStream(filePath, FileMode.Open);
            BinaryFormatter formatter = new BinaryFormatter();
            SaveData saveData = formatter.Deserialize(fileStream) as SaveData;
            fileStream.Close();
            return saveData;
        }
        else
        {
            Debug.LogError("Save file not found at " + filePath);
            return null;
        }
    }
}
