using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    private Transform player;
    private bool isHeld = false;
    public float pickUpDistance = 1.5f; // 変更可能な持ち上げ距離

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
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