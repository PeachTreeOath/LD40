using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggroBehavior : MonoBehaviour {

    private CircleCollider2D AggorCollider;
    public float radius;
    

	// Use this for initialization
	public void Start () {
        if (gameObject.GetComponent<CircleCollider2D>() != null)
            AggorCollider = gameObject.GetComponent<CircleCollider2D>();
        else
            AggorCollider = gameObject.AddComponent<CircleCollider2D>();
        AggorCollider.radius = radius;
        AggorCollider.isTrigger = true;
    }

    //Update is called once per frame
    public void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name.Contains("Boy") || other.name.Contains("Player"))
        {
            Debug.Log("Player entered aggro");
        }
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.name.Contains("Boy") || other.name.Contains("Player"))
        {
            //Aggro Behavior??
        }
    }

    private void ChasePlayer()
    {
        //Todo
    }

}
