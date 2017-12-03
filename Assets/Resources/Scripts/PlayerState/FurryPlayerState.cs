using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The Furry State, contains the specific APlayerState implementations for being a Furry.
/// </summary>
public class FurryPlayerState : APlayerState
{
    /// <summary>
    /// The audio clip that is played when the player changes to this state.
    /// </summary>
    /// <returns></returns>
    public override void PlayStateMusic()
    {
        AudioManager.instance.PlayMusicWithIntro("furry_intro", "furry_loop", .3f);
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
        return new FurryStateBehaviour();
    }
}
