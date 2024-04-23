using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointScript : MonoBehaviour
{
   
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<FG_GameManager>().spawnPoint = transform;
            gameObject.GetComponent<Animator>().SetTrigger("CheckPointTriggered");
        }
    }
}

