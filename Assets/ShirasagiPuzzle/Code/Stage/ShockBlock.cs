using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShockBlock : Elec
{
    // https://shibuya24.info/entry/unity-transform-find
    [SerializeField] Transform _Wall;
    [SerializeField] Transform _Killer;

    // https://yuumekou.net/csharp-guide-5-4/
    // virtual-override により親クラスの関数上書き
    public override void TurnOff()
    {
        base.SetImageOff();
        if (_Wall != null) _Wall.gameObject.SetActive(true);
        if (_Killer != null) _Killer.gameObject.SetActive(false);
    }
    public override void TurnOn()
    {
        base.SetImageOn();
        if (_Wall != null) _Wall.gameObject.SetActive(false);
        if (_Killer != null) _Killer.gameObject.SetActive(true);
    }
}