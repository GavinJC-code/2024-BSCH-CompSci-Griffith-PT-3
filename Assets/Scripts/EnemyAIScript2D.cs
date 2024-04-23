using UnityEngine;

public class EnemyPigController : MonoBehaviour
{
    public float patrolSpeed = 2f;
    public float chaseSpeed = 5f;
    public float detectionRange = 5f;  // Assuming you may want to use this for finer control
    public Transform[] patrolPoints;
    private int currentPatrolIndex = 0;
    private bool movingRight = true;

    private Transform player;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    public float cooldownTime = 3f;
    private float cooldownTimer;
    public GameManagerScript gameManager;

    private enum State
    {
        Patrolling,
        Chasing,
        Cooldown
    }

    private State currentState = State.Patrolling;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        UpdatePatrolPoint();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagerScript>();
    }

    void Update()
    {
        switch (currentState)
        {
            case State.Patrolling:
                Patrol();
                break;
            case State.Chasing:
                Chase();
                break;
            case State.Cooldown:
                Cooldown();
                break;
        }
    }

private void Patrol()
{
    // Determine the direction to the current patrol point
    if (transform.position.x < patrolPoints[currentPatrolIndex].position.x)
    {
        movingRight = true;
        //spriteRenderer.flipX = false; // Assuming sprite faces right by default
    }
    else if (transform.position.x > patrolPoints[currentPatrolIndex].position.x)
    {
        movingRight = false;
        //spriteRenderer.flipX = true;  // Flip sprite if moving left
    }

    // Move towards the current patrol point
    float step = patrolSpeed * Time.deltaTime;
    transform.position = Vector2.MoveTowards(transform.position, patrolPoints[currentPatrolIndex].position, step);

    // Check if the patrol point has been reached
    if (Vector2.Distance(transform.position, patrolPoints[currentPatrolIndex].position) < 0.1f)
    {
        // Move to the next patrol point
        currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
    }
}

    private void Chase()
    {
        float step = chaseSpeed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, player.position, step);

        if (movingRight && player.position.x < transform.position.x || !movingRight && player.position.x > transform.position.x)
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
            movingRight = !movingRight;
        }
    }

    private void Cooldown()
    {
        if (cooldownTimer < cooldownTime)
        {
            cooldownTimer += Time.deltaTime;
        }
        else
        {
            cooldownTimer = 0;
            currentState = State.Patrolling;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            currentState = State.Chasing;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            currentState = State.Cooldown;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameManager.ReduceHealth();
            // Implement the logic to hurt the player here, e.g., reducing health
        }
    }

    private void UpdatePatrolPoint()
    {
        if (patrolPoints.Length < 2)
        {
            Debug.LogError("Insufficient patrol points assigned");
        }
    }
}
