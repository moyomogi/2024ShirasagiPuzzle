using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Elec : MonoBehaviour
{
    // https://allmoreidea.hatenablog.com/entry/2018/06/30/112231
    [SerializeField] Sprite imageOff;
    [SerializeField] Sprite imageOn;
    // [SerializeField] BoxCollider boxCol;
    SpriteRenderer sr;

    public virtual void Start()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
        TurnOff();
        // obj = this.gameObject.name.Substring(0, 3);
        // if (obj == "Inv") boxCol.enabled = false;
    }

    // virtual-override https://yuumekou.net/csharp-guide-5-4/
    public virtual void TurnOff()
    {
        SetImageOff();
    }
    public void SetImageOff()
    {
        if (sr != null) sr.sprite = imageOff;
    }
    public virtual void TurnOn()
    {
        SetImageOn();
    }
    public void SetImageOn()
    {
        if (sr != null) sr.sprite = imageOn;
    }
    private void OnTriggerStay(Collider other)
    {
        // var other = otherCollider.GetComponent<Collider>();
        bool collides = false;

        // 能動的発電を行う LightningBox はグループ化処理から除外
        if (this.tag != "LightningBox")
        {
            if (this.tag != "ElecBackground")
            {
                // 基本は全部グループ化
                switch (other.tag)
                {
                    case "Elec" or "ElecBackground" or "Player" or "LightningBox":
                        collides = true;
                        break;
                }
            }
            else
            {
                // InvisBlock, Chain は、発電物体 (Player, LightningBox) とは衝突せず、Elec, ElecBackground とは衝突する
                switch (other.tag)
                {
                    case "Elec" or "ElecBackground":
                        collides = true;
                        break;
                    case "Player" or "LightningBox":
                        if (sr.sprite == imageOn) collides = true;
                        break;
                }
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
