using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReputationUIManager : Singleton<ReputationUIManager> {

    public RectTransform furryFill;
    public RectTransform skaterFill;
    public RectTransform jockFill;

    public float maxWidth;
    // Use this for initialization
    void Start() {
        maxWidth = furryFill.rect.width;
    }

    public void UpdateFill(int barNum, float ratio)
    {

        if(barNum == 0)
        {
            // furryFill. = new Vector3(position, furryFill.position.y, furryFill.position.z);
            //furryFill.sizeDelta = 
        }

      //  if (barNum == 1)
           // skaterFill.position = new Vector3(position, skaterFill.position.y, skaterFill.position.z);

      //  if (barNum == 2)
        //    jockFill.position = new Vector3(position, jockFill.position.y, jockFill.position.z);
            
    }
}
