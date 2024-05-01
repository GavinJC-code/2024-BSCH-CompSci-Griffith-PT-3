using UnityEngine;

public class FG_HealthCollectible : MonoBehaviour
{
    public FG_GameManager gameManager;
    [SerializeField] private float healthValue;
    [SerializeField] private float respawnTime = 20f; // Set the respawn time to 20 seconds

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<FG_GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //gameManager.AddScore(10);
            collision.GetComponent<FG_Health>().AddHealth(healthValue); // Add health to the player
            gameObject.SetActive(false); // Disable the gameObject making it disappear
            Invoke(nameof(Reactivate), respawnTime); // Schedule reactivation
        }
    }

    /// <summary>
    /// Reactivates the health collectible.
    /// </summary>
    private void Reactivate()
    {
        gameObject.SetActive(true); // Re-enable the gameObject making it reappear
    }
}