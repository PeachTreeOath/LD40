using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OutfitManager : Singleton<OutfitManager> {

    public Text txt;

    public void Enable()
    {
        txt.enabled = true;
    }

    public void Disable()
    {
        txt.enabled = false;
    }
}
