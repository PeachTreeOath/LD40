﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The Jock State, contains the specific APlayerState implementations for being a Jock.
/// </summary>
public class JockPlayerState : APlayerState {

    /// <summary>
    /// The audio clip that is played when the player changes to this state.
    /// </summary>
    /// <returns></returns>
    public override void PlayStateMusic()
    {
        AudioManager.instance.PlayMusicWithIntroResumingTime("Jock_intro", "Jock_loop");
    }
    /// <summary>
    /// The sprite the player takes on when changing to this state.
    /// </summary>
    /// <returns></returns>
    public override Sprite GetStateSprite()
    {
        return ResourceLoader.instance.jockSprite;
    }
    /// <summary>
    /// Change the speed of the player because you are now on a skateboard.
    /// </summary>
    /// <returns></returns>
    public override float GetStateSpeed()
    {
        //Change this to whatever it really should be 5 is a placeholder.
        return 7f;
    }

    /// <summary>
    /// Return the specfic implementation for this playerstate.
    /// </summary>
    /// <returns></returns>
    public override APlayerBehaviour GetPlayerBehaviour()
    {
        return new JockStateBehaviour();
    }
}
