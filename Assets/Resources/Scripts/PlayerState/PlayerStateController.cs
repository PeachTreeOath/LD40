using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateController : Singleton<PlayerStateController> {

    /// <summary>
    /// The Current state for the player.
    /// </summary>
    public CliqueEnum CurrentState;

    private PlayerController player;
    private SpriteRenderer playerSprite;
    private Dictionary<CliqueEnum, APlayerState> stateMap = new Dictionary<CliqueEnum, APlayerState>();

    void Start()
    {
        player = GetComponent<PlayerController>();
        playerSprite = GetComponentInChildren<SpriteRenderer>();

        stateMap.Add(CliqueEnum.BEARDSTER, new HipsterState());
        stateMap.Add(CliqueEnum.FURBOI, new FurryState());
        stateMap.Add(CliqueEnum.JOCK, new JockState());
        stateMap.Add(CliqueEnum.SK8R, new SkaterState());
    }

    /// <summary>
    /// Change the players state to the given state.
    /// </summary>
    /// <param name="state"></param>
    public void ChangePlayerState(CliqueEnum state)
    {
        CurrentState = state;
        playerSprite.sprite = stateMap[state].GetStateSprite();
                player.moveSpeed = stateMap[state].GetStateSpeed();

        switch (state)
        {
            case CliqueEnum.BEARDSTER:
                //Call the singleton for the Player controller here and then go
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
