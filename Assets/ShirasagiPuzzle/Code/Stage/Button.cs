using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("発動！！");
        Box box = other.GetComponent<Box>();

        if (box != null)
        {
            Debug.Log("Box entered trigger: " + box.gameObject.name + " with color: " + box.boxColor);
            DestroyAllBoxesOfColor(box.boxColor);
        }
        else
        {
            Debug.Log("Object entered trigger, but it's not a Box.");
        }
    }

    private void DestroyAllBoxesOfColor(Color color)
    {
        Box[] boxes = FindObjectsOfType<Box>();
        foreach (Box box in boxes)
        {
            if (box.boxColor == color)
            {
                Destroy(box.gameObject);
            }
        }
    }
}
