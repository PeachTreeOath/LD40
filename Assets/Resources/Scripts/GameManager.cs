using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager> {

    //public Level currentLevel;
    int currentLevelIndex;
    Level[] levels;
    Dictionary<CliqueEnum, bool> cliquesAvailable = new Dictionary<CliqueEnum, bool>();
    Level currentLevel;

    // Use this for initialization
    void Start () {
        LoadLevelNow();

    }
    public void GenLevel()
    {
        if (currentLevel == null)
        {
            currentLevel = ResourceLoader.instance.GetLevel(GlobalPersistentStats.instance.level);
        }
        else
        {
            Debug.Log("Load level called unnecessarily");
            Destroy(currentLevel.gameObject);
        }
    }
    void LoadLevelNow()
    {

        //currentLevelIndex = GlobalPersistentStats.instance.level - 1;
        //GameObject levelObj = GameObject.Find("Levels");
        //levels = levelObj.GetComponentsInChildren<Level>();
        //Level currentLevel = levels[currentLevelIndex];
        //currentLevel.Enable();
        //disable other levels
        //for(int i = 0; i < levels.Length; i++)
        // {
        //    if (i != currentLevelIndex)
        //    {
        //        levels[i].Disable();
        //    }
        //}


        //currentLevel = levels[GlobalPersistentStats.instance.level - 1];


        if (GlobalPersistentStats.instance.level == 1)
            GameObject.Find("Help1").GetComponent<Text>().enabled = true;
        else if (GlobalPersistentStats.instance.level == 2)
            GameObject.Find("Help2").GetComponent<Text>().enabled = true;
        else if (GlobalPersistentStats.instance.level == 3)
            GameObject.Find("Help3").GetComponent<Text>().enabled = true;
        else if (GlobalPersistentStats.instance.level == 4)
            GameObject.Find("Help4").GetComponent<Text>().enabled = true;

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
        if(GlobalPersistentStats.instance.level == 7)
        {
            SceneManager.LoadScene("Victory");
        }

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
        return currentLevel;
    }
}
