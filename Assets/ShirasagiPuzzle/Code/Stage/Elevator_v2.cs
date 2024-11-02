using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator_v2 : MonoBehaviour
{
    private Rigidbody _rb;

    void Start()
    {
        _rb = this.transform.GetComponent<Rigidbody>();
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
        // }
        float subtract;
        switch (other.collider.tag)
        {
            case "Player":
                subtract = 0.5f + 1.1f * other.transform.localScale.y;
                other.transform.Translate(0, -subtract, 0);
                // transform.position = new Vector3(transform.position.x, other.transform.position.y + subtract, 0);
                break;
            case "PushBlock":
                subtract = 0.5f + 1.0f * other.transform.localScale.y;
                other.transform.Translate(0, -subtract, 0);
                // transform.position = new Vector3(transform.position.x, other.transform.position.y + subtract, 0);
                break;
            case "LightningBox":
                subtract = 0.5f + 0.5f * other.transform.localScale.y;
                other.transform.Translate(0, -subtract, 0);
                // transform.position = new Vector3(transform.position.x, other.transform.position.y + subtract, 0);
                break;
        }
    }
}
