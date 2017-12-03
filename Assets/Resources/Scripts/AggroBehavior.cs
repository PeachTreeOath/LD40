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
        //AggorCollider.offset = transform.position;

        if (AggorCollider.IsTouching(PlayerController.instance.GetComponent<Collider2D>()))
        {
            Debug.Log("Were touching");
        }
        if (AggorCollider.IsTouching(PlayerController.instance.GetComponent<Collider2D>()))
        {
            Debug.Log("Were really touching");
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == ("Player"))
        {
            Debug.Log("Player trig enter");
        }
        if (other.name.Contains("Enemy"))
        {
            Debug.Log("Enemy trig enter");
        }
        else
            Debug.Log("Something trig enter " + other.ToString());
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag.Contains("Player"))
        {
            Debug.Log("stay trigger");
        }
    }
    
    //public void OnTriggerEnter2D(Collider2D collision)
    //{
    //    Debug.Log("Someone entered");
    //}

    //public void OnTriggerExit2D(Collider2D collision)
    //{
    //    Debug.Log("Someone Left");
    //}

    private void ChasePlayer()
    {
        //Todo
    }

}
