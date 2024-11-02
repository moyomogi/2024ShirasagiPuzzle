using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator_v2 : MonoBehaviour
{
    // 1 s あたりの移動距離
    [SerializeField] float speed;
    // 行き先リスト
    [SerializeField] List<Vector3> dests;
    private int curDestIdx = 0;
    private float elapsedTime = 0.0f;
    private Vector3 defaultPos, displacement;

    private Rigidbody _rb;
    // private GameObject _player;
    private Rigidbody _player_rb;

    void Start()
    {
        defaultPos = transform.position;

        _rb = transform.GetComponent<Rigidbody>();
        GameObject _player = StageController.instance._player;
        _player_rb = _player.transform.GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        int nxtDestIdx = (curDestIdx + 1) % dests.Count;
        Vector3 curDest = dests[curDestIdx], nxtDest = dests[nxtDestIdx];

        // elapsedTime += Time.deltaTime;
        elapsedTime += 1.0f / 60;
        float r = speed * elapsedTime / (curDest - nxtDest).magnitude;
        if (r > 1.0f) r = 1.0f;
        displacement = curDest * (1 - r) + nxtDest * r; // 変位
        // _rb.Move(defaultPos + displacement, Quaternion.identity);
        transform.position = defaultPos + displacement;
        if (r == 1.0f)
        {
            r = 0.0f;
            curDestIdx = nxtDestIdx;
            elapsedTime = 0;
        }
    }

    private void OnCollisionStay(Collision other)
    {
        // https://docs.unity3d.com/jp/2018.4/ScriptReference/Collision.html
        // other.transform
        // other.gameObject
        // other.rigidbody
        // other.collider.tag
        // if (other.collider.tag == "Player")
        // {
        float add;
        switch (other.collider.tag)
        {
            case "PushBlock":
                add = 0.5f * transform.localScale.y + 1.0f * other.transform.localScale.y;
                other.transform.Translate(0, add, 0);
                break;
            case "LightningBox":
                add = 0.5f * transform.localScale.y + 0.5f * other.transform.localScale.y;
                other.transform.Translate(0, add, 0);
                break;
        }
    }
    private void OnCollisionExit(Collision other)
    {
        switch (other.collider.tag)
        {
            case "PushBlock":
                other.rigidbody.velocity = new Vector3(0, -4.0f * speed, 0);
                break;
            case "LightningBox":
                other.rigidbody.velocity = new Vector3(0, -4.0f * speed, 0);
                break;
        }
    }
}
