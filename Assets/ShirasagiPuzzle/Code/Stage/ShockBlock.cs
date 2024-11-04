using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShockBlock : Elec
{
    // https://shibuya24.info/entry/unity-transform-find
    [SerializeField] Transform _Wall;
    [SerializeField] Transform _Killer;
    private List<LightningBox> lightningBoxs = new List<LightningBox>();

    void Awake()
    {
        GameObject[] elecGameObjs = GameObject.FindGameObjectsWithTag("LightningBox");
        foreach (GameObject elecGameObj in elecGameObjs)
        {
            LightningBox lightningBox = elecGameObj.GetComponent<LightningBox>();
            if (lightningBox != null)
            {
                lightningBoxs.Add(lightningBox);
            }
        }
    }

    // https://yuumekou.net/csharp-guide-5-4/
    // virtual-override により親クラスの関数上書き
    public override void TurnOff()
    {
        // foreach (LightningBox lightningBox in lightningBoxs)
        // {
        //     if (lightningBox.gameObject.activeSelf == true) lightningBox.NotRed();
        // }
        base.SetImageOff();
        if (_Wall != null) _Wall.gameObject.SetActive(true);
        if (_Killer != null) _Killer.gameObject.SetActive(false);
    }
    public override void TurnOn()
    {
        // foreach (LightningBox lightningBox in lightningBoxs)
        // {
        //     if (lightningBox.gameObject.activeSelf == true) lightningBox.Red();
        // }
        base.SetImageOn();
        if (_Wall != null) _Wall.gameObject.SetActive(false);
        if (_Killer != null) _Killer.gameObject.SetActive(true);
    }
}
