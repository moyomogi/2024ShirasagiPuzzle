using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    private Transform player;
    private bool isHeld = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (isHeld)
        {
            transform.position = player.position + new Vector3(0, 1f, 0); // プレイヤーの上に配置
        }

        if (Input.GetKeyDown(KeyCode.E)) // Eキーで持ち上げ/下ろす
        {
            isHeld = !isHeld;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isHeld = true; // まず持ち上げ状態にする
        }
    }
}
