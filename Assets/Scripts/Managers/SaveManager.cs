using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveManager : ManagerBase<SaveManager>
{
    public string saveFilePath;
    public string newGameDataPath;
    
    public override void Awake()
    {
        base.Awake();

        saveFilePath = Application.persistentDataPath + "/blood&stone.save";
        newGameDataPath = Application.persistentDataPath + "default.save";
    }

    private void Start()
    {
        StoreNewGameData();
    }

    public void SaveGame(SaveData saveData, string filePath)
    {
        FileStream fileStream = new FileStream(filePath, FileMode.Create);
        BinaryFormatter formatter = new BinaryFormatter();
        formatter.Serialize(fileStream, saveData);
        fileStream.Close();
    }

    public SaveData LoadGame(string filePath)
    {
        if (SaveFileExists(filePath)) // If a file exists at this path (i.e. is there a save game file)
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

    public bool SaveFileExists(string filePath)
    {
        return File.Exists(filePath);
    }

    public void StoreNewGameData()
    {
        PlayerManager.Instance.SetSaveData();
        SaveGame(PlayerManager.Instance.saveData, newGameDataPath);
    }
}
