using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTrap : MonoBehaviour
{
    //Bug arrows
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] arrows; // for arrows or fire
    private float cooldownTimer; // delay


    private void Update()
    {

        cooldownTimer += Time.deltaTime;

        //attacks
        if (cooldownTimer >= attackCooldown)
            Attack(); 
        
    }

    //shooting projectiles
    private void Attack()
    {
        cooldownTimer = 0; // resetting 


        arrows[FindArrow()].transform.position = firePoint.position;
        arrows[FindArrow()].GetComponent<EnemyProjectile>().ActivateProjectile();

    }

    private int FindArrow()
    {
        for (int i = 0; i < arrows.Length; i++)
        {
            if (!arrows[i].activeInHierarchy)
                return i;
        }

        return 0;

    }


}
