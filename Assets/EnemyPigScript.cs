using System;
using System.Collections;
using UnityEngine;

public class EnemyPigScript : MonoBehaviour
{
    public enum State
    {
        Patrol,
        DetectPlayer,
        Chasing,
        AggroIdle,
    }

    public GameManagerScript gameManager;
    public State enemyAIState = State.Patrol;
    public float moveSpeed = 2.0f;
    public float chaseSpeed = 3.5f;
    public float detectedPlayerTime = 1.5f;
    public float aggroTime = 3.0f;
    public Transform playerTransform;
    public Animator animator;

    private Rigidbody2D _myRb;
    private bool movingRight = true; // Ensure this matches the initial facing direction of your sprite
    private Vector2 startPosition;
    public float patrolDistance = 5.0f;
    private Coroutine detectCoroutine;
    private Coroutine aggroCoroutine;

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagerScript>();
        _myRb = GetComponent<Rigidbody2D>();
        startPosition = _myRb.position;
        if (playerTransform == null)
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        if (animator == null)
            animator = GetComponent<Animator>();
    }

    void Update()
    {
        switch (enemyAIState)
        {
            case State.Patrol:
                Patrol();
                ChangeAnimationState("idle");
                break;
            case State.DetectPlayer:
                ChangeAnimationState("chasing");
                break;
            case State.Chasing:
                ChasePlayer();
                ChangeAnimationState("chasing");
                break;
            case State.AggroIdle:
                _myRb.velocity = Vector2.zero;
                ChangeAnimationState("idle");
                break;
        }
    }

    private void Patrol()
    {
        float targetX = movingRight ? startPosition.x + patrolDistance : startPosition.x - patrolDistance;
        if ((movingRight && _myRb.position.x >= targetX) || (!movingRight && _myRb.position.x <= targetX))
            movingRight = !movingRight;

        _myRb.velocity = new Vector2(movingRight ? moveSpeed : -moveSpeed, 0);
    }

    private void ChasePlayer()
    {
        Vector2 direction = new Vector2(playerTransform.position.x - transform.position.x, 0).normalized;
        _myRb.velocity = direction * chaseSpeed;
        FlipCharacterToFacePlayer();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (detectCoroutine != null)
                StopCoroutine(detectCoroutine);
            detectCoroutine = StartCoroutine(DetectTimer());
        }
    }

    private void FlipCharacterToFacePlayer()
    {
        bool shouldFaceRight = playerTransform.position.x > transform.position.x;
        if (shouldFaceRight != movingRight)
        {
            movingRight = shouldFaceRight;
            Vector3 theScale = transform.localScale;
            theScale.x = Mathf.Abs(theScale.x) * (shouldFaceRight ? 1 : -1);
            transform.localScale = theScale;
        }
    }

    IEnumerator DetectTimer()
    {
        enemyAIState = State.DetectPlayer;
        yield return new WaitForSeconds(detectedPlayerTime);
        enemyAIState = State.Chasing;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (aggroCoroutine != null)
                StopCoroutine(aggroCoroutine);
            aggroCoroutine = StartCoroutine(AggroTimer());
        }
    }

    IEnumerator AggroTimer()
    {
        yield return new WaitForSeconds(aggroTime);
        if (enemyAIState != State.Chasing)
            enemyAIState = State.AggroIdle;

        yield return new WaitForSeconds(aggroTime * 2);
        enemyAIState = State.Patrol;
    }

    private void ChangeAnimationState(string newState)
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName(newState)) return;
        animator.SetTrigger(newState);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player touched the pig");
            gameManager.ReduceHealth();
        }
    }
}