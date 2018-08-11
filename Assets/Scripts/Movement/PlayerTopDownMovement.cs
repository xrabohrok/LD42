using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTopDownMovement : TopDownMovement {

    public static PlayerTopDownMovement instance;

    public enum PlayerState { Walking, Standing, Flinching, Attacking, }
    public PlayerState playerState;
    public GameObject bullet;

    public GameObject holding;
    //public GameObject interactionZone;

    public void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }


    //Start overrides the Start function of TopDownMovement
    protected override void Start()
    {
        facing = Facing.South;

        base.Start();
    }

    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    public override void FixedUpdate()
    {
        HandleInput();
    }

    public void HandleInput()
    {
        HandleMovementInput();
        HandleShootingInput();
    }

    public void HandleMovementInput()
    {
        Vector2 velocity = new Vector2(0, 0);

        if (Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow))
        {
            velocity.Set(velocity.x, speed);
            facing = Facing.North;
        }
        if (Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.UpArrow))
        {
            velocity.Set(velocity.x, -(speed));
            facing = Facing.South;
        }

        if (Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
        {
            velocity.Set(-(speed), velocity.y);
            facing = Facing.West;
        }
        if (Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow))
        {
            velocity.Set(speed, velocity.y);
            facing = Facing.East;
        }

        SetTriggers();

        rb2D.velocity = velocity;
    }

    public void HandleShootingInput()
    {
        float speed = 5.0f;
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 playerPos = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = new Vector2(worldPoint.x, worldPoint.y) - playerPos;
            direction.Normalize();
            // Spawn bullet object from player position moving in vector between player and mouse cursor.
            GameObject shotFired = Instantiate(bullet, new Vector3(playerPos.x, playerPos.y, 0), Quaternion.identity);
            shotFired.GetComponent<Rigidbody2D>().velocity = direction * speed;
        }
    }
}
