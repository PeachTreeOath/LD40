using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReputationUIManager : Singleton<ReputationUIManager> {

    public Image furryFill;
    public Image skaterFill;
    public Image jockFill;

    public float minWidth = 0.07f;
    // Use this for initialization
    void Start() {
        
    }

    public void UpdateFill(int barNum, float ratio)
    {
        float fillRatio = Mathf.Max(minWidth, ratio);

        if(barNum == 0)
        {
            furryFill.fillAmount = fillRatio;
        }

        if (barNum == 1)
            skaterFill.fillAmount = fillRatio;


        if (barNum == 2)
            jockFill.fillAmount = fillRatio;
    }
}
