using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RideButtonWall : MonoBehaviour
{
    [SerializeField] RideButton _RideButton;
    private float cnt = 240.0f, scale;

    void Start()
    {
        scale = transform.localScale.y;
        if (scale == 0) scale = 4.0f;
    }

    void Update()
    {
        transform.localScale = new Vector3(2, scale * cnt / 240.0f, 1);
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
