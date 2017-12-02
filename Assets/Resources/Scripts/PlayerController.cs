using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed;

    private Rigidbody2D rbody;
    private SpriteRenderer playerSprite;

    // Use this for initialization
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        playerSprite = GetComponent<SpriteRenderer>();
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
            PlayerStateController.instance.ChangePlayerState(CliqueEnum.SK8R);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            PlayerStateController.instance.ChangePlayerState(CliqueEnum.JOCK);
        }

        Vector3 deltaPos = new Vector3(currentSpeed * Input.GetAxisRaw("Horizontal"), currentSpeed * Input.GetAxisRaw("Vertical"), 0);
        rbody.MovePosition(deltaPos + transform.position);
    }

    public void ChangeSprite(Sprite newSprite)
    {
        playerSprite.sprite = newSprite;
    }

    public void ChangeSpeed(float newSpeed)
    {
        moveSpeed = newSpeed;
    }
}
