using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// A base representation for what a Enemy state is.
/// All enemy states extend this class.
/// Every implementer of this class needs to fill out all fields related to this.
/// </summary>
public abstract class AEnemyState {
    
    /// <summary>
    /// The sprite the player takes on when changing to this state.
    /// </summary>
    /// <returns></returns>
    public abstract Sprite GetStateSprite();

    /// <summary>
    /// Updates against the enemy's transform
    /// </summary>
    public abstract void DoUpdate(EnemyController enemy);

    /// <summary>
    /// Get the speed for the enemy state.
    /// </summary>
    public virtual float GetStateSpeed()
    {
        return .1f;
    }

    /// <summary>
    /// Get the speed for the enemy affiliation.
    /// </summary>
    public virtual float GetIncrementAffiliationSpeed()
    {
        return .01f;
    }

    /// <summary>
    /// Get the speed for the enemy deffiliation.
    /// </summary>
    public virtual float GetDecrementAffiliationSpeed()
    {
        return .02f;
    }
}
