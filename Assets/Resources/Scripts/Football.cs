using UnityEngine;
using System.Collections;

public class Football : MonoBehaviour {

    private Rigidbody2D rbody;
    private FootballPickup pickup;

    void Start() {
        rbody = GetComponent<Rigidbody2D>();
        pickup = GetComponentInChildren<FootballPickup>();
    }

    public void ThrowFootball(Vector2 force)
    {
        pickup.throwTime = Time.time;
        rbody.AddForce(force, ForceMode2D.Impulse);
    }
}
