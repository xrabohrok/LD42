using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownMovement : MonoBehaviour {

    public Rigidbody2D rb2D;               //The Rigidbody2D component attached to this object.
    public BoxCollider2D bc2D;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public LayerMask blockingLayer;
    public float speed = 1;             //Floating point variable to store the player's movement speed.

    public bool busyHandlingInput = false;

    public enum Facing { North, South, East, West };
    public Facing facing;


    // Use this for initialization
    protected virtual void Start()
    {
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        bc2D = gameObject.GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    public virtual void FixedUpdate()
    {
    }

    public virtual void SetTriggers()
    {
        animator.SetFloat("YSpeed", rb2D.velocity.y);
        animator.SetFloat("XSpeed", rb2D.velocity.x);

        if (rb2D.velocity.x < 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (rb2D.velocity.x > 0)
        {
            spriteRenderer.flipX = true;
        }
    }

}
