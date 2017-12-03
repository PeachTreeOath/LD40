﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceLoader : Singleton<ResourceLoader>
{
    [HideInInspector]
    public Sprite skaterSprite;

    [HideInInspector]
    public Sprite jockSprite;

    [HideInInspector]
    public GameObject skaterPrefab;

    [HideInInspector]
    public GameObject jockPrefab;

    protected override void Awake() {
        base.Awake();
        LoadResources();
    }

    private void LoadResources() {

        skaterSprite = Resources.Load<Sprite>("Textures/skaterBoy");
        jockSprite = Resources.Load<Sprite>("Textures/Ali");

        skaterPrefab = Resources.Load<GameObject>("Prefabs/SkaterEnemy");
        jockPrefab = Resources.Load<GameObject>("Prefabs/JockEnemy");
    }

}
