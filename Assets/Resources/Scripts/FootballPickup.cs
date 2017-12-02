using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootballPickup : MonoBehaviour
{

    private Rigidbody2D rbody;
    private float throwTime;
    public float throwTimeDeltaNeededForPickup = 1f;

    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            if (Time.time > throwTimeDeltaNeededForPickup + throwTime)
            {
                PlayerController player = PlayerController.instance;
                player.hasBall = true;
                gameObject.SetActive(false);
            }
        }
    }

    public void ThrowFootball(Vector2 force)
    {
        throwTime = Time.time;
        rbody.AddForce(force, ForceMode2D.Impulse);
    }
}
