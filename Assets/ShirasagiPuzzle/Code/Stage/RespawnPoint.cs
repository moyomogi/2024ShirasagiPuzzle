using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RespawnPoint : MonoBehaviour
{
    [SerializeField] Sprite imageOn;
    SpriteRenderer sr;
    private bool isTouched = false;
    public Vector3 pos;

    // Start is called before the first frame update
    void Start()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider otherCollider)
    {
        if(!isTouched)
        {
            if(otherCollider.gameObject.CompareTag("Player"))
            {
                if(sr != null)sr.sprite = imageOn;
                isTouched = true;
                Transform myTransform = this.transform;
                Vector3 pos = myTransform.position;
                RespawnManager respawnManager;
                GameObject obj = GameObject.FindGameObjectWithTag("RespawnManager");
                respawnManager = obj.GetComponent<RespawnManager>();
                respawnManager.respos.x = pos.x;
                respawnManager.respos.y = pos.y;
            }
        }    
    }
}
