using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FG_PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireballs;

    private Animator anim;
    private fg_playerMovement playerMovement;
    private float cooldownTimer = Mathf.Infinity;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<fg_playerMovement>();
    }

    private void Update()
    {
      
        if ((Input.GetMouseButtonDown(0) || Input.GetKey(KeyCode.LeftShift)) && cooldownTimer > attackCooldown && playerMovement.canAttack())
            Attack();

        cooldownTimer += Time.deltaTime;
    }
    private void Attack()
    {
        anim.SetTrigger("Attack");
        cooldownTimer = 0;

        int fireballIndex = FindFireball();
        if (fireballIndex == -1) return; 
        fireballs[fireballIndex].transform.position = firePoint.position;
        float direction = playerMovement.FacingDirection;
        // debug log the direction float
        Debug.Log($"Direction float from PlayerAttack is {direction}");
        fireballs[fireballIndex].GetComponent<FG_Projectile>().SetDirection(direction);
    }
    private int FindFireball()
    {
        for (int i = 0; i < fireballs.Length; i++)
        {
            if (!fireballs[i].activeInHierarchy)
                return i;
        }

        return -1;
    }
}