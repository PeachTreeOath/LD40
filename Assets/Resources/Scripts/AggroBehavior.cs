using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggroBehavior : MonoBehaviour {

    private CircleCollider2D AggorColliders;
    public GameObject Player;
    public float radius;
    public CliqueEnum clique;

	// Use this for initialization
	public void Start () {
        AggorColliders = new CircleCollider2D();
        AggorColliders.radius = radius;
        AggorColliders.isTrigger = true;
        AggorColliders.offset = transform.position;
	}

    //Update is called once per frame
    public void Update()
    {
        if (AggorColliders.IsTouching(Player.GetComponent<Collider2D>()))
        {
            Debug.Log("We touching");
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Someone entered");
    }

    private void ChasePlayer()
    {
        //Todo
    }

}
