using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : Singleton<LevelManager> {

    public float requiredAffiliationPerClique;
    public float totalAffiliationPerClique;
    public float defaultAffiliation;
    public float incrementAffiliationAmount;
    public float decrementAffiliationAmount;

    public bool overrideChangingRoomCheat;

    private Dictionary<CliqueEnum, float> cliqueAffiliations = new Dictionary<CliqueEnum, float>();
    private Canvas canvas;
    private Text skaterCanvasValue;
    private Text furryCanvasValue;
    private Text jockCanvasValue;

	// Use this for initialization
	void Start () {
        //Set default affiliation for all affiliations
        foreach (CliqueEnum clickEnum in Enum.GetValues(typeof(CliqueEnum)))
        {
            cliqueAffiliations.Add(clickEnum, defaultAffiliation);
        }
        
        //Get the canvas to use later
        jockCanvasValue = GameObject.Find("Jocks Affinity Value").GetComponent<Text>();
        skaterCanvasValue = GameObject.Find("Skaters Affinity Value").GetComponent<Text>();
        furryCanvasValue = GameObject.Find("Furries Affinity Value").GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {
        UpdateCanvas();
	}

    void UpdateCanvas()
    {
        jockCanvasValue.text = cliqueAffiliations[CliqueEnum.JOCK].ToString();
        skaterCanvasValue.text = cliqueAffiliations[CliqueEnum.SK8R].ToString();
        furryCanvasValue.text = cliqueAffiliations[CliqueEnum.FURBOI].ToString();
    }

    public float getCurrentAffiliation(CliqueEnum clique)
    {
        return cliqueAffiliations[clique];
    }

    public void setCurrentAffiliation(CliqueEnum clique, float newAffiliation)
    {
        cliqueAffiliations[clique] = newAffiliation;
    }

    public void incrementCurrentAffiliation(CliqueEnum clique, float incrementAmount)
    {
        if (incrementAmount >= 0 )
            cliqueAffiliations[clique] = Math.Min(totalAffiliationPerClique, cliqueAffiliations[clique] + incrementAmount);
        else
            cliqueAffiliations[clique] = Math.Max(0, cliqueAffiliations[clique] + incrementAmount);
    }

}
