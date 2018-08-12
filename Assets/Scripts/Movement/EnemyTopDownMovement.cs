﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTopDownMovement : TopDownMovement {

    protected string currentState;  // This is ripped from animator. DO NOT SET IT OTHERWISE
    public GameObject player;

    private bool enemyCanMove = true;

    public float interactionRange = 1f;
    public int hp = 100;
    private bool isDead = false;
    
    
    public GameObject gooPrefab;

    public float InteractionRange
    {
        get
        {
            return interactionRange;
        }

        set
        {
            interactionRange = value;
        }
    }

   

    private float curTime = 0f;

    //Start overrides the Start function of TopDownMovement
    protected override void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        facing = Facing.South;
        isDead = false;
        base.Start();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if(this.hp <= 0 && !isDead)
        {
            rb2D.velocity = Vector2.zero;
            Death();
           
            return;
        }
        else if (isDead)
        {
            rb2D.velocity = Vector2.zero;
        }

        //can the enemy move and is the player alive
        if (enemyCanMove && !player.GetComponent<PlayerStatus>().IsDead)
        {
            //Enemy should move
            curTime += Time.deltaTime;
            float playerDist = FindPlayerDistance();
            if (animator.GetFloat("Speed") < 1)
            {
                //If enemy is not moving
                if (playerDist < InteractionRange)
                {
                   //Player is within interaction area
                   //Interact with player

                }
                else
                {
                    //Player is not within interaction area
                    //Find player and move towards
                    Vector3 unitVelocity = FindPlayer();
                    MoveTowardPlayer(unitVelocity);
                }
            }
            else if (animator.GetFloat("Speed") >= 1)
            {
                //If Enemy is moving
                if (playerDist < InteractionRange)
                {
                    //If player is within interaction zone
                    //Set speed to zero, STOP
                    animator.SetFloat("Speed", 0);
                    rb2D.velocity = Vector2.zero;
                }
                else
                {
                    //Else player is not within interaction Zone
                    //Find player, move towards
                    Vector3 unitVelocity = FindPlayer();
                    MoveTowardPlayer(unitVelocity);
                }
            }
        }
        else
        {
            //Enemy should not be moving
            rb2D.velocity = Vector2.zero;
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            TakeDamage(collision.gameObject.GetComponent<BulletScript>().bulletDamage);
            Destroy(collision.gameObject);
        }
    }

    public void TakeDamage(int amount)
    {
        this.hp -= amount;
    }

    
    public void Death()
    {
        isDead = true;
        Instantiate(gooPrefab, new Vector3(this.transform.position.x, this.transform.position.y, 0), Quaternion.identity);
        EventManager.TriggerEvent("ENEMY_DIED");
        animator.SetTrigger("die");   
       
    }

    private void CleanUp()
    {
        Destroy(gameObject);
    }
    
    public void OnDestroy()
    {
        
    }

    public Vector3 FindPlayer()
    {
        Vector3 heading = player.transform.position - transform.position;
        Vector3 unitVelocity = heading / heading.magnitude;

        if (rb2D.velocity != Vector2.zero)
        {

            float directionX = heading.x;
            float directionY = heading.y;

            float angle = Mathf.Atan2(directionY, directionX);
            float ratio = ( angle / (Mathf.PI * 2));

            if (ratio < 0)
            {
                ratio += 1;
            }
            
            //Debug.Log("Ratio set to: " + ratio);
            animator.SetFloat("rotation",ratio);
        }
        return unitVelocity;

    }

    private float FindPlayerDistance()
    {
        float dist = Vector3.Distance(player.transform.position, transform.position);

        return dist;
    }
    public void MoveTowardPlayer(Vector3 unitVelocity)
    {
        rb2D.velocity = unitVelocity * speed;
       
    }
}

