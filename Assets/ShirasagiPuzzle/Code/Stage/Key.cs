using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    // private bool hasKey = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.instance.hasKey = true;
            GameManager.instance.PlaySound("key");
            Destroy(this.gameObject);
        }
    }
}
