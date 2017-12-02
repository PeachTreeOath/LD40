using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public CliqueEnum clique;

    [HideInInspector]
    public Rigidbody2D rbody; // public so the states can easily access

    private AEnemyState state;

	// Use this for initialization
	void Start () {
        rbody = GetComponent<Rigidbody2D>();
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
        state.DoUpdate(this);
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        PlayerStateController player = col.GetComponent<PlayerStateController>();
        if(player != null)
        {
            if(clique == player.GetPlayerState())
            {
                LevelManager.instance.incrementCurrentAffiliation(state.GetIncrementAffiliationSpeed());
            }
            else
            {
                LevelManager.instance.decrementCurrentAffiliation(state.GetDecrementAffiliationSpeed());
            }
        }
    }
}
