using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Elec : MonoBehaviour
{
    [SerializeField] Sprite imageOff;
    [SerializeField] Sprite imageOn;
    SpriteRenderer sr;

    private void Start()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
    }

    // private void Update()
    // {
    //     sr.sprite = imageOff;
    // }
    public void TurnOff()
    {
        if (sr != null) sr.sprite = imageOff;
    }
    public void TurnOn()
    {
        if (sr != null) sr.sprite = imageOn;
    }
    private void OnTriggerStay(Collider otherCollider)
    {
        var other = otherCollider.GetComponent<Collider>();
        // if (other.tag == "Elec")
        // {
        //     Debug.Log("Elec");
        // }
        switch (other.tag)
        {
            case "Elec" or "Player":
                // collidesWithPlayer = true;
                int thisId = this.gameObject.GetInstanceID();
                int otherId = other.gameObject.GetInstanceID();
                ElecManager.instance.UfElecUniteByIds(thisId, otherId);
                break;
        }
        // if (other.GetComponent<Collider>().tag == "Player")
        // {
        // }
    }
}
