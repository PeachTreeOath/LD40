using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MuteButton : MonoBehaviour {

    private Image image;
    private bool mute = false;

    public void Start() {
        image = GetComponent<Image>(); 
    }

    public void ToggleMute() {
        mute = !mute;

        AudioManager.instance.ToggleMute(mute);
        if(mute)
            image.sprite = ResourceLoader.instance.unmuteSprite;
        else
            image.sprite = ResourceLoader.instance.muteSprite;
    }
}
