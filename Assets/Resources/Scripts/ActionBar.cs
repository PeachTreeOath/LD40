using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionBar : Singleton<ActionBar> {

    public Text txt;

    public Image img;
	public void Activate()
    {
        img.enabled = true;
        txt.enabled = true;
    }

    public void Deactivate()
    {
        img.enabled = false;
        txt.enabled = false;
    }

    public void ChangeText(string text)
    {
        txt.text = text;
    }
}
