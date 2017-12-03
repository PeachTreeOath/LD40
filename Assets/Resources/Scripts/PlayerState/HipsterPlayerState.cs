using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The Hipster State, contains the specific APlayerState implementations for being a Hipster
/// </summary>
public class HipsterPlayerState : APlayerState {


    /// <summary>
    /// The audio clip that is played when the player changes to this state.
    /// </summary>
    /// <returns></returns>
    public override void PlayStateMusic()
    {
        AudioManager.instance.PlayMusicWithIntro("hipster_intro", "hipster_loop", .3f);
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

    public override APlayerBehaviour GetPlayerBehaviour()
    {
        return new HipsterStateBehaviour();
    }
}
