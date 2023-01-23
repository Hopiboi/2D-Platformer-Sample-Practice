using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D rg2D;
    private Animator anim;
    private SpriteRenderer sprite;

    [SerializeField] private float jumpPower = 5.4f;
    [SerializeField] private float moveSpeed = 7f;
    private float dirX = 0f;

    private enum MovementState { idle, running, jump, fall }

    void Start()
    {
        rg2D = GetComponent<Rigidbody2D>();
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
        if (Input.GetButtonDown("Jump"))
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
}
