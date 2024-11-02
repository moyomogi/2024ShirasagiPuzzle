using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Void : MonoBehaviour
{
    public Vector2 warpPoint;
    private Vector3 target;

    // Start is called before the first frame update
    void Start()
    {
        RespawnManager respawnManager;
        GameObject obj = GameObject.FindGameObjectWithTag("RespawnManager");
        respawnManager = obj.GetComponent<RespawnManager>();
        warpPoint.x = respawnManager.respos.x;
        warpPoint.y = respawnManager.respos.y;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player"|| other.gameObject.tag == "Box")
        {
            target = other.gameObject.transform.position;
            target.x = warpPoint.x;
            target.y = warpPoint.y;
            other.gameObject.transform.position = target;
        }
    }
}