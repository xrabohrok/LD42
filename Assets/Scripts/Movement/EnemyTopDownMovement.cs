using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTopDownMovement : TopDownMovement {

    protected string currentState;  // This is ripped from animator. DO NOT SET IT OTHERWISE
    public GameObject player;

    private bool enemyCanMove = true;

    public float interactionRange = 1f;

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
        base.Start();
    }

    // Update is called once per frame
    protected virtual void Update()
    {

        if (enemyCanMove)
        {
            //Enemy should move
            curTime += Time.deltaTime;
            float playerDist = FindPlayerDistance();
            Debug.Log("Player Distance: " + playerDist);
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

    public Vector3 FindPlayer()
    {
        Vector3 heading = player.transform.position - transform.position;
        Vector3 unitVelocity = heading / heading.magnitude;

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
        if (rb2D.velocity != Vector2.zero)
        {
            animator.SetFloat("Speed", 1);
        }
    }
}

