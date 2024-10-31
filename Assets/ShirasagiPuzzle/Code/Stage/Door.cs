using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public string SceneName;
    public bool isLockDoor;

    bool collides = false, enters = false;
    float timeCount = 0.0f;
    const float SPEED = 2.4f;
    // private int curLevel, maxLevel;

    void Start()
    {
        if (SceneName == "")
        {
            Debug.LogError($"Set \"SceneName\"");
        }
        // curLevel = SceneManager.GetActiveScene().buildIndex;
        // maxLevel = SceneManager.sceneCountInBuildSettings - 1;
    }

    void Update()
    {
        transform.rotation = Quaternion.Euler(0, -Mathf.Min(1.0f, timeCount * SPEED) * 90, 0);
        if (enters)
        {
            timeCount += Time.deltaTime;
            if (timeCount * SPEED > 0.9f) SceneManager.LoadScene(SceneName);
        }
        else if (collides && (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)))
        {
            if (isLockDoor)
            {
                if (!GameManager.instance.hasKey)
                {
                    GameManager.instance.PlaySound("locked");
                    return;
                }
                GameManager.instance.PlaySound("unlock");
            }
            else
            {
                GameManager.instance.PlaySound("door");
            }
            enters = true;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            collides = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            collides = false;
        }
    }
}
