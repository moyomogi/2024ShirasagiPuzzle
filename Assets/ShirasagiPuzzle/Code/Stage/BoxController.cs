using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    private Transform player;
    private bool isHeld = false;
    public float pickUpDistance = 1.5f; // 変更可能な持ち上げ距離
    public Color boxColor;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
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

    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isHeld)
            {
                // 現在持っている場合は下ろす
                isHeld = false;
            }
            else if (distanceToPlayer <= pickUpDistance)
            {
                // 距離が十分近い場合のみ持ち上げる
                isHeld = true;
            }
        }

        if (isHeld)
        {
            transform.position = player.position + new Vector3(0, 1f, 0); // プレイヤーの上に配置
        }
    }
}