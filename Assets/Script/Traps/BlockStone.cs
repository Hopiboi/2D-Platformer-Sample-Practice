using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockStone : PlayerLife
{
    [Header("Properties of spike")]
    [SerializeField] private float speed;
    [SerializeField] private float range;

    [Header("Activation Time")]
    [SerializeField] private float checkDelay;
    [SerializeField] private float checkTimer;
    [SerializeField] private LayerMask playerLayer;

    private Vector3 destination;
    private Vector3[] directions = new Vector3[4];

    private bool attacking; // when to attack

    private void OnEnable()
    {
        Stop();
    }

    private void Update()
    {
        //will move when attacking and no movement when not attacking
        if (attacking)
            //translate = moving based on direction and distance
            transform.Translate(destination * Time.deltaTime * speed);
        else
        {
            checkTimer += Time.deltaTime;
            if (checkTimer > checkDelay)
                PlayerCheck();
        }
    }

    //checking if the block see's the player
    private void PlayerCheck()
    {
        CalculateDirections();

        for (int i = 0; i < directions.Length; i++)
        {
            Debug.DrawRay(transform.position, directions[i], Color.red);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, directions[i], range, playerLayer);

            if (hit.collider != null && !attacking)
            {
                attacking = true;
                destination = directions[i];
                checkTimer = 0;
            }
        }
    }

    private void Stop()
    {
        destination = transform.position;
        attacking = false;
    }

    private void CalculateDirections()
    {
        directions[0] = transform.right * range; 
        directions[1] = -transform.right * range; // left
        directions[2] = transform.up * range;
        directions[3] = -transform.up * range; //down
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.GetComponent<PlayerLife>().Death();
        Stop();
    }
}
