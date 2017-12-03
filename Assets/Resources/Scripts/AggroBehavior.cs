using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggroBehavior : MonoBehaviour {

    private CircleCollider2D AggorColliders;
    public float radius;
    

	// Use this for initialization
	public void Start () {
        AggorColliders = gameObject.AddComponent<CircleCollider2D>();
        AggorColliders.radius = radius;
        AggorColliders.isTrigger = true;
        AggorColliders.offset = transform.position;
	}

    //Update is called once per frame
    public void Update()
    {
        if (AggorColliders.IsTouching(PlayerController.instance.GetComponent<Collider2D>()))
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
