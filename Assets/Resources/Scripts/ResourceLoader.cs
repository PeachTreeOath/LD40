using System.Collections;
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

    public GameObject level1Prefab;
    public GameObject level2Prefab;
    public GameObject level3Prefab;
    public GameObject level4Prefab;
    public GameObject level5Prefab;
    public GameObject level6Prefab;

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

        level1Prefab = Resources.Load<GameObject>("Prefabs/Levels/Level1");
        level2Prefab = Resources.Load<GameObject>("Prefabs/Levels/Level2");
        level3Prefab = Resources.Load<GameObject>("Prefabs/Levels/Level3");
        level4Prefab = Resources.Load<GameObject>("Prefabs/Levels/Level4");
        level5Prefab = Resources.Load<GameObject>("Prefabs/Levels/Level5");
        level6Prefab = Resources.Load<GameObject>("Prefabs/Levels/Level6");
        Instantiate(ResourceLoader.instance.level1Prefab);
    }

    public Level GetLevel(int level)
    {
        switch(level)
        {
            case 1:
                return Instantiate(ResourceLoader.instance.level1Prefab).GetComponent<Level>();
            case 2:
                return Instantiate(ResourceLoader.instance.level2Prefab).GetComponent<Level>();
            case 3:
                return Instantiate(ResourceLoader.instance.level3Prefab).GetComponent<Level>();
            case 4:
                return Instantiate(ResourceLoader.instance.level4Prefab).GetComponent<Level>();
            case 5:
                return Instantiate(ResourceLoader.instance.level5Prefab).GetComponent<Level>();
            case 6:
                return Instantiate(ResourceLoader.instance.level6Prefab).GetComponent<Level>();
        }
        return null;
    }
}

