using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : PlayerLife// inheritance
{

    [SerializeField] private float speed;
    [SerializeField] private float resetTime;
    private float lifeTime;

    //Death
    private void Update()
    {
        float movementSpeed = speed * Time.deltaTime;
        transform.Translate(movementSpeed, 0, 0);

        lifeTime += Time.deltaTime;
        if (lifeTime > resetTime)
            gameObject.SetActive(false);


    }

    public void ActivateProjectile()
    {
        lifeTime -= 0;//reset
        gameObject.SetActive(true);



    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        gameObject.SetActive(false);
    }
}
