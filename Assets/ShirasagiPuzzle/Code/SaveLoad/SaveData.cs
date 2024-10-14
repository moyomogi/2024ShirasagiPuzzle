using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

// How to Build A Save System in Unity https://youtu.be/5roZtuqZyuw
// SAVE & LOAD SYSTEM in Unity https://youtu.be/XOjd_qU2Ido
[System.Serializable]
public class SaveData
{
    // 保存すべき内容
    // - player.transform.position
    public float[] playerPosition = new float[3];
    public string sceneName = "";
    public SaveData()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (!player)
        {
            Debug.LogError("(SaveData) Player タグの付いた GameObject が見つかりませんでした");
        }
        else
        {
            playerPosition[0] = player.transform.position.x;
            playerPosition[1] = player.transform.position.y;
            // playerPosition[2] = player.transform.position.z;
            playerPosition[2] = 0;
            // Debug.Log($"(S) x: {player.transform.position.x}, y: {player.transform.position.y}");
        }
        sceneName = SceneManager.GetActiveScene().name;
    }
}
