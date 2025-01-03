using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class StageController : MonoBehaviour
{
    // public static StageController instance { get; private set; }
    // public GameObject gameoverText;
    public GameObject _player;
    // private SpriteRenderer _player_sr;
    // public BossScript boss;
    // public GameObject gameClearUI;

    // private void Awake()
    // {
    //     // _player = GameObject.FindGameObjectWithTag("Player");
    //     // if (_player == null)
    //     // {
    //     //     GameManager.instance.shouldLoad = true;
    //     // }
    //     // if (!instance)
    //     // {
    //     //     // 未生成
    //     //     instance = this;
    //     //     DontDestroyOnLoad(gameObject);
    //     // }
    //     // else
    //     // {
    //     //     // 生成済み
    //     //     Destroy(gameObject);
    //     // }
    // }

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        // if (_player == null)
        // {
        //     Debug.LogError("_player == null");
        //     return;
        // }
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
#if UNITY_EDITOR
        bool leftClicked = Input.GetMouseButtonDown(1);
        if (leftClicked)
        {
            Vector3 v = new Vector3(
                Input.mousePosition.x - 1920.0f / 2,
                Input.mousePosition.y - 1080.0f / 2,
                0.0f) / (1080.0f / 32);
            Debug.Log($"v: {v}");
            _player.transform.position = v;
        }
#endif
    }

    private void FixedUpdate()
    {
        if (_player == null)
        {
            Debug.LogError("_player == null");
            return;
        }
        if (_player.transform.position.y < -20)
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
