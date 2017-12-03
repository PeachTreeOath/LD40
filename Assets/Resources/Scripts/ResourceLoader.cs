using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceLoader : Singleton<ResourceLoader>
{
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
    public GameObject hipsterPrefab;

    protected override void Awake() {
        base.Awake();
        LoadResources();
    }

    private void LoadResources() {

        skaterSprite = Resources.Load<Sprite>("Textures/mainGuySkater");
        jockSprite = Resources.Load<Sprite>("Textures/mainGuyJock");
        furrySprite = Resources.Load<Sprite>("Textures/mainGuyFurry");

        skaterPrefab = Resources.Load<GameObject>("Prefabs/SkaterEnemy");
        jockPrefab = Resources.Load<GameObject>("Prefabs/JockEnemy");
        furryPrefab = Resources.Load<GameObject>("Prefabs/FurryEnemy");
        //hipsterPrefab = Resources.Load<GameObject>("Prefabs/HipsterEnemy");
    }

}
