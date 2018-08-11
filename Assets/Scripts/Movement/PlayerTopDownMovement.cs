﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTopDownMovement : TopDownMovement {

    public static PlayerTopDownMovement instance;

    public enum PlayerState { Walking, Standing, Flinching, Attacking, }
    public PlayerState playerState;

  
    public GameObject cleaner;

    public GameObject holding;
    //public GameObject interactionZone;


    public GameObject currentGun;
    public GameObject[] gunsInInventory;
    

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
        currentGun = Instantiate(currentGun, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);

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
        HandleCleanerInput();
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
        currentGun.GetComponent<BaseGunScript>().Shoot();
    }

    public void HandleCleanerInput()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Vector2 playerPos = new Vector2(this.transform.position.x, this.transform.position.y);
            GameObject cleanerArea = Instantiate(cleaner, new Vector3(playerPos.x, playerPos.y, 0), Quaternion.identity);

        }

    }


    
}
