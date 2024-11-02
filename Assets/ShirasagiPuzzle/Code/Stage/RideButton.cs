using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RideButton : MonoBehaviour
{
    public bool ridden = false;

    // void FixedUpdate()
    // {
    //     // if (ridden) transform.localScale = new Vector3(2, 0.7f, 1);
    //     // else transform.localScale = Vector3.one * 2;

    // }
    void OnTriggerEnter(Collider other)
    {
        // if (other.gameObject.CompareTag("Player"))
        // {
        //     ridden = true;
        //     transform.localScale = new Vector3(2, 0.7f, 1);
        // }
        switch (other.tag)
        {
            case "Player" or "PushBlock" or "LightningBlock":
                ridden = true;
                transform.localScale = new Vector3(2, 0.7f, 1);
                break;
        }
    }
    void OnTriggerExit(Collider other) {
        // if (other.gameObject.CompareTag("Player"))
        // {
        //     ridden = false;
        //     transform.localScale = Vector3.one * 2;
        // }
        switch (other.tag)
        {
            case "Player" or "PushBlock":
                ridden = false;
                transform.localScale = Vector3.one * 2;
                break;
        }
    }
}
