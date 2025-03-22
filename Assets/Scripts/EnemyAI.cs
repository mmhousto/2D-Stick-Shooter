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

    protected SpriteRenderer spriteRenderer;
    protected Color originalColor;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
            originalColor = spriteRenderer.color;

        if (GameObject.FindWithTag("Player") != null)
            player = GameObject.FindWithTag("Player").transform;
        canFollow = false;
    }

    private void OnDisable()
    {
        if (spriteRenderer != null)
            spriteRenderer.color = originalColor;

        if (spawner != null)
            spawner.ResetSpawn(indexSpawnedAt);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canFollow = true;
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

    public void Knockback()
    {
        StartCoroutine(KnockbackCoroutine());
    }

    protected IEnumerator KnockbackCoroutine()
    {
        Vector3 velocity = agent.velocity;
        agent.isStopped = true;
        if (spriteRenderer != null)
            spriteRenderer.color = new Color(128, 0, 0);

        transform.Translate(-velocity.normalized * .1f);

        yield return new WaitForSeconds(0.25f);

        if (spriteRenderer != null)
            spriteRenderer.color = originalColor;

        agent.isStopped = false;
    }

}