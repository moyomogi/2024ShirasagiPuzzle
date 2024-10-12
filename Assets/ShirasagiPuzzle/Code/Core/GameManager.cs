using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public bool shouldLoad = false;

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
    }


    // Update は描画前に実行される
    private void Update()
    {
        // Esc でゲーム終了
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
            return;
        }
    }
}
