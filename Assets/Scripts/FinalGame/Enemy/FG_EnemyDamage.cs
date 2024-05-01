using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FG_EnemyDamage : MonoBehaviour
{
    [SerializeField] protected float damage;

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<FG_Health>().TakeDamage(damage);
        }
    }
}