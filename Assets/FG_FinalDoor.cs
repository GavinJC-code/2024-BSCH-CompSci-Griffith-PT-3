using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FG_FinalDoor : MonoBehaviour
{
   
    // reference the FG_GameManager
    private FG_GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<FG_GameManager>();
    }
    // collistion detection with boxcollider2D
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            // Add score to the player
            gameManager.ActivateEndScreen();
            // disable the player
            col.gameObject.SetActive(false);
           
        }
    }
}
