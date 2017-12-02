using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{

    public float moveSpeed;

    private Rigidbody2D rbody;

    // Use this for initialization
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float currentSpeed = moveSpeed;
        if (Input.GetAxisRaw("Horizontal") != 0 && Input.GetAxisRaw("Vertical") != 0)
        {
            currentSpeed *= 0.707f;
        }

        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("Swap to furry");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log("Swap to skater");
        }

        Vector3 deltaPos = new Vector3(currentSpeed * Input.GetAxisRaw("Horizontal"), currentSpeed * Input.GetAxisRaw("Vertical"), 0);
        rbody.MovePosition(deltaPos + transform.position);
    }
}
