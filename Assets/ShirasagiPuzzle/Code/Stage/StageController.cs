using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageController : MonoBehaviour
{
    // public GameObject gameoverText;
    // public PlayerScript player;
    // public BossScript boss;
    // public GameObject gameClearUI;

    public void GameOver()
    {
        // gameoverText.SetActive(true);
        Invoke("GameRestart", 1f);
    }
    public void GameClear()
    {
        // var obj = GameObject.Find("BossCanvas");
        // if (obj != null) obj.SetActive(false);
        // gameClearUI.SetActive(true);
    }
    public void GameRestart()
    {
        GameManager.instance.shouldLoad = true;
    }
    private void Update()
    {
        // if (player.getIsDead()) GameOver();
        // if (SceneManager.GetActiveScene().name == "BossScene")
        // {
        //     // if (boss.IsBossDead()) GameClear();
        // }
    }
}
