using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager> {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        //Check for level complete
        if (LevelManager.instance.IsWinConditionSatisfied())
        {
            TransitionToNextLevel();
        }
    }

    public void TransitionToNextLevel()
    {
        GlobalPersistentStats.instance.level++;
        //playEndingEffects();
        SceneManager.LoadScene("Game");
    }
}
