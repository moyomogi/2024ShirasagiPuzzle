using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RideButton : MonoBehaviour
{
    public bool ridden = false;
    private float scale;

    void Start() {
        scale = transform.localScale.x;
    }

    void OnTriggerStay(Collider other)
    {
        // if (other.gameObject.CompareTag("Player"))
        // {
        //     ridden = true;
        //     transform.localScale = new Vector3(2, 0.7f, 1);
        // }
        switch (other.tag)
        {
            case "Player" or "PushBlock" or "LightningBox":
                ridden = true;
                transform.localScale = new Vector3(scale, 0.35f * scale, 1);
                break;
        }
    }
    void OnTriggerExit(Collider other)
    {
        // if (other.gameObject.CompareTag("Player"))
        // {
        //     ridden = false;
        //     transform.localScale = Vector3.one * 2;
        // }
        switch (other.tag)
        {
            case "Player" or "PushBlock" or "LightningBox":
                ridden = false;
                transform.localScale = Vector3.one * scale;
                break;
        }
    }
}
