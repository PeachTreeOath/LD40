using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The Furry State, contains the specific APlayerState implementations for being a Furry.
/// </summary>
public class FurryState : APlayerState
{
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
        Sprite stateSprite = new Sprite();
        return stateSprite;
    }
}
