using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Elec : MonoBehaviour
{
    // https://allmoreidea.hatenablog.com/entry/2018/06/30/112231
    // ShockBlock などの Elec の子クラスからアクセスできるように public
    [SerializeField] Sprite imageOff;
    [SerializeField] Sprite imageOn;
    public SpriteRenderer sr;

    private void Start()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
        TurnOff();
    }

    // private void Update()
    // {
    //     sr.sprite = imageOff;
    // }
    // https://yuumekou.net/csharp-guide-5-4/
    // virtual-override により親クラスの関数上書き
    public virtual void TurnOff()
    {
        SetImageOff();
        // if (sr != null) sr.sprite = imageOff;
    }
    public void SetImageOff()
    {
        if (sr != null) sr.sprite = imageOff;
    }
    public virtual void TurnOn()
    {
        SetImageOn();
        // if (sr != null) sr.sprite = imageOn;
    }
    public void SetImageOn()
    {
        if (sr != null) sr.sprite = imageOn;
    }

    public void OnTriggerStay(Collider otherCollider)
    {
        var other = otherCollider.GetComponent<Collider>();

        // 衝突しているか
        bool collides = false;

        if (this.tag == "ElecBackground")
        {
            switch (other.tag)
            {
                case "Elec" or "ElecBackground":
                    collides = true;
                    break;
            }
        }
        else
        {
            switch (other.tag)
            {
                case "Elec" or "ElecBackground" or "Player":
                    collides = true;
                    break;
            }
        }
        if (collides)
        {
            int thisId = this.gameObject.GetInstanceID();
            int otherId = other.gameObject.GetInstanceID();
            ElecManager.instance.UfElecUniteByIds(thisId, otherId);
        }
    }
}
