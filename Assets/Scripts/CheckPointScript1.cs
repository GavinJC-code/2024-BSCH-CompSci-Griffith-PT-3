using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointScript1 : MonoBehaviour
{
   
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagerScript>().spawnPoint = transform;
            gameObject.GetComponent<Animator>().SetTrigger("CheckPointTriggered");
        }
    }
}

