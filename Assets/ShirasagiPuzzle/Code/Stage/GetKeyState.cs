using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetKeyState : MonoBehaviour
{
    private bool isGetKey = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision other)
    {
        //鍵を取得状態にした後、ステージ上の鍵を破壊する
        if(other.gameObject.CompareTag("Key"))
        {
            isGetKey = true;
            Destroy(other.gameObject);
        }
        //鍵を持った状態でドアに触れるとドアを破壊する
        if(other.gameObject.CompareTag("Door")&&isGetKey)
        {
            Destroy(other.gameObject);
        }
    }
}
