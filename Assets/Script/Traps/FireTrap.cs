using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTrap : MonoBehaviour
{
    //[SerializeField] private bool death;

    [Header("Firetrap Timers")]
    [SerializeField] private float activationDelayTime; //how long time to activate is
    [SerializeField] private float activationTime;  //how long is activation time

    private Animator anim;
    private SpriteRenderer spr;

    private bool triggered; // trigger to fire or get triggered
    private bool active; // active time for fire/trap

    private void Update()
    {


    }

    private void Start()
    {
        anim = GetComponent<Animator>();
        spr = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") 
        {
            if(!triggered)
            {
                StartCoroutine(ActivateFireTrap());
            }
            
            if (active)
            {
                collision.GetComponent<PlayerLife>().Death();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        collision.GetComponent<PlayerLife>().Death();

    }

    private IEnumerator ActivateFireTrap()
    {
        //turning the sprite to red to notfy the player and triggering the trap
        triggered = true;
        spr.color = Color.red; //notifying the player

        // delay, activating the trap, turning on animation, color back to normal
        yield return new WaitForSeconds(activationDelayTime);
        spr.color = Color.white; //returning color
        active = true;
        anim.SetBool("Activate", true);

        // deactivating the trap
        yield return new WaitForSeconds(activationTime); 
        active = false;
        triggered = false;
        anim.SetBool("Activate", false);
    }

}
