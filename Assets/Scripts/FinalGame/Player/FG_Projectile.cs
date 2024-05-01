using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FG_Projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    private float direction = 1;
    private bool hit;
    private float lifetime;
    private Animator anim;
    private BoxCollider2D boxCollider;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        speed = 4;
    }

    private void Update()
    {
        if (hit) return;

        // Calculate movement based on direction and speed
        float movement = speed * (Time.deltaTime + 0.1f) * direction;
        transform.Translate(movement, 0, 0);

        // Update lifetime and check if the projectile should be deactivated
        lifetime += Time.deltaTime;
        if (lifetime > 5)
        {
            Debug.Log("Deactivating fireball due to lifetime expiry.");
            Deactivate();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;
        boxCollider.enabled = false;
        anim.SetTrigger("explode");

        if (collision.tag == "Enemy")
        {
            collision.GetComponent<FG_Health>().TakeDamage(1);
        }
    }

    public void SetDirection(float _direction)
    {
        direction = Mathf.Sign(_direction); // Ensure direction is strictly -1 or 1
        gameObject.SetActive(true);
        hit = false;
        boxCollider.enabled = true;

        float localScaleX = transform.localScale.x;
        if (Mathf.Sign(localScaleX) != _direction)
            localScaleX = -localScaleX;

        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }

    private void Deactivate()
    {
        Debug.Log("Fireball is now inactive.");
        gameObject.SetActive(false);
        lifetime = 0;
        direction = 1;
    }
}