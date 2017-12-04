﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceLoader : Singleton<ResourceLoader>
{
    [HideInInspector]
    public Sprite muteSprite;

    [HideInInspector]
    public Sprite unmuteSprite;

    [HideInInspector]
    public Sprite skaterSprite;

    [HideInInspector]
    public Sprite jockSprite;

    [HideInInspector]
    public Sprite furrySprite;

    [HideInInspector]
    public GameObject skaterPrefab;

    [HideInInspector]
    public GameObject jockPrefab;

    [HideInInspector]
    public GameObject furryPrefab;

    [HideInInspector]
    public GameObject nobodyPrefab;

    public RuntimeAnimatorController furAnim;
    public RuntimeAnimatorController jockAnim;

    public Color darkReputationColor;
    public Color lightReputationColor;

    protected override void Awake() {
        base.Awake();
        LoadResources();
    }

    private void LoadResources() {

        muteSprite = Resources.Load<Sprite>("Textures/UI/mute");
        unmuteSprite = Resources.Load<Sprite>("Textures/UI/unmute");

        skaterSprite = Resources.Load<Sprite>("Textures/mainGuySkater");
        jockSprite = Resources.Load<Sprite>("Textures/mainGuyJock");
        furrySprite = Resources.Load<Sprite>("Textures/mainGuyFurry");

        skaterPrefab = Resources.Load<GameObject>("Prefabs/SkaterEnemy");
        jockPrefab = Resources.Load<GameObject>("Prefabs/JockEnemy");
        furryPrefab = Resources.Load<GameObject>("Prefabs/FurryEnemy");
        nobodyPrefab = Resources.Load<GameObject>("Prefabs/Nobody");

        //Skater has no anim
        jockAnim = Resources.Load<RuntimeAnimatorController>("Textures/mainGuyJock_0 (1)");
        furAnim = Resources.Load<RuntimeAnimatorController>("Textures/mainGuyFurry_0");

        ColorUtility.TryParseHtmlString("25255CFF", out darkReputationColor);
        ColorUtility.TryParseHtmlString("A73A96FF", out lightReputationColor);
    }

}

