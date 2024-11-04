using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvisBlock : Elec
{
    // https://shibuya24.info/entry/unity-transform-find
    [SerializeField] Transform _Wall;

    // https://yuumekou.net/csharp-guide-5-4/
    // virtual-override により親クラスの関数上書き
    public override void TurnOff()
    {
        base.SetImageOff();
        if (_Wall != null) _Wall.gameObject.SetActive(false);
    }
    public override void TurnOn()
    {
        base.SetImageOn();
        if (_Wall != null) _Wall.gameObject.SetActive(true);
    }
    // PushBlock が貫通して下に落ちないように、押し上げるべき
    // private void OnTriggerStay(Collider other)
    // {
    //     if (other.tag == "PushBlock")
    //     {
    //         float yDis = other.transform.position.y - transform.position.y;
    //         if (yDis > 0.5f * transform.localScale.y + 0.5f * other.transform.localScale.y - 1)
    //         {
    //             other.transform.position = new Vector3(transform.position.x, 2.0f * yDis - 1.0f * transform.localScale.y, 0);
    //             Rigidbody _rb = other.GetComponent<Rigidbody>();
    //             _rb.velocity = Vector3.zero;
    //         }
    //     }
    // }
}
