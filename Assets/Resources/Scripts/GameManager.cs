using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager> {

    public Level currentLevel;
    int currentLevelIndex;
    Level[] levels;

	// Use this for initialization
	void Start () {
        if(currentLevel == null)
        {
            LoadLevelNow();
        }
    }
	
    void LoadLevelNow()
    {
        GlobalPersistentStats.instance.level++;
        currentLevelIndex = GlobalPersistentStats.instance.level - 1;
        GameObject levelObj = GameObject.Find("Levels");
        levels = levelObj.GetComponentsInChildren<Level>();
        currentLevel = levels[currentLevelIndex];
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
        currentLevelIndex = GlobalPersistentStats.instance.level - 1;
        GameObject levelObj = GameObject.Find("Levels");
        levels = levelObj.GetComponentsInChildren<Level>();
        currentLevel = levels[currentLevelIndex];

        //nextLevel.gameObject.SetActive(true);
        //playEndingEffects();
        SceneManager.LoadScene("Game");
    }

    public Dictionary<CliqueEnum,bool> getCliquesAvailable()
    {
        if (currentLevel == null)
        {
            LoadLevelNow();
        }
        Dictionary<CliqueEnum, bool> cliquesAvailable = new Dictionary<CliqueEnum, bool>();
        if (currentLevel.furryCount == 0)
        {
            cliquesAvailable[CliqueEnum.FURBOI] = false;
        }
        if (currentLevel.footballerCount == 0)
        {
            cliquesAvailable[CliqueEnum.JOCK] = false;
        }
        if (currentLevel.skaterCount == 0)
        {
            cliquesAvailable[CliqueEnum.SK8R] = false;
        }
        return cliquesAvailable;
    }
}
