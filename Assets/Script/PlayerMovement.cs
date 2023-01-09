using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public Rigidbody2D rg2D;
    
    float jumpPower = 5.4f;


    // Start is called before the first frame update
    void Start()
    {
     
        rg2D = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        float dirX = Input.GetAxis("Horizontal");
        rg2D.velocity = new Vector2 (dirX * 7f, rg2D.velocity.y);

        //jump
        if (Input.GetButtonDown("Jump"))
        {
            rg2D.velocity = new Vector2(rg2D.velocity.x, jumpPower);
        }


    }
}
