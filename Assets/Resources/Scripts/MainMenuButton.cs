using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuButton : MonoBehaviour {

    void OnMouseDown()
    {
        Application.LoadLevel("Game");
    }
}
