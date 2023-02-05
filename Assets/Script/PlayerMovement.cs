using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D rg2D;
    private BoxCollider2D coll;
    private Animator anim;
    private SpriteRenderer sprite;

    [SerializeField] private LayerMask grounded;

    [SerializeField] private float jumpPower = 5.4f;
    [SerializeField] private float moveSpeed = 7f;
    private float dirX = 0f;

    private enum MovementState { idle, running, jump, fall }

    void Start()
    {
        rg2D = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }


    private void Update()
    {
        Movement();
    }

    //Movement keys 
    private void Movement()
    {
        dirX = Input.GetAxis("Horizontal");
        rg2D.velocity = new Vector2(dirX * moveSpeed, rg2D.velocity.y);

        //jump
        if (Input.GetButtonDown("Jump") && IsGrounded()) 
        {
            rg2D.velocity = new Vector2(rg2D.velocity.x, jumpPower);
        }

        AnimationMovement();
    }

    //Animation code
    private void AnimationMovement()
    {
        MovementState state;

        if (dirX > 0f)
        {
            state = MovementState.running;
            sprite.flipX = false;
        }

        else if (dirX < 0f)
        {
            state = MovementState.running;
            sprite.flipX = true;
        }

        else
        {
            state = MovementState.idle;
        }

        if (rg2D.velocity.y > .1f)
        {
            state = MovementState.jump;
        }

        else if (rg2D.velocity.y < -.1f)
        {
            state = MovementState.fall;
        }

        anim.SetInteger("state", (int)state);
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, grounded);

    }

}
