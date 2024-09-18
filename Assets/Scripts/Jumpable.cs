using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumpable : MonoBehaviour
{
    public Collider2D objectCollider;

    // Start is called before the first frame update
    void Start()
    {
        objectCollider = GetComponent<BoxCollider2D>();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player") && collision.transform.GetComponent<GetPlayerInput>().isJumping && objectCollider.isTrigger == false)
        {
            Debug.Log("Colliding & Jumping");
            objectCollider.isTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Jumped Over");
            objectCollider.isTrigger = false;
        }
    }
}
