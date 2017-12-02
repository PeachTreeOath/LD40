using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

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

    private AEnemyState state;

	// Use this for initialization
	void Start () {
        rbody = GetComponent<Rigidbody2D>();
        navigation = GetComponent<Navigation>();
        if(navigation == null)
        {
            Debug.LogWarning(this.name + " navigation is null.");
        }
        Init(); //TODO remove depending on how we spawn enemies
    }
	
    /// <summary>
    /// Manually called init since I'm not sure how we're gonna spawn enemies yet
    /// </summary>
    public void Init()
    {
        //TODO change to map somewhere
        if(clique == CliqueEnum.JOCK)
        {
            state = new JockEnemyState();
        }
        if (clique == CliqueEnum.SK8R)
        {
            state = new SkaterEnemyState();
        }
    }

	// Update is called once per frame
	void Update () {
        if (state != null)
        {
            state.DoUpdate(this);
        }
	}

    void OnTriggerStay2D(Collider2D col)
    {
        PlayerStateController player = col.GetComponent<PlayerStateController>();
        if(player != null && state != null) //TODO: Change state check later when state init changes
        {
            if(clique == player.GetPlayerState())
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
