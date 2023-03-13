using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Components
    private Rigidbody2D rg2D;
    private BoxCollider2D coll;
    private Animator anim;
    private SpriteRenderer sprite;

    [SerializeField] private LayerMask grounded;

    //Movement 
    [SerializeField] private float jumpPower = 5.4f;
    [SerializeField] private float doubleJumpPower = 5.4f;
    [SerializeField] private float jumpRelease = 2f;
    [SerializeField] private float moveSpeed = 7f;
    private float dirX = 0f;
    private bool doubleJump;


    //animation states
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


        //Double Jump turning to false in the air


        //Jump and Double Jump
        if (Input.GetButtonDown("Jump")) 
        {
            if (IsGrounded())
            {
                rg2D.velocity = new Vector2(rg2D.velocity.x, jumpPower);

                doubleJump = true;
            }

            else if (doubleJump)
            {
                rg2D.velocity = new Vector2(rg2D.velocity.x, doubleJumpPower);

                doubleJump = false;
            }
        }

        //Controlling the jump in short press or long press
        if (Input.GetButtonUp("Jump"))
        {
            rg2D.velocity = new Vector2(rg2D.velocity.x, rg2D.velocity.y / jumpRelease);
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
    

    //Checking if its Grounded
    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, grounded);

    }

}
