﻿using UnityEngine;
using System.Collections;

public class NobodyController : WandererEnemyController {

    public override float GetStateSpeed() {
        return 0.2f;
    }
}
