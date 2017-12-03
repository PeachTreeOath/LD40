using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JockEnemyState : WandererEnemyState {
    
    /// <summary>
    /// The sprite the player takes on when changing to this state.
    /// </summary>
    /// <returns></returns>
    public override Sprite GetStateSprite()
    {
        return ResourceLoader.instance.jockSprite;
    }

    public override float GetStateSpeed() {
        return 3f;
    }
}
