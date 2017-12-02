﻿using System.Collections;
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
    /// The audio clip that is played when the player changes to this state.
    /// </summary>
    /// <returns></returns>
    public abstract AudioClip GetStateAudioClip();
    

}
