using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ElecManager : MonoBehaviour
{
    public static ElecManager instance { get; private set; }

    private const int UNION_FIND_SIZE = 1024;
    private UnionFind ufElec;
    private Dictionary<int, int> elecInstanceIdToIdx = new Dictionary<int, int>();
    private List<Elec> elecs = new List<Elec>();

    // 放電物体の index。具体的には Player, LigntningBox が該当
    private List<int> LightningIndices = new List<int>();

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

        string[] elecTags = { "Elec", "ElecBackground", "LightningBox" };
        foreach (string elecTag in elecTags)
        {
            GameObject[] elecGameObjs = GameObject.FindGameObjectsWithTag(elecTag);
            foreach (GameObject elecGameObj in elecGameObjs)
            {
                Elec elecComponent = elecGameObj.GetComponent<Elec>();
                // Check if the component is not null before adding
                if (elecComponent != null)
                {
                    if (elecTag == "LightningBox") LightningIndices.Add(elecs.Count);
                    elecs.Add(elecComponent);
                }
                else
                {
                    Debug.Log($"GameObject with tag {elecTag} does not have an Elec component.");
                }
            }
        }

        // elecs = GameObject.FindGameObjectsWithTag("Elec").GetComponent<Elec>();
        for (int i = 0; i < elecs.Count; i++)
        {
            Elec elec = elecs[i];
            int elecId = elec.gameObject.GetInstanceID();
            elecInstanceIdToIdx.Add(elecId, i);
            // Debug.Log($"[Start] {EditorUtility.InstanceIDToObject(elecId).name}({elecId}), {i}");
        }

        // player は class Elec の継承が難しいため、別で処理
        int playerIdx = elecs.Count;
        LightningIndices.Add(playerIdx);
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        int playerId = player.gameObject.GetInstanceID();
        elecInstanceIdToIdx.Add(playerId, playerIdx);

        InitUfElec();
    }

    private void InitUfElec()
    {
        ufElec = new UnionFind(UNION_FIND_SIZE);
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
        bool[] isLightningList = new bool[elecs.Count];
        for (int i = 0; i < elecs.Count; i++)
        {
            isLightningList[i] = false;
        }
        for (int i = 0; i < elecs.Count; i++)
        {
            Elec elec = elecs[i];
            foreach (int idx in LightningIndices)
            {
                // 発電物体と同じグループなら光る
                bool same = UfElecSameByIndices(i, idx);
                isLightningList[i] = isLightningList[i] || same;
            }
        }
        for (int i = 0; i < elecs.Count; i++)
        {
            Elec elec = elecs[i];
            if (isLightningList[i])
            {
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
