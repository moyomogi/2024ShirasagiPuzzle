using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElecShock : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Sprite previousSprite;  // 前のスプライトを保持
    private GameObject spawnedObject;  // 生成したオブジェクトの参照
    private bool isSpawned = false;
    //public Vector3 newPosition;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        previousSprite = spriteRenderer?.sprite;  // 最初のスプライトを記憶
    }

    void Update()
    {
        // スプライトが変わった場合だけ処理する
        if (spriteRenderer.sprite != previousSprite)
        {
            Flowing();  // 新しいオブジェクトを生成
        }
        Hakai();
    }

    public void Flowing()
    {
        // ElecCurrentプレハブを生成して参照を保存
        GameObject obj = (GameObject)Resources.Load("ElecCurrent");
        if (obj != null)
        {
            if(!isSpawned)
            {
                // 親オブジェクトの真上に生成（Y座標を上げる）
                Vector3 spawnPosition = transform.position + new Vector3(0.0f, 3.8f, 0.0f);
                spawnedObject = Instantiate(obj, spawnPosition, Quaternion.identity);
                isSpawned = true;
            }
        }
        else
        {
            Debug.LogError("ElecCurrent プレハブが見つかりません！");
        }
    }

    public void Hakai()
    {
        if (spriteRenderer.sprite == previousSprite)
        {
            if(isSpawned)
            {
                //Debug.Log("破壊した");
                Destroy(spawnedObject);
                isSpawned = false;
            }
        }
    }
}