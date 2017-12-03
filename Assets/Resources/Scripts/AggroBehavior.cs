using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggroBehavior : MonoBehaviour {

    private CircleCollider2D AggorCollider;
    public float radius;
    

	// Use this for initialization
	public void Start () {
        AggorCollider = gameObject.AddComponent<CircleCollider2D>();
        AggorCollider.radius = radius;
        //AggorCollider.isTrigger = true;
    }

    //Update is called once per frame
    public void Update()
    {
        AggorCollider.offset = transform.position;

        if (AggorCollider.IsTouching(PlayerController.instance.GetComponent<Collider2D>()))
        {
            Debug.Log("We touching");
        }
        if (AggorCollider.IsTouching(PlayerController.instance.GetComponent<Collider2D>()))
        {
            Debug.Log("We really touching");
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Contains("Player"))
        {
            Debug.Log("We really touching");
        }
        if (other.name.Contains("Enemy"))
        {
            Debug.Log("Enemy really touching");
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("hope it is");
        }
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("Player"))
        {
            Debug.Log("We did it");
        }
        if (collision.gameObject.name.Contains("Enemy"))
        {
            Debug.Log("THis is an enemy");
        }
        Debug.Log("Someone entered1" +collision.gameObject.ToString() + collision.ToString() + collision.GetType() + collision.rigidbody + collision.Equals(PlayerController.instance.GetComponent<Collider2D>()));
    }
    public void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Debug.Log("HERRERE");
        }
    }
    public void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("Enemy"))
        {
            Debug.Log("enemy left");
        }
        Debug.Log("Someone Left2");
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
