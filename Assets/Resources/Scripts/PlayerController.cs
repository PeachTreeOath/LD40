﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{

    public float moveSpeed;

    public float skateMoveSpeed;
    public float skateTurnSpeed;

    private Rigidbody2D rbody;
    private SpriteRenderer playerSprite;
    private Animator playerAnimator;
    private APlayerBehaviour behaviour;

    private Vector2 direction = new Vector2();

    [SerializeField]
    public GameObject FootballGO;
    public bool hasBall = true;


    public bool goingForward;
    public bool canGrabTail = true;

    [HideInInspector]
    public bool inChangingRoom { get; set; }

    public bool canChangeFaction
    {
        get
        {
            return LevelManager.instance.overrideChangingRoomCheat || inChangingRoom;
        }
    }

    private Vector2 Vector2FromAngle(float a) {
        a *= Mathf.Deg2Rad;
        return new Vector2(Mathf.Cos(a), Mathf.Sin(a));
    }

    // Use this for initialization
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        playerSprite = GetComponentInChildren<SpriteRenderer>();
        playerAnimator = GetComponentInChildren<Animator>();
        inChangingRoom = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerStateController.instance.GetPlayerState().Equals(CliqueEnum.SK8R))
        {
            //
            // Rotate the sprite and move the player object instead of 
            // rotating and moving the player object
            // so the minimap doesn't rotate since it's anchored to the player object
            //
            UpdateFactionChange();
            float currentSpeed = skateMoveSpeed * Time.deltaTime;
            float z = playerSprite.transform.rotation.z - Input.GetAxisRaw("Horizontal") * skateTurnSpeed * Time.deltaTime;
            playerSprite.transform.Rotate(0, 0, z);
            Vector2 v = (playerSprite.flipX ? 1 : -1) * Vector2FromAngle(playerSprite.transform.rotation.eulerAngles.z) * currentSpeed + rbody.position;
            rbody.MovePosition(v);
        } else
        {
            playerSprite.transform.rotation = Quaternion.Euler(0, 0, 0);
            float currentSpeed = moveSpeed * Time.deltaTime;
            if (Input.GetAxisRaw("Horizontal") > 0)
            {
                playerSprite.flipX = true;
                goingForward = true;
            }
            else if (Input.GetAxisRaw("Horizontal") < 0)
            {
                playerSprite.flipX = false;
                goingForward = false;
            }

            direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            if (Input.GetAxisRaw("Horizontal") != 0 && Input.GetAxisRaw("Vertical") != 0)
            {
                //multiply diagonals by sin(45) so it moves properly circlewise
                currentSpeed *= 0.707f;
                /**
                 * currently doing this to make sure football flies correct speed at 45 deg
                 *  angle, this could cause other issues if direction is used elsewhere,
                 *  BEWARE
                 */
                direction *= 0.707f;
            }

            UpdateFactionChange();

            if (Input.GetKeyDown(KeyCode.Space) && !PlayerStateController.instance.GetPlayerState().Equals(CliqueEnum.NORMAL))
            {
                if (behaviour != null)
                    behaviour.ExecuteBehaviourAction();
            }

            Vector3 deltaPos = new Vector3(currentSpeed * Input.GetAxisRaw("Horizontal"), currentSpeed * Input.GetAxisRaw("Vertical"), 0);
            rbody.MovePosition(deltaPos + transform.position);
        }
    }

    public void ChangeSprite(Sprite newSprite)
    {
        playerSprite.sprite = newSprite;
    }

    public void ChangeSpeed(float newSpeed)
    {
        moveSpeed = newSpeed;
    }

    public void ChangeBehaviour(APlayerBehaviour newBehaviour)
    {
        behaviour = newBehaviour;
    }

    /// <summary>
    /// Return the input X,Y for the player.
    /// </summary>
    /// <returns></returns>
    public Vector2 GetPlayerDirection()
    {
        return direction;
    }

    protected void UpdateFactionChange()
    {
        if (!canChangeFaction) return;

        if (PlayerStateController.instance.GetPlayerState().Equals(CliqueEnum.FURBOI))
        {
            if (!canGrabTail)
                behaviour.ExecuteBehaviourAction();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            PlayerStateController.instance.ChangePlayerState(CliqueEnum.FURBOI);
            playerAnimator.enabled = true;
            playerAnimator.runtimeAnimatorController = ResourceLoader.instance.furAnim;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            PlayerStateController.instance.ChangePlayerState(CliqueEnum.SK8R);
            playerAnimator.enabled = false;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            PlayerStateController.instance.ChangePlayerState(CliqueEnum.JOCK);
            playerAnimator.enabled = true;
            playerAnimator.runtimeAnimatorController = ResourceLoader.instance.jockAnim;
        }

    }

    public PlayerStateController GetState()
    {
        return GetComponent<PlayerStateController>();
    }
}
