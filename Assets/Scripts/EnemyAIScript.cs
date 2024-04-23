using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAIScript : MonoBehaviour
{
    public enum State
    {
        Idle,
        Patrol,
        DetectPlayer,
        Chasing,
        AggroIdle,
    }
 
    public NavMeshAgent  agent;
    public State enemyAIState;

    public bool playerDetected;
    public bool aggro;
    public float moveSpeed;
    public float maxSpeed;
    public float chaseSpeed;
    public float speed;
    public float detectedPlayerTime;
    public float aggroTime;
    private Rigidbody2D myRb;
    private Transform player;
    public Animator anim;
    public float currentVelocity;
    void Start()
    {
        enemyAIState = State.Idle;
        myRb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
      
    }

    void Update()
    {
        if (myRb.velocity.magnitude < maxSpeed)
        {
            switch (enemyAIState)
            {
                case State.Idle:
                    speed = 0;
                    break;
                case State.Patrol:
                    speed = moveSpeed;
                    break;
                case State.DetectPlayer:
                    speed = 0;
                    break;
                case State.Chasing:
                    speed = chaseSpeed;
                    break;
                case State.AggroIdle:
                    break;
            }

            if (playerDetected)
            {
                Vector2 direction = (player.position - transform.position).normalized;
                myRb.AddForce(direction * speed, ForceMode2D.Force);

                // Flip the character's animation
                if (direction.x < 0)
                {
                    anim.transform.localScale = new Vector3(1, 1, 1);
                }
                else if (direction.x > 0)
                {
                    anim.transform.localScale = new Vector3(-1, 1, 1);
                }
            }
        }
     
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !aggro)
        {
            playerDetected = true;
            StopCoroutine("DetectTimer");
            StartCoroutine("DetectTimer");
        }
        if (other.CompareTag("Player") && aggro)
        {
            playerDetected = true;
            enemyAIState = State.Chasing;
        }
    }

    IEnumerator DetectTimer()
    {
        enemyAIState = State.DetectPlayer;
        yield return new WaitForSeconds(detectedPlayerTime);
        if (playerDetected)
        {
            aggro = true;
            enemyAIState = State.Chasing;
        }
        if (!playerDetected)
        {
            aggro = false;
            enemyAIState = State.Idle;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerDetected = false;
            if (aggro)
            {
                StartCoroutine("AggroTimer");
            }
        }
    }

    IEnumerator AggroTimer()
    {
        enemyAIState = State.AggroIdle;
        yield return new WaitForSeconds(aggroTime);
        if (playerDetected)
        {
            aggro = true;
            enemyAIState = State.Chasing;
        }
        if (!playerDetected)
        {
            aggro = false;
            enemyAIState = State.Idle;
        }
    }
}