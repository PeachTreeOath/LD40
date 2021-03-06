﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : Singleton<LevelManager>
{

    public float totalAffiliationPerClique;
    public float defaultAffiliation;
    public float incrementAffiliationAmount;
    public float decrementAffiliationAmount;
    public float affinityRequiredPerFactionToWin;


    public bool overrideChangingRoomCheat;


    private Dictionary<CliqueEnum, float> cliqueAffiliations = new Dictionary<CliqueEnum, float>();

    public Image furryComplete;
    public Image skaterComplete;
    public Image jockComplete;
    // Use this for initialization
    void Start()
    {

        //Set default affiliation for all affiliations
        foreach (CliqueEnum clickEnum in Enum.GetValues(typeof(CliqueEnum)))
        {
            cliqueAffiliations.Add(clickEnum, defaultAffiliation);


        }



        //Get the canvas to use later
        //jockCanvasValue = GameObject.Find("Jocks Affinity Value").GetComponent<Text>();
        //skaterCanvasValue = GameObject.Find("Skaters Affinity Value").GetComponent<Text>();
        //furryCanvasValue = GameObject.Find("Furries Affinity Value").GetComponent<Text>();

        SpawnEnemies();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCanvas();
    }

    void UpdateCanvas()
    {
        /*
        jockCanvasValue.text = cliqueAffiliations[CliqueEnum.JOCK].ToString();
        skaterCanvasValue.text = cliqueAffiliations[CliqueEnum.SK8R].ToString();
        furryCanvasValue.text = cliqueAffiliations[CliqueEnum.FURBOI].ToString();
        */

        ReputationUIManager.instance.UpdateFill(0, cliqueAffiliations[CliqueEnum.FURBOI]);
        ReputationUIManager.instance.UpdateFill(1, cliqueAffiliations[CliqueEnum.SK8R]);
        ReputationUIManager.instance.UpdateFill(2, cliqueAffiliations[CliqueEnum.JOCK]);
    }

    public float getCurrentAffiliation(CliqueEnum clique)
    {
        return cliqueAffiliations[clique];
    }

    public void setCurrentAffiliation(CliqueEnum clique, float newAffiliation)
    {
        cliqueAffiliations[clique] = newAffiliation;
    }

    public void incrementCurrentAffinity(CliqueEnum clique, float incrementAmount)
    {
        if (incrementAmount >= 0)
            cliqueAffiliations[clique] = Math.Min(totalAffiliationPerClique, cliqueAffiliations[clique] + incrementAmount);
        else
            cliqueAffiliations[clique] = Math.Max(0, cliqueAffiliations[clique] + incrementAmount);
    }

    public bool IsWinConditionSatisfied()
    {
        Level currentLevel = GameManager.instance.GetCurrentLevel();
        Dictionary<CliqueEnum, bool> cliquesPresentMap = GameManager.instance.getCliquesAvailable();
        foreach (KeyValuePair<CliqueEnum, float> cliqueAffinityEntry in cliqueAffiliations)
        {
            if (cliqueAffinityEntry.Value < affinityRequiredPerFactionToWin)
            {
                switch (cliqueAffinityEntry.Key)
                {
                    case CliqueEnum.FURBOI:
                        furryComplete.enabled = false;
                        break;
                    case CliqueEnum.SK8R:
                        skaterComplete.enabled = false;
                        break;
                    case CliqueEnum.JOCK:
                        jockComplete.enabled = false;
                        break;

                }
            }
            else
            {
                switch (cliqueAffinityEntry.Key)
                {
                    case CliqueEnum.FURBOI:
                        furryComplete.enabled = true;
                        break;
                    case CliqueEnum.SK8R:
                        skaterComplete.enabled = true;
                        break;
                    case CliqueEnum.JOCK:
                        jockComplete.enabled = true;
                        break;
                }
            }
        }
        foreach (KeyValuePair<CliqueEnum, float> cliqueAffinityEntry in cliqueAffiliations)
        {
            if (cliqueAffinityEntry.Key != CliqueEnum.NORMAL &&
    cliquesPresentMap[cliqueAffinityEntry.Key] &&
    cliqueAffinityEntry.Value < affinityRequiredPerFactionToWin)
            {
                return false;
            }
        }

        return true;
    }

    private void SetSortingOrder(GameObject go, int sortingOrder)
    {
        SpriteRenderer[] sprites = go.GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer sprite in sprites)
        {
            if (sprite.name.Equals("square"))
                sprite.sortingOrder = sortingOrder;
        }
        go.GetComponentInChildren<SpriteMask>().isCustomRangeActive = true;
        go.GetComponentInChildren<SpriteMask>().frontSortingOrder = sortingOrder + 1;
        go.GetComponentInChildren<SpriteMask>().backSortingOrder = sortingOrder;
    }

    private void SpawnEnemies()
    {
        Level lvl = GameManager.instance.GetCurrentLevel();
        if (lvl == null)
        {
            int level = GlobalPersistentStats.instance.level;
            GameManager.instance.GenLevel();
            GameManager.instance.LoadLevelNow();
            GameObject levelObj = GameObject.Find("Level" + level + "(Clone)");
            //levelObj.SetActive(true);
            lvl = levelObj.GetComponent<Level>();
        }
        //Level lvl = GameManager.instance.GetCurrentLevel();
        WaypointGroup wg = lvl.GetComponentInChildren<WaypointGroup>();
        Waypoint[] wps = wg.GetComponentsInChildren<Waypoint>();
        //Waypoint[] wps = GameObject.Find("Waypoints" + GlobalPersistentStats.instance.level).GetComponentsInChildren<Waypoint>();
        int sortingOrder = 0;

        for (int i = 0; i < lvl.furryCount; i++)
        {
            Waypoint wp = wps[UnityEngine.Random.Range(0, wps.Length)];
            GameObject go = Instantiate(ResourceLoader.instance.furryPrefab, wp.transform.position, Quaternion.identity);
            go.GetComponent<EnemyController>().personalAffinityMax = 100.0f / lvl.furryCount;
            SetSortingOrder(go, sortingOrder++);
        }

        for (int i = 0; i < lvl.skaterCount; i++)
        {
            Waypoint wp = wps[UnityEngine.Random.Range(0, wps.Length)];
            GameObject go = Instantiate(ResourceLoader.instance.skaterPrefab, wp.transform.position, Quaternion.identity);
            go.GetComponent<EnemyController>().personalAffinityMax = 100.0f / lvl.skaterCount;
            SetSortingOrder(go, sortingOrder++);
        }

        for (int i = 0; i < lvl.footballerCount; i++)
        {
            Waypoint wp = wps[UnityEngine.Random.Range(0, wps.Length)];
            GameObject go = Instantiate(ResourceLoader.instance.jockPrefab, wp.transform.position, Quaternion.identity);
            go.GetComponent<EnemyController>().personalAffinityMax = 100.0f / lvl.footballerCount;
            SetSortingOrder(go, sortingOrder++);
        }
        for (int i = 0; i < lvl.nobodyCount; i++)
        {
            Waypoint wp = wps[UnityEngine.Random.Range(0, wps.Length)];
            GameObject go = Instantiate(ResourceLoader.instance.nobodyPrefab, wp.transform.position, Quaternion.identity);
        }
    }

}
