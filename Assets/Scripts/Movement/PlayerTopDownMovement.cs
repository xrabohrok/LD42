﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerTopDownMovement : TopDownMovement {

    public static PlayerTopDownMovement instance;

    public GameObject cleanerPrefab;
    public GameObject gunPrefab;

    public GameObject holding;


    //public GameObject interactionZone;


    public GameObject[] gunsInInventory;
    private GameObject currentCleaner;
    private GameObject currentGun;


    private bool lockedMovement;

    public void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }


    //Start overrides the Start function of TopDownMovement
    public override void Start()
    {
        EquipGun();
        EquipCleaner();

        base.Start();
    }

    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    public void Update()
    {
        PlayerStatus playerStatus = this.GetComponent<PlayerStatus>();
        if (!playerStatus.IsDead)
        { 
            if (!playerStatus.IsEquiped)
            {
                EquipGun();
                playerStatus.IsEquiped = true;
            }
            HandleInput();

            if (currentCleaner == null)
            {
                EquipCleaner();
            }
        }
    }

    private void EquipCleaner()
    {
        currentCleaner = Instantiate(cleanerPrefab, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity, gameObject.transform);
        currentCleaner.transform.localScale = gameObject.transform.localScale;
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
        if (!lockedMovement)
        {
            if (Input.GetAxis("Vertical") > 0 && !Input.GetKey(KeyCode.DownArrow))
            {
                velocity.Set(velocity.x, speed);
            }
            if (Input.GetAxis("Vertical") < 0 && !Input.GetKey(KeyCode.UpArrow))
            {
                velocity.Set(velocity.x, -(speed));
            }

            if (Input.GetAxis("Horizontal") < 0 && !Input.GetKey(KeyCode.RightArrow))
            {
                velocity.Set(-(speed), velocity.y);
            }
            if (Input.GetAxis("Horizontal") > 0 && !Input.GetKey(KeyCode.LeftArrow))
            {
                velocity.Set(speed, velocity.y);
            }
        }
        else
        {
            velocity.Set(0,0);
        }
        
        rb2D.velocity = velocity;

        HandleMovementAnimations();
    }

    public void HandleMovementAnimations()
    {
        if (!rb2D.velocity.y.Equals(0.0f) || !rb2D.velocity.x.Equals(0.0f))
        {
            animator.SetFloat("speed", 1);
        }
        else if (rb2D.velocity.y.Equals(0.0f) && rb2D.velocity.x.Equals(0.0f))
        {
            animator.SetFloat("speed", 0);
        }
        
        //Call base triggers handler
        SetTriggers();
    }

    public void HandleShootingInput()
    {
        //if currentGun gun exists
        if (currentGun)
        {
            currentGun.GetComponent<BaseGunScript>().TryShoot();
        }
    }

    public void HandleCleanerInput()
    {
        var gunPart = currentCleaner.GetComponent<IGun>();
        if (gunPart != null)
        {
            if (Input.GetMouseButtonDown(1))
            {
                gunPart.Fire();
                lockedMovement = true;
                animator.SetBool("dropped", true);
            }
            else if( Input.GetMouseButtonUp(1))
            {
                gunPart.StoppedFire();
                lockedMovement = false;
                animator.SetBool("dropped", false);
            }
        }
    }

    public void SetCurrentGun(GameObject newGun)
    {
        currentGun = newGun;
    }

    public void EquipGun()
    {
        if(currentGun == null)
        {
            currentGun = Instantiate(gunPrefab, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
            currentGun.transform.localScale = this.transform.localScale;
        }
    }

    public void Respawn()
    {
        animator.SetTrigger("respawn");
    }
}
