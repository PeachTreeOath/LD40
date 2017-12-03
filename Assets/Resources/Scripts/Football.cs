using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class Football : MonoBehaviour {

    private Rigidbody2D rbody;
    private FootballPickup pickup;

    public UnityEvent lureEvent;

    void Start() {
        rbody = GetComponent<Rigidbody2D>();
        pickup = GetComponentInChildren<FootballPickup>();

        if (lureEvent == null)
            lureEvent = new UnityEvent();
    }

    public void ThrowFootball(Vector2 force)
    {
        pickup.throwTime = Time.time;
        rbody.AddForce(force, ForceMode2D.Impulse);
    }
}
