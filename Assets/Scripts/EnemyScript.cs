using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEditor.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class EnemyScript : MonoBehaviour
{
    
    [FormerlySerializedAs("animBOT")] public Animator anim;
    public NavMeshAgent  agent;

    public float currentVelocity;
    public Transform player;
    public Transform[] patrolPoints;
    public bool destinationReached;
    public float destinationThreshold;
    public float aggroTimer;
    

    private bool aggro;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        aggro = false;
        destinationReached = true;
    }

    // Update is called once per frame
    void Update()
    {
        // move towards the player ysing navmesh
        currentVelocity = agent.velocity.magnitude;
        anim.SetFloat("velocity", currentVelocity);

        if (aggro)
        {
            agent.destination = player.position;
        }

        if (!aggro && destinationReached)
        {
            destinationReached = false;
            agent.destination = patrolPoints[Random.Range(0, patrolPoints.Length)].position;
        }
        if(Vector3.Distance(transform.position, agent.destination) < destinationThreshold)
        {
            destinationReached = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            aggro = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            agent.destination = player.position;
            StopCoroutine("AggroTimer");
            StartCoroutine("AggroTimer");
            
        }
    }
    IEnumerator AggroTimer()
    {
        yield return new WaitForSeconds(aggroTimer);
        aggro = false;
        destinationReached = true;
    }
}
