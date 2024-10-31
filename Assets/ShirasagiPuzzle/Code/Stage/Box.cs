using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    private Transform player;
    private bool isHeld = false;
    private Collider boxCollider;

    public float pickUpDistance = 1.5f; // 変更可能な持ち上げ距離
    public Color boxColor;
    private float fixedZPosition;

    // プレイヤーが現在持っているBoxの参照を保持する静的変数
    private static Box heldBox;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        fixedZPosition = transform.position.z;

        // レンダラーから色を初期化
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            boxColor = renderer.material.color;
            Debug.Log("Box color initialized: " + boxColor);
        }
        else
        {
            Debug.Log("Renderer not found on Box object!");
        }
        // Boxのコライダーを取得
        boxCollider = GetComponent<Collider>();
        if (boxCollider == null)
        {
            Debug.LogWarning("Collider not found on Box object!");
        }
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isHeld)
            {
                Debug.Log("捨てる");
                isHeld = false;
                heldBox = null; // 持っているBoxを空にする
                // 捨てたときにコライダーを有効にする
                if (boxCollider != null)
                {
                    boxCollider.enabled = true;
                }
            }
            else if (heldBox == null && distanceToPlayer <= pickUpDistance) // 他に持っていない場合のみ持ち上げられる
            {
                Debug.Log("持ち上げる");
                isHeld = true;
                heldBox = this; // このBoxを持っているBoxとして設定

                // 持っている間はコライダーを無効にする
                if (boxCollider != null)
                {
                    boxCollider.enabled = false;
                }
            }
        }

        // Boxの位置を更新（Z軸を固定）
        if (isHeld)
        {
            // プレイヤーの上に配置する
            transform.position = new Vector3(player.position.x, player.position.y + 2f, fixedZPosition);
        }
        else
        {
            // Z軸を固定したまま、現在のX, Y座標を維持する
            transform.position = new Vector3(transform.position.x, transform.position.y, fixedZPosition);
        }
        
    }

    public void ApplyPaint(Color newColor)
    {
        // Boxの色を変更する
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.color = newColor;
            boxColor = newColor;
            Debug.Log($"Box color changed to: {newColor}");
        }
    }
}
