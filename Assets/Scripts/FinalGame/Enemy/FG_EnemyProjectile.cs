using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FG_EnemyProjectile : FG_EnemyDamage // will damage the player when it touches player
{


    [SerializeField] private float speed;
    [SerializeField] private float resetTime;
    private float lifetime;
    // exploding fireballs
    private Animator anim;
    private bool hit;
    private BoxCollider2D collider;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        collider = GetComponent<BoxCollider2D>();
    }

    public void ActivateProjectile()
    {
        hit = false;
        lifetime = 0;
        gameObject.SetActive(true);
        collider.enabled = true;
    }

    private void Update()
    {
        if (hit) return;
        float movementSpeed = speed * Time.deltaTime;
        transform.Translate(movementSpeed, 0, 0);

        lifetime += Time.deltaTime;
        if(lifetime >= resetTime)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        

        hit = true;
        base.OnTriggerEnter2D(collision); // execute parent script first
        collider.enabled = false;
        
        if(anim != null){
            anim.SetTrigger("explode"); // expode fireballs
        }
        else gameObject.SetActive(false); // deactivate arrows when hit

    }
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
