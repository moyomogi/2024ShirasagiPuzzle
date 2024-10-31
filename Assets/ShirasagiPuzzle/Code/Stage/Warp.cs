using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warp : MonoBehaviour
{
	public Vector2 warpPoint;
	private Vector3 target;
	
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        RespawnManager respawnManager;
        GameObject obj = GameObject.FindGameObjectWithTag("RespawnManager");
        respawnManager = obj.GetComponent<RespawnManager>();
        warpPoint.x = respawnManager.respos.x;
        warpPoint.y = respawnManager.respos.y;
    }
    //ワープポイントに設定した座標に移動
    void OnCollisionEnter(Collision other)
     {
        if(other.gameObject.tag==("Player"))
        {
            target = other.gameObject.transform.position;
    	    target.x = warpPoint.x;
    	    target.y = warpPoint.y;
    	    other.gameObject.transform.position = target;
        }
    }
}