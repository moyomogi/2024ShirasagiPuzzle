using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;

public static class SaveManager
{
    public static void Save()
    {
        string saveFolderPath = Application.persistentDataPath + "/save";
        if (!Directory.Exists(saveFolderPath))
        {
            Directory.CreateDirectory(saveFolderPath);
        }
        string saveFilePath = saveFolderPath + "/data.dat";
        Debug.Log("Save to " + saveFilePath);

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(saveFilePath);

        // セーブデータ取得
        SaveData saveData = new SaveData();
        saveData.sceneName = SceneManager.GetActiveScene().name;

        // Output
        bf.Serialize(file, saveData);
        file.Close();
    }
}
