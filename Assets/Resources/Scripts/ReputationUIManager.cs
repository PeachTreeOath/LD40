using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReputationUIManager : Singleton<ReputationUIManager> {

    public Image furryFill;
    public Image skaterFill;
    public Image jockFill;

    public float minWidth = 0.07f;

    public void UpdateFill(int barNum, float ratio)
    {
        ratio /= 100;
        ratio *= 0.93f;

        float fillRatio = ratio+minWidth;
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
