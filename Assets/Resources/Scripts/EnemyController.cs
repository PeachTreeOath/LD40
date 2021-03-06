﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public CliqueEnum clique;

    //Stores affinity per enemy so we cannot max affinity on one player.
    public float personalAffinity;
    public float personalAffinityMax;

    public float incrementAffinitySpeed;
    public float decrementAffinitySpeed;

    [HideInInspector]
    public Rigidbody2D rbody; // public so the states can easily access
    [HideInInspector]
    public Navigation navigation;

    // Use this for initialization
    protected virtual void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        navigation = GetComponent<Navigation>();
        if (navigation == null)
        {
            Debug.LogWarning(this.name + " navigation is null.");
        }
    }

    public virtual float GetStateSpeed()
    {
        return .1f;
    }

    public void ChangeFavor()
    {
        PlayerStateController player = PlayerStateController.instance;
        if (clique == player.GetPlayerState())
        {
            if (personalAffinity < personalAffinityMax)
            {
                //This logic is so that we do not go above personal affinity max, and also so levelCliqueAffinity does not get
                //incremented any more than personal.
                float oldPersonalAffinity = personalAffinity;
                float newPersonalAffinity = Mathf.Min(personalAffinityMax, personalAffinity + incrementAffinitySpeed * Time.deltaTime);
                float deltaPersonalAffinity = newPersonalAffinity - oldPersonalAffinity;
                IncrementPersonalAffinity(deltaPersonalAffinity);
                LevelManager.instance.incrementCurrentAffinity(clique, deltaPersonalAffinity);
            }
        }
        else
        {
            float oldPersonalAffinity = personalAffinity;
            float newPersonalAffinity = Mathf.Max(0, personalAffinity + decrementAffinitySpeed * Time.deltaTime);
            float deltaPersonalAffinity = newPersonalAffinity - oldPersonalAffinity;
            IncrementPersonalAffinity(deltaPersonalAffinity);
            LevelManager.instance.incrementCurrentAffinity(clique, deltaPersonalAffinity);
        }
    }

    public virtual float GetPersonalAffinity()
    {
        return personalAffinity;
    }

    public virtual void IncrementPersonalAffinity(float incrementAmount)
    {
        if (incrementAmount >= 0)
            personalAffinity = Mathf.Min(personalAffinityMax, personalAffinity + incrementAmount);
        else
            personalAffinity = Mathf.Max(0, personalAffinity + incrementAmount);
    }
}
