using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessEnemy : EnemyAI
{
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
            originalColor = spriteRenderer.color;

        if (GameObject.FindWithTag("Player") != null)
            player = GameObject.FindWithTag("Player").transform;
        canFollow = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null && GameObject.FindWithTag("Player"))
            player = GameObject.FindWithTag("Player").transform;

        if (player != null && canFollow == true)
            agent.SetDestination(player.position);
    }
}
