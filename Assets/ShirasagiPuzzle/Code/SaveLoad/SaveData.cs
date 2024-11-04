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
    // - _player.transform.position
    public float[] playerPosition = new float[3];
    public string sceneName = "";
    public SaveData()
    {
        GameObject _player = GameObject.FindGameObjectWithTag("Player");
        if (!_player)
        {
            // Debug.LogError("(SaveData) Player タグの付いた GameObject が見つかりませんでした");
        }
        else
        {
            playerPosition[0] = _player.transform.position.x;
            playerPosition[1] = _player.transform.position.y;
            // playerPosition[2] = _player.transform.position.z;
            playerPosition[2] = 0;
            // Debug.Log($"(S) x: {_player.transform.position.x}, y: {_player.transform.position.y}");
        }
        sceneName = SceneManager.GetActiveScene().name;
    }
}
