using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager> {

    //public Level currentLevel;
    int currentLevelIndex;
    Level[] levels;
    Dictionary<CliqueEnum, bool> cliquesAvailable = new Dictionary<CliqueEnum, bool>();
    //Level currentLevel;

    // Use this for initialization
    void Start () {
        LoadLevelNow();

    }
	
    void LoadLevelNow()
    {
       
        currentLevelIndex = GlobalPersistentStats.instance.level - 1;
        GameObject levelObj = GameObject.Find("Levels");
        levels = levelObj.GetComponentsInChildren<Level>();
        Level currentLevel = levels[currentLevelIndex];
        currentLevel.Enable();
        //disable other levels
        for(int i = 0; i < levels.Length; i++)
        {
            if (i != currentLevelIndex)
            {
                levels[i].Disable();
            }
        }


        currentLevel = levels[GlobalPersistentStats.instance.level - 1];
        cliquesAvailable = new Dictionary<CliqueEnum, bool>();
        cliquesAvailable[CliqueEnum.FURBOI] = currentLevel.furryCount == 0 ? false : true;
        cliquesAvailable[CliqueEnum.JOCK] = currentLevel.footballerCount == 0 ? false : true;
        cliquesAvailable[CliqueEnum.SK8R] = currentLevel.skaterCount == 0 ? false : true;
    }
	// Update is called once per frame
	void Update () {

        //Check for level complete
        if (LevelManager.instance.IsWinConditionSatisfied())
        {
            TransitionToNextLevel();
        }
    }

    protected override void Awake()
    {
        base.Awake();

        SetDontDestroy();
    }

    public void TransitionToNextLevel()
    {
        GlobalPersistentStats.instance.level++;
        LoadLevelNow();

        //nextLevel.gameObject.SetActive(true);
        //playEndingEffects();
        SceneManager.LoadScene("Game");
    }

    public Dictionary<CliqueEnum,bool> getCliquesAvailable()
    {
        
        return cliquesAvailable;
    }

    public Level GetCurrentLevel()
    {
        currentLevelIndex = GlobalPersistentStats.instance.level - 1;
       
        if (levels == null)
        {
            GameObject levelObj = GameObject.Find("Levels");
            levels = levelObj.GetComponentsInChildren<Level>();
        }

        return levels[GlobalPersistentStats.instance.level - 1];
    }
}
