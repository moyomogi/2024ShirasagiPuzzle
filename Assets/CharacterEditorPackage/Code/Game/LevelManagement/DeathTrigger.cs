﻿using UnityEngine;
using System.Collections;
//--------------------------------------------------------------------
//When the player enters, respawn them
//--------------------------------------------------------------------
public class DeathTrigger : MonoBehaviour {

    void OnTriggerEnter(Collider a_Collider)
    {
        ControlledCapsuleCollider controlledCapsuleCollider = a_Collider.GetComponent<ControlledCapsuleCollider>();
        if (controlledCapsuleCollider != null)
        {
            //Prevent death state to be used if the collider is no-clipping
            if (controlledCapsuleCollider.AreCollisionsActive())
            {
                Debug.Log("Death triggered by: " + transform.name);

                // moyomogi
                GameManager.instance.PlaySound("death");
                GameManager.instance.shouldLoad = true;

                // if (InSceneLevelSwitcher.Get())
                // {
                //     Debug.Log("InSceneLevelSwitcher.Get(): " + InSceneLevelSwitcher.Get());
                //     InSceneLevelSwitcher.Get().Respawn();
                // }
            }
        }
    }
}
