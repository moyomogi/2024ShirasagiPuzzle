using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class GameManager : MonoBehaviour
{
    // ゲームマネージャーを作ってみよう https://youtu.be/JyrBl-06FAs?list=PLED8667EEZ9aB72WVMHfRHBd6oj9vplRy
    // GameManager とは、Scene を移動しても消滅させたくない変数を置く場所です
    public static GameManager instance { get; private set; }

    private string bgmName = "";
    public SerializableDictionary<string, AudioClip> bgmDict;
    private AudioSource bgmAudioSource;
    public SerializableDictionary<string, AudioClip> soundDict;
    private AudioSource soundAudioSource;

    public bool shouldLoad = false;
    public bool hasKey = false;
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
        soundAudioSource = gameObject.AddComponent<AudioSource>();
    }

    public void Init()
    {
        // Init
        GameManager.instance.hasKey = false;
        GameManager.instance.shouldRepositionPlayer = false;
        for (int i = 0; i < 3; i++)
        {
            GameManager.instance.playerPosition[i] = 0.0f;
        }
        GameManager.instance.shouldLoad = false;
    }

    // Update は描画前に実行される
    private void Update()
    {
        string newBgmName = "";
        switch (SceneManager.GetActiveScene().name)
        {
            case "TitleScene":
                newBgmName = "anohi";
                break;
            case "Stage1_1":
                newBgmName = "voyage";
                break;
            default:
                newBgmName = "osananajimi";
                break;
        }
        if (bgmName != newBgmName)
        {
            bgmName = newBgmName;
            PlayBGM(bgmName);
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

    // BGM: ループ、重複しない
    private void PlayBGM(string bgmName)
    {
        if (bgmDict.ContainsKey(bgmName))
        {
            bgmAudioSource.clip = bgmDict[bgmName];
            bgmAudioSource.loop = true;
            bgmAudioSource.volume = 0.8f;
            bgmAudioSource.Play();
        }
        else
        {
            Debug.LogError($"bgmDict にキー {bgmName} の要素がありません");
        }
    }
    // 効果音: ループしない、重複する
    public void PlaySound(string soundName)
    {
        if (soundDict.ContainsKey(soundName))
        {
            soundAudioSource.clip = soundDict[soundName];
            soundAudioSource.loop = false;
            soundAudioSource.volume = 0.8f;
            soundAudioSource.Play();
        }
        else
        {
            Debug.LogError($"soundDict にキー {soundName} の要素がありません");
        }
    }
}

// https://zenn.dev/tmb/articles/9b4c532da8d467
[Serializable]
public class SerializableDictionary<TKey, TValue> :
    Dictionary<TKey, TValue>,
    ISerializationCallbackReceiver
{
    [Serializable]
    public class Pair
    {
        public TKey key = default;
        public TValue value = default;

        /// <summary>
        /// Pair
        /// </summary>
        public Pair(TKey key, TValue value)
        {
            this.key = key;
            this.value = value;
        }
    }

    [SerializeField]
    private List<Pair> _list = null;

    /// <summary>
    /// OnAfterDeserialize
    /// </summary>
    void ISerializationCallbackReceiver.OnAfterDeserialize()
    {
        Clear();
        foreach (Pair pair in _list)
        {
            if (ContainsKey(pair.key))
            {
                continue;
            }
            Add(pair.key, pair.value);
        }
    }

    /// <summary>
    /// OnBeforeSerialize
    /// </summary>
    void ISerializationCallbackReceiver.OnBeforeSerialize()
    {
        // 処理なし
    }
}

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
