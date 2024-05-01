using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeScript : MonoBehaviour
{
    public float scoreValue;
    public GameManagerScript gameManager;
    
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.gameObject.CompareTag("Player")) return;
        
 
        if (col.transform.position.x < transform.position.x || col.transform.position.x > transform.position.x)
        {
            col.GetComponent<Rigidbody2D>().AddForce(new Vector2(-5, 5), ForceMode2D.Impulse);
        }
        

        if (col.transform.position.y > transform.position.y)
        {
            col.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 5), ForceMode2D.Impulse);
        }
        
        // check for a delay of 2 seconds before the player can take damage again
        gameManager.ReduceHealth();

        if (gameManager.GetHealth() <= 0)
        {
            col.transform.position = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagerScript>().spawnPoint.position;
            gameManager.SetHealth(10);
        }
       
        
    }
}
