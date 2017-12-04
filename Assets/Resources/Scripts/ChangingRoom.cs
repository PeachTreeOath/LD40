using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangingRoom : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            PlayerController.instance.inChangingRoom = true;
            OutfitManager.instance.Enable();
            if (Debug.isDebugBuild)
            {
                Debug.Log("Entering the changing room");
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            PlayerController.instance.inChangingRoom = false;
            OutfitManager.instance.Disable();
            if (Debug.isDebugBuild)
            {
                Debug.Log("Exiting the changing room");
            }
        }
    }
}
