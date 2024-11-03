using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;
using System.Linq;

public static class LoadManager
{
    public static void Load()
    {
        string saveFolderPath = Application.persistentDataPath + "/save";
        string saveFilePath = saveFolderPath + "/data.dat";

        // Init
        GameManager.instance.Init();

        if (!File.Exists(saveFilePath))
        {
            Debug.Log("No save data");
            SceneManager.LoadScene(GameManager.FIRST_SCENE_NAME);
            // SceneManager.LoadScene("Stage1_1");
            return;
        }
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(saveFilePath, FileMode.Open);

        // Input
        SaveData saveData = (SaveData)bf.Deserialize(file);

        // if (SceneManager.GetActiveScene().name != saveData.sceneName)
        // {
        Debug.Log("Transition to " + saveData.sceneName);
        SceneManager.LoadScene(saveData.sceneName);
        // }

        GameManager.instance.playerPosition = saveData.playerPosition;
        // Debug.Log($"(L) x: {saveData.playerPosition[0]}, y: {saveData.playerPosition[1]}");

        file.Close();

        GameManager.instance.shouldRepositionPlayer = true;
    }
}
