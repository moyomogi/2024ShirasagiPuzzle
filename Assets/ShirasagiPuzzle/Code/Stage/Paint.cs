using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paint : MonoBehaviour
{
    public Color PaintColor; // ペンキの色


    void Start()
    {
        // レンダラーから色を初期化
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            PaintColor = renderer.material.color;
            Debug.Log("PaintColor color initialized: " + PaintColor);
        }
        else
        {
            Debug.Log("Renderer not found on Paint object!");
        }
    }


    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger entered by: " + other.gameObject.name);

        // 当たったオブジェクトがBoxかどうかをチェック
        Box box = other.GetComponent<Box>();
        if (box != null)
        {
            // Boxの色を変えるメソッドを呼ぶ（ここで色を指定します）
            box.ApplyPaint(PaintColor);

        }
        else
        {
            Debug.Log("当たったオブジェクトはBoxではありません。");
        }
    }
}