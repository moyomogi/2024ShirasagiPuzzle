using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Elec : MonoBehaviour
{
    [SerializeField] Sprite imageOff;
    [SerializeField] Sprite imageOn;
    [SerializeField] BoxCollider boxCol;
    SpriteRenderer sr;
    string obj;
    bool touch;
    private void Start()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
        obj = this.gameObject.name.Substring(0, 4);
        if (obj == "Invi") boxCol.enabled = false;
        //Debug.Log(obj);
    }

    // private void Update()
    // {
    //     sr.sprite = imageOff;
    // }
    public void TurnOff()
    {
        if (sr != null) sr.sprite = imageOff;
        if (touch == true)
        {
            touch = false;
            Debug.Log(touch);
        }
        if (obj == "Invi") boxCol.enabled = false;
    }
    public void TurnOn()
    {
        if (sr != null) sr.sprite = imageOn;
        if (touch == false)
        {
            touch = true;
            Debug.Log(touch);
        }
        if (obj == "Invi") boxCol.enabled = true;
    }
    private void OnTriggerStay(Collider otherCollider)
    {
        var other = otherCollider.GetComponent<Collider>();

        //if (other.tag == "Elec")
        //{
        //    Debug.Log("Elec");
        //}
        switch (other.tag)
        {
            case "Elec":
                int thisId1 = this.gameObject.GetInstanceID();
                int otherId1 = other.gameObject.GetInstanceID();
                ElecManager.instance.UfElecUniteByIds(thisId1, otherId1);
                break;
            case "Player":
                //ここにboxのタグを追加するとboxでも電流を流せるようになります
                //boxにPlayerのタグをつけると何故か動かなかったです
                if (obj != "Invi" || touch == true)
                {
                    int thisId2 = this.gameObject.GetInstanceID();
                    int otherId2 = other.gameObject.GetInstanceID();
                    ElecManager.instance.UfElecUniteByIds(thisId2, otherId2);
                }
                break;
        }
    }
}
