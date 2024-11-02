using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class ElecElevator : Elec
{
    private Renderer blockRenderer;
    private Color initialColor;
    //移動制限
    public float upperLimit = 5f; 
    public float lowerLimit = 0f;
    public float rightLimit = 5f;
    public float leftLimit = 0f;
    //移動速度
    public float speed = 2f;
    //移動中か判定
    private bool isMoving = false;
    private bool isMovingup = true;
    private bool isMovingright = true;
    private Vector3 initialPosition;
    
    private void Start()
    {
        blockRenderer = GetComponent<Renderer>();
        //initialColor = blockRenderer.material.color;
        //initialPosition = transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //blockRenderer.material.color = Color.yellow;//通電の仕方に悩んだので、一旦マテリアルの色を黄色にするだけにとどめました
            isMoving = true;
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //blockRenderer.material.color = initialColor;
            isMoving = false;
            collision.transform.SetParent(null);
        }
        
    }

    private void Update()
    {
       if(isMoving)
       {
            Moving();
            Moving2();
       }
    }

    //上下移動
    private void Moving()
    {
        float step = speed * Time.deltaTime; // 毎フレームの移動距離
        if (isMovingup)
        {
            // 上限まで移動
            transform.position += Vector3.up * step;
            if (transform.position.y >= initialPosition.y + upperLimit)
            {
                isMovingup = false; // 上限に達したら下へ移動するように切り替え
            }
        }
        else
        {
            // 下限まで移動
            transform.position -= Vector3.up * step;
            if (transform.position.y <= initialPosition.y + lowerLimit)
            {
                isMovingup = true; // 下限に達したら上へ移動するように切り替え
            }
        }
    }

    //左右移動
    private void Moving2()
    {
        float step = speed * Time.deltaTime;
        if(isMovingright)
        {
            transform.position += Vector3.right * step;
            if (transform.position.x >= initialPosition.x + rightLimit)
            {
                isMovingright = false;
            }
        }
        else
        {
            transform.position -= Vector3.right * step;
            if (transform.position.x <= initialPosition.x + leftLimit)
            {
                isMovingright = true;
            }
        }
    }
}