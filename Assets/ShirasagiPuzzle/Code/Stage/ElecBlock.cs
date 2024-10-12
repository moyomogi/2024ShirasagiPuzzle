using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElecBlock : Elec
{
    // [SerializeField] Sprite imageOff;
    // [SerializeField] Sprite imageOn;
    // SpriteRenderer sr;
    // // bool collidesWithPlayer = false;

    // // GameObject[] objs = GameObject.FindGameObjectsWithTag("Elec");
    // // foreach (GameObject obj in objs)
    // // {
    // //     Debug.Log(obj.name);
    // // }

    // private void Start()
    // {
    //     sr = gameObject.GetComponent<SpriteRenderer>();
    // }

    // private void Update()
    // {
    //     sr.sprite = imageOff;
    // }
    // public void TurnOn()
    // {
    //     sr.sprite = imageOn;
    // }
    // private void OnTriggerStay(Collider otherCollider)
    // {
    //     var other = otherCollider.GetComponent<Collider>();
    //     switch (other.tag)
    //     {
    //         case "Elec":
    //         case "Player":
    //             // collidesWithPlayer = true;
    //             int thisId = this.GetInstanceID();
    //             int otherId = other.GetInstanceID();
    //             GameManager.instance.UfElecUniteByIds(thisId, otherId);
    //             sr.sprite = imageOn;
    //             break;
    //     }
    //     // if (other.GetComponent<Collider>().tag == "Player")
    //     // {
    //     // }
    // }
}
