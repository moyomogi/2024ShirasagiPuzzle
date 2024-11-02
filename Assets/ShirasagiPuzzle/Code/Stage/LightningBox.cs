using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningBox : Elec
{
    private Transform player;
    private bool isHeld = false;
    private Collider boxCollider;

    public float pickUpDistance = 1.5f; // 変更可能な持ち上げ距離

    private float fixedZPosition;

    // プレイヤーが現在持っているBoxの参照を保持する静的変数
    private static LightningBox heldBox;
    private Renderer _renderer;

    public override void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        fixedZPosition = transform.position.z;



        // Boxのコライダーを取得
        boxCollider = transform.Find("Wall").gameObject.GetComponent<Collider>();
        if (boxCollider == null)
        {
            Debug.LogWarning("Collider not found on Box object!");
        }
        _renderer = GetComponent<Renderer>();

        if (_renderer != null)
        {
            _renderer.material.color = Color.cyan;
            // _renderer.material.color = Color.yellow;
        }
        else
        {
            Debug.LogWarning("Renderer not found!");
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
            transform.position = new Vector3(player.position.x, player.position.y + 2.0f, fixedZPosition);
        }
        else
        {
            // Z軸を固定したまま、現在のX, Y座標を維持する
            transform.position = new Vector3(transform.position.x, transform.position.y, fixedZPosition);
        }

    }

    public override void TurnOff()
    {
        if (_renderer != null) _renderer.material.color = Color.cyan;
    }
    public override void TurnOn()
    {
        if (_renderer != null) _renderer.material.color = Color.yellow;
    }

}
