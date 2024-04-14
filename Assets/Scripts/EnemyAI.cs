using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent agent;
    /*public LayerMask obstacleMask;
    public float moveSpeed = 5f;
    public float obstacleAvoidanceForce = 10f;

    private Rigidbody2D rb;*/

    private void Start()
    {
        //rb = GetComponent<Rigidbody2D>();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    private void Update()
    {
        agent.SetDestination(player.position);
    }
    /*private void FixedUpdate()
    {
        if (player == null)
            return;

        Vector2 direction = (player.position - transform.position).normalized;
        Vector2 movement = direction * moveSpeed;

        // Apply movement force
        rb.velocity = movement;

        // Check for obstacles
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 1, obstacleMask);
        if (hit.collider != null)
        {
            // If there's an obstacle, apply avoidance force
            Vector2 obstacleAvoidanceDirection = Vector2.Perpendicular(direction);
            rb.AddForce(obstacleAvoidanceDirection * obstacleAvoidanceForce);
        }
    }*/
}