﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JockEnemyState : AEnemyState {
    
    /// <summary>
    /// The sprite the player takes on when changing to this state.
    /// </summary>
    /// <returns></returns>
    public override Sprite GetStateSprite()
    {
        return ResourceLoader.instance.jockSprite;
    }

    /// <summary>
    /// Updates against the enemy's transform
    /// </summary>
    /// <param name="transform"></param>
    public override void DoUpdate(EnemyController enemy)
    {
        float step = GetStateSpeed() * Time.deltaTime;
        enemy.rbody.MovePosition(Vector2.MoveTowards(enemy.transform.position, PlayerController.instance.transform.position, step));
    }
}
