using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{

    public float moveSpeed;

    private Rigidbody2D rbody;
    private SpriteRenderer playerSprite;
    private APlayerBehaviour behaviour;

    private Vector2 direction = new Vector2();

    public bool hasBall = true;

    [HideInInspector]
    public bool inChangingRoom { get; set; }

    public bool canChangeFaction {
        get {
            return LevelManager.instance.overrideChangingRoomCheat || inChangingRoom;
        }
    }

    // Use this for initialization
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        playerSprite = GetComponentInChildren<SpriteRenderer>();
        inChangingRoom = false;
    }

    // Update is called once per frame
    void Update()
    {
        float currentSpeed = moveSpeed;
        if (Input.GetAxisRaw("Horizontal") != 0 && Input.GetAxisRaw("Vertical") != 0)
        {
            currentSpeed *= 0.707f;
        }

        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        UpdateFactionChange();

        if(Input.GetKeyDown(KeyCode.Space) && !PlayerStateController.instance.GetPlayerState().Equals(CliqueEnum.NORMAL))
        {
            behaviour.ExecuteBehaviourAction();
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

    

    protected void UpdateFactionChange() {
        if (!canChangeFaction) return;

        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            PlayerStateController.instance.ChangePlayerState(CliqueEnum.SK8R);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            PlayerStateController.instance.ChangePlayerState(CliqueEnum.JOCK);
        }

    }
}
