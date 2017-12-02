using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateController : Singleton<PlayerStateController>
{

    /// <summary>
    /// The Current state for the player.
    /// </summary>
    public CliqueEnum CurrentState;

    private PlayerController player;
    private Dictionary<CliqueEnum, APlayerState> stateMap = new Dictionary<CliqueEnum, APlayerState>();

    void Start()
    {
        player = GetComponent<PlayerController>();

        stateMap.Add(CliqueEnum.BEARDSTER, new HipsterPlayerState());
        stateMap.Add(CliqueEnum.FURBOI, new FurryPlayerState());
        stateMap.Add(CliqueEnum.JOCK, new JockPlayerState());
        stateMap.Add(CliqueEnum.SK8R, new SkaterPlayerState());
    }

    /// <summary>
    /// Change the players state to the given state.
    /// </summary>
    /// <param name="state"></param>
    public void ChangePlayerState(CliqueEnum state)
    {
        CurrentState = state;
        player.ChangeSprite(stateMap[state].GetStateSprite());
        player.ChangeSpeed(stateMap[state].GetStateSpeed());
        //player.ChangeBehaviour(stateMap[state].GetPlayerBehaviour());

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
