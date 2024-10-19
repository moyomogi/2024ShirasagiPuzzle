using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

// グループ分け
public class UnionFind
{
    int n;
    int[] par;
    public UnionFind(int n)
    {
        this.n = n;
        par = new int[n];
        for (int i = 0; i < n; i++) par[i] = -1;
    }
    public int this[int x]
    {
        get
        {
            Debug.Assert(0 <= x && x < n);
            return par[x] < 0 ? x : par[x] = this[par[x]];
        }
    }
    public bool Unite(int x, int y)
    {
        Debug.Assert(0 <= x && x < n && 0 <= y && y < n);
        x = this[x]; y = this[y];
        if (x == y) return false;
        if (-par[x] < -par[y]) (x, y) = (y, x);
        par[x] += par[y];
        par[y] = x;
        return true;
    }
    public int Size(int x)
    {
        Debug.Assert(0 <= x && x < n);
        return -par[this[x]];
    }
    public bool Same(int x, int y)
    {
        Debug.Assert(0 <= x && x < n && 0 <= y && y < n);
        return this[x] == this[y];
    }
}


public class GameManager : MonoBehaviour
{
    // ゲームマネージャーを作ってみよう https://youtu.be/JyrBl-06FAs?list=PLED8667EEZ9aB72WVMHfRHBd6oj9vplRy
    // GameManager とは、Scene を移動しても消滅させたくない変数を置く場所です
    public static GameManager instance { get; private set; }

    public AudioClip[] bgm;
    private int bgmIdx = -1;
    AudioSource bgmAudioSource;

    public bool shouldLoad = false;
    public bool shouldRepositionPlayer = false;
    public float[] playerPosition = new float[3];
    // Awake には初期化処理を書く (Start より先に実行されるため)
    private void Awake()
    {
        if (!instance)
        {
            // 未生成
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // 生成済み
            Destroy(gameObject);
        }
        bgmAudioSource = gameObject.AddComponent<AudioSource>();
    }

    public void Init()
    {
        // Init
        GameManager.instance.shouldRepositionPlayer = false;
        for (int i = 0; i < 3; i++)
        {
            GameManager.instance.playerPosition[i] = 0.0f;
        }
    }

    // Update は描画前に実行される
    private void Update()
    {
        int newBgmIdx = -1;
        switch (SceneManager.GetActiveScene().name)
        {
            case "TitleScene":
                newBgmIdx = 0;
                break;
            case "Stage1_1":
                newBgmIdx = 1;
                break;
            default:
                newBgmIdx = 1;
                break;
        }
        if (bgmIdx != newBgmIdx)
        {
            bgmIdx = newBgmIdx;
            PlayBGM(bgmIdx);
        }

        // If there is no save data file, save the game
        if (!File.Exists(Application.persistentDataPath + "/save/data.dat"))
        {
            SaveManager.Save();
            return;
        }
        // Press F2 to return to title scene
        if (Input.GetKeyDown(KeyCode.F2))
        {
            // Load TitleScene.unity
            SceneManager.LoadScene("TitleScene");
            // SceneManager.LoadScene("TitleScene");
            return;
        }
        // Esc でゲーム終了
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
            return;
        }
#if UNITY_EDITOR
        // P で Save (For debugging)
        if (Input.GetKeyDown(KeyCode.P))
        {
            SaveManager.Save();
        }
#endif
        // R で Retry
        if (Input.GetKeyDown(KeyCode.R) || GameManager.instance.shouldLoad)
        {
            LoadManager.Load();
        }
    }

    private void PlayBGM(int idx)
    {
        if (idx == -1)
        {
            bgmAudioSource.Stop();
        }
        else
        {
            bgmAudioSource.clip = bgm[idx];
            bgmAudioSource.loop = true;
            bgmAudioSource.volume = 0.8f;
            bgmAudioSource.Play();
        }
    }
}
