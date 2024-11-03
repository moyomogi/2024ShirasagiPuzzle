using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class TitleMenuManager : MonoBehaviour
{
    public GameObject[] buttonObjects = new GameObject[3];

    private bool newGameButtonClicked = false, loadGameButtonClicked = false;
    private int index = -1;
    void Awake()
    {
        // Setup buttonObjects
        buttonObjects[0] = GameObject.Find("NewGameButton");
        buttonObjects[1] = GameObject.Find("LoadGameButton");
        buttonObjects[2] = GameObject.Find("ExitGameButton");

        if (File.Exists(Application.persistentDataPath + "/save/data.dat"))
        {
            index = 1;
        }
    }
    private void Update()
    {
        bool isPressedLeft = Input.GetKeyDown(KeyCode.LeftArrow);
        bool isPressedRight = Input.GetKeyDown(KeyCode.RightArrow);
        // Left
        if (isPressedLeft && !isPressedRight)
        {
            Debug.Log("Left key is pressed");
            index = index == -1 ? 0 : (index + 2) % 3;
            EventSystem.current.SetSelectedGameObject(buttonObjects[index]);
        }
        // Right
        if (!isPressedLeft && isPressedRight)
        {
            Debug.Log("Right key is pressed");
            index = index == -1 ? 0 : (index + 1) % 3;
            EventSystem.current.SetSelectedGameObject(buttonObjects[index]);
        }
    }

    // New Game
    public void OnClickNewGameButton()
    {
        if (newGameButtonClicked) return;
        newGameButtonClicked = true;
        GameManager.instance.PlaySound("decide");
        Debug.Log("New game button is clicked");

        // Delete save data file
        File.Delete(Application.persistentDataPath + "/save/data.dat");
        Debug.Log($"delete {Application.persistentDataPath}/save/data.dat");

        GameManager.instance.Init();
        SceneManager.LoadScene(GameManager.FIRST_SCENE_NAME);
    }
    // Load Game
    public void OnClickLoadGameButton()
    {
        if (loadGameButtonClicked) return;
        loadGameButtonClicked = true;
        GameManager.instance.PlaySound("decide");
        Debug.Log("Load game button is clicked");

        string saveFolderPath = Application.persistentDataPath + "/save";
        string saveFilePath = saveFolderPath + "/data.dat";
        if (!File.Exists(saveFilePath))
        {
            Debug.Log("No save data");
        }
        else
        {
            LoadManager.Load();
        }
    }
    // Exit game
    public void OnClickExitGameButton()
    {
        Debug.Log("Exit game button is clicked");

        Application.Quit();
    }
}
