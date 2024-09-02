using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform player;
    public NavMeshAgent agent;
    public int indexSpawnedAt = -1;
    protected EnemySpawner spawner;
    protected bool canFollow;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        if(GameObject.FindWithTag("Player") != null)
            player = GameObject.FindWithTag("Player").transform;
        canFollow = false;
    }

    private void OnDisable()
    {
        if(spawner != null)
            spawner.ResetSpawn(indexSpawnedAt);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canFollow = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canFollow = false;
        }
    }

    private void Update()
    {
        if (player == null && GameObject.FindWithTag("Player"))
            player = GameObject.FindWithTag("Player").transform;

        if (player != null && canFollow == true)
            agent.SetDestination(player.position);
    }

    public void SetSpawner(EnemySpawner enemySpawner)
    {
        spawner = enemySpawner;
    }
 
}