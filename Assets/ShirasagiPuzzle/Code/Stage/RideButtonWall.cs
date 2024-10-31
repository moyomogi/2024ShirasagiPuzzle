using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RideButtonWall : MonoBehaviour
{
    [SerializeField] RideButton _RideButton;
    float cnt = 240.0f;

    void Update()
    {
        transform.localScale = new Vector3(2, 4.0f * cnt / 240.0f, 1);
        if (_RideButton.ridden)
        {
            cnt = Mathf.Max(cnt - 1, 0.0f);
        }
        else
        {
            cnt = Mathf.Min(cnt + 1.5f, 240.0f);
        }
    }
}
