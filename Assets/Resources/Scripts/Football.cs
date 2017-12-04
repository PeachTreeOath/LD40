using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class Football : MonoBehaviour {

    private Rigidbody2D rbody;
    private FootballPickup pickup;

    private bool attracting = false;
    private float attractTimer = 0;
    private float attractPulseTimer = 0;

    public LureEvent lureEvent;

    public float attractDuration = 5;
    public float attractInterval = .5f;

    void Start() {
        rbody = GetComponent<Rigidbody2D>();
        pickup = GetComponentInChildren<FootballPickup>();

        if (lureEvent == null)
            lureEvent = new LureEvent();
    }

    void Update() {
        if(attracting) {
            attractTimer -= Time.deltaTime;
            if(attractTimer > 0) {
                attractPulseTimer -= Time.deltaTime;
                if(attractPulseTimer <= 0) {
                    Debug.Log("attract!");
                    lureEvent.Invoke(LureEvent.ATTRACT);
                    attractPulseTimer = attractInterval;
                }
            } else {
                attracting = false;
                Debug.Log("attract cancel");
                lureEvent.Invoke(LureEvent.CANCEL);
            }
        } 
    }

    public void ThrowFootball(Vector2 force)
    {
        pickup.throwTime = Time.time;
        rbody.AddForce(force, ForceMode2D.Impulse);
        AudioManager.instance.PlaySound("ball_throw");
        StartAttract();
    }

    public void OnPickUp() {
        AudioManager.instance.PlaySound("ball_pick_up");
        gameObject.SetActive(false);
        if(attracting) {
            Debug.Log("attract cancel");
            lureEvent.Invoke(LureEvent.CANCEL);
            attracting = false;
        }
    }

    protected void StartAttract() {
        attracting = true;
        attractTimer = attractDuration;
        attractPulseTimer = attractInterval;
    }
}
