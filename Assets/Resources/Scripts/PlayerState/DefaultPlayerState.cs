using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The Def State, contains the specific APlayerState implementations for being a Jock.
/// </summary>
public class DefaultPlayerState : APlayerState {
    public override APlayerBehaviour GetPlayerBehaviour()
    {
        throw new NotImplementedException();
    }

    public override APlayerBehaviour GetPlayerBehaviour()
    {
        throw new System.NotImplementedException();
    }

    /// <summary>
    /// The audio clip that is played when the player changes to this state.
    /// </summary>
    /// <returns></returns>
    public override AudioClip GetStateAudioClip()
    {
        AudioClip clip = new AudioClip();
        return clip;
    }
    /// <summary>
    /// The sprite the player takes on when changing to this state.
    /// </summary>
    /// <returns></returns>
    public override Sprite GetStateSprite()
    {
        return ResourceLoader.instance.jockSprite;
    }
}
