using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FG_KillZone : MonoBehaviour
{
    // Reference to the Panel that needs to be enabled
    

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            // Deal damage to the player
            col.GetComponent<FG_Health>().TakeDamage(1);

            // Log that the player is in the killzone
            Debug.Log("In the killzone");

        }
    }
}
