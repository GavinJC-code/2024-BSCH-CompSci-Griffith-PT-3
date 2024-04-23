using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FG_Health : MonoBehaviour
{
    [Header ("Health")]
    // Start is called before the first frame update
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; } // get from any script but only set from this script
    private Animator anim;
    private bool isDead;
    [Header("iFrames")]
    [SerializeField] private float iFrameDuration;
    [SerializeField] private float numberOfFlashes;
    private SpriteRenderer spriteRenderer;
    public FG_GameManager gameManager;

    private float lastDamageTime;
    public float damageCooldown = 1.0f; // Seconds

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<FG_GameManager>();
    }

    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void disableCharacter(){
        gameManager.AddScore(5);
        gameObject.SetActive(false);
    }

    public void TakeDamage(float _damage)
    {
        // Check if the GameObject is the player and if the damage cooldown has passed
        if (gameObject.CompareTag("Player") && Time.time - lastDamageTime < damageCooldown)
        {
            Debug.Log("Damage attempt ignored due to cooldown.");
            return;
        }


        Debug.Log("TakeDamage called with damage: " + _damage + ", current health: " + currentHealth);
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if (currentHealth > 0)
        {
            anim.SetTrigger("hurt");
            if (gameObject.CompareTag("Player"))
            {
                // Only teleport the player to the spawn point
                Transform spawnPoint = GameObject.FindGameObjectWithTag("GameManager").GetComponent<FG_GameManager>().spawnPoint;
                transform.position = spawnPoint.position;
                Camera.main.GetComponent<FG_CameraController>().MoveToNewRoom(spawnPoint);
            }
            StartCoroutine(InvulnerabilityFrames());
        }
        else if (!isDead) // only execute once
        {
            isDead = true;
            anim.SetTrigger("die");

            if(GetComponent<fg_playerMovement>() != null)
            {
                GetComponent<fg_playerMovement>().enabled = false;
            }

            if(GetComponent<FG_EnemyPatrol>() != null)
            {
                GetComponent<FG_EnemyPatrol>().enabled = false;
            }

            if(GetComponent<FG_MeleeEnemy>() != null)
            {
                GetComponent<FG_MeleeEnemy>().enabled = false;
            }

            if (gameObject.CompareTag("Player"))
            {
                gameManager.ActivateEndScreen();
            }
        }

        // Update last damage time
        lastDamageTime = Time.time;
    }

    public void AddHealth(float _health)
    {
        currentHealth = Mathf.Clamp(currentHealth + _health, 0, startingHealth);
    }

    private IEnumerator InvulnerabilityFrames()
    {


        Physics2D.IgnoreLayerCollision(8, 9, true); // ignore collision between player and enemy
        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRenderer.color = new Color(1, 1, 1, 0.5f);
            yield return new WaitForSeconds(iFrameDuration / (numberOfFlashes * 2));
            spriteRenderer.color = new Color(1, 1, 1, 1);
            yield return new WaitForSeconds(iFrameDuration / (numberOfFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(8, 9, false); // ignore collision between player and enemy 
    }



    // //testing damage
    // private void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.E))
    //     {
    //         // debug that the key was pressed
    //         Debug.Log("E key was pressed");
    //         TakeDamage(1);
    //     }
    // }

    
}
