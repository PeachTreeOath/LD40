using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalPersistentStats : Singleton<GlobalPersistentStats> {

    public int level = 1;

    protected override void Awake()
    {
        base.Awake();

        SetDontDestroy();
    }
}
