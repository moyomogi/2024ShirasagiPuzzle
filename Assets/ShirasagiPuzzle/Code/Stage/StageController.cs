using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageController : MonoBehaviour
{
    // public GameObject gameoverText;
    private GameObject _player;
    // private SpriteRenderer _player_sr;
    // public BossScript boss;
    // public GameObject gameClearUI;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        if (_player == null)
        {
            Debug.Log("_player == null");
            return;
        }
        // GameObject sa = _player.transform.Find("SpriteAnimator").gameObject;
        // if (sa == null)
        // {
        //     Debug.Log("sa == null");
        //     return;
        // }
        // _player_sr = sa.GetComponent<SpriteRenderer>();
    }
    public void GameOver()
    {
        // gameoverText.SetActive(true);
        Invoke("GameRestart", 1f);
    }
    // public void GameClear()
    // {
    //     // gameClearUI.SetActive(true);
    // }
    public void GameRestart()
    {
        GameManager.instance.shouldLoad = true;
    }
    private void Update()
    {
        if (_player == null)
        {
            Debug.LogError("_player == null");
            return;
        }
        if (_player.transform.position.y < -20.5)
        {
            GameManager.instance.PlaySound("death");
            GameManager.instance.shouldLoad = true;
        }
        // if (player.getIsDead()) GameOver();
        // if (!_player_sr.isVisible)
        // {
        //     GameManager.instance.shouldLoad = true;
        // }
    }
}
