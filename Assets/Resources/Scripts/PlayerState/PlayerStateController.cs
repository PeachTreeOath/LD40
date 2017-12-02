using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateController : MonoBehaviour {

    public static PlayerStateController instance = null;

    public CliqueEnum CurrentState;

    void Awake()
    {
        if (instance == null)
            instance = this;
        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// Change the players state to the given state.
    /// </summary>
    /// <param name="state"></param>
    public void ChangePlayerState(CliqueEnum state)
    {
        CurrentState = state;
        switch (state)
        {
            case CliqueEnum.BEARDSTER:
                //Call the singleton for the Player controller here and then go
                //PlayerController.Sprite = HipsterState.GetStateSprite();
                break;
            case CliqueEnum.FURBOI:
                break;
            case CliqueEnum.JOCK:
                break;
            case CliqueEnum.SK8R:
                break;
        }
    }

    /// <summary>
    /// Return the current state the player is.
    /// </summary>
    /// <returns></returns>
    public CliqueEnum GetPlayerState()
    {
        return CurrentState;
    }



    
}
