using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceLoader : Singleton<ResourceLoader>
{
    [HideInInspector]
    public Sprite skaterSprite;

    [HideInInspector]
    public Sprite jockSprite;

    protected override void Awake() {
        base.Awake();
        LoadResources();
    }

    private void LoadResources() {

        skaterSprite = Resources.Load<Sprite>("Textures/skaterBoy");
        jockSprite = Resources.Load<Sprite>("Textures/Ali");

    }

}
