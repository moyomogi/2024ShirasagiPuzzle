using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ElecManager : MonoBehaviour
{
    public static ElecManager instance { get; private set; }

    private UnionFind ufElec = new UnionFind(128);
    private Dictionary<int, int> elecInstanceIdToIdx = new Dictionary<int, int>();
    private List<Elec> elecs = new List<Elec>();

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
    private void Start()
    {
        Init();
    }
    private void Init()
    {
        elecInstanceIdToIdx = new Dictionary<int, int>();
        elecs = new List<Elec>();

        // string[] elecTags = { "Elec", "ElecBackground", "LightningBox" };
        // string[] elecTags = { "Elec", "ElecBackground", "LightningBox" };
        string[] elecTags = { "Elec", "ElecBackground" };
        foreach (string elecTag in elecTags)
        {
            GameObject[] elecGameObjs = GameObject.FindGameObjectsWithTag(elecTag);
            foreach (GameObject elecGameObj in elecGameObjs)
            {
                elecs.Add(elecGameObj.GetComponent<Elec>());
            }
        }

        // GameObject[] elecGameObjs = GameObject.FindGameObjectsWithTag("Elec");
        // foreach (GameObject elecGameObj in elecGameObjs)
        // {
        //     elecs.Add(elecGameObj.GetComponent<Elec>());
        // }

        // GameObject[] elecBackgroundGameObjs = GameObject.FindGameObjectsWithTag("ElecBackground");
        // foreach (GameObject elecGameObj in elecBackgroundGameObjs)
        // {
        //     elecs.Add(elecGameObj.GetComponent<Elec>());
        // }

        // elecs = GameObject.FindGameObjectsWithTag("Elec").GetComponent<Elec>();
        for (int i = 0; i < elecs.Count; i++)
        {
            Elec elec = elecs[i];
            int elecId = elec.gameObject.GetInstanceID();
            elecInstanceIdToIdx.Add(elecId, i);
            // Debug.Log($"[Start] {EditorUtility.InstanceIDToObject(elecId).name}({elecId}), {i}");
        }
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        int playerId = player.gameObject.GetInstanceID();
        // Debug.Log($"{playerId}");
        elecInstanceIdToIdx.Add(playerId, elecs.Count);
    }

    private void InitUfElec()
    {
        ufElec = new UnionFind(128);
    }
    public void UfElecUniteByIds(int id1, int id2)
    {
        if (!elecInstanceIdToIdx.ContainsKey(id1))
        {
            // Debug.Log($"[Err] {EditorUtility.InstanceIDToObject(id1).name}({id1}), {EditorUtility.InstanceIDToObject(id2).name}({id2})");
            Init();
            return;
        }
        int idx1 = elecInstanceIdToIdx[id1], idx2 = elecInstanceIdToIdx[id2];
        ufElec.Unite(idx1, idx2);
    }
    private bool UfElecSameByIndices(int idx1, int idx2)
    {
        return ufElec.Same(idx1, idx2);
    }
    // FixedUpdate は物理演算 (当たり判定など) の前に実行される
    private void FixedUpdate()
    {
        for (int i = 0; i < elecs.Count; i++)
        {
            Elec elec = elecs[i];
            if (UfElecSameByIndices(i, elecs.Count))
            {
                // Debug.Log($"[FixedUpdate] player is same as {i}");
                elec.TurnOn();
            }
            else
            {
                elec.TurnOff();
            }
        }

        InitUfElec();
    }
}
