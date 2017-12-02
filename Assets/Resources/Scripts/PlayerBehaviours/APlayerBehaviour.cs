using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A Base representation for what a players behaviour is. 
/// all behaviour extend this class and their implementation defines the execution
/// of the action button.
/// </summary>
public abstract class APlayerBehaviour  {

    /// <summary>
    /// Execute the player state specific action.
    /// </summary>
    public abstract void ExecuteBehaviourAction();
}
