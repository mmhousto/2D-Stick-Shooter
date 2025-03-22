using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public Rigidbody2D rb;
    private BulletPool pool;

    // Start is called before the first frame update
    void Start()
    {
        pool = BulletPool.instance;
        rb = GetComponent<Rigidbody2D>();
        //Destroy(gameObject, 2.5f);
        Invoke(nameof(DisableObject), 2.5f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyAI>().Knockback();
            collision.gameObject.GetComponent<Health>().TakeDamage(25);
            
        }
        //Destroy(gameObject);
        DisableObject();
    }

    private void DisableObject()
    {
        pool.bulletPool.Release(this);
        //gameObject.SetActive(false);
    }


}
