using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JockStateBehaviour : APlayerBehaviour
{
    /// <summary>
    /// The Gameobject for the football.
    /// </summary>
    private GameObject FootballGO;


    

    /// <summary>
    /// Throw the Foosball
    /// </summary>
    public override void ExecuteBehaviourAction()
    {
        PlayerController player = PlayerController.instance;

        if (FootballGO == null)
            GameObject.FindGameObjectWithTag("Football");
        

        
    
    }
}
