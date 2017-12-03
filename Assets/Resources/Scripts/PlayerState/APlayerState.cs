using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// A base representation for what a Player state is.
/// All player states extend this class.
/// Every implementer of this class needs to fill out all fields related to this.
/// </summary>
public abstract class APlayerState {

    /// <summary>
    /// The sprite the player takes on when changing to this state.
    /// </summary>
    /// <returns></returns>
    public abstract Sprite GetStateSprite();


    /// <summary>
    /// Play a particular music when the player changes to this state.
    /// </summary>
    /// <returns></returns>
    public abstract void PlayStateMusic();

    /// <summary>
    /// Get the new speed for the player state. Not all states 
    /// change the speed thus this is a virutal instead of an abstract method.
    /// </summary>
    public virtual float GetStateSpeed()
    {
        return .1f;
    }

    /// <summary>
    /// Get the Player behaviour for this player state.
    /// </summary>
    /// <returns></returns>
    public abstract APlayerBehaviour GetPlayerBehaviour();
    

}
