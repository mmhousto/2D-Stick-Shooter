using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public Rigidbody2D rb;
    public AudioClip hitSound, zombieHitSound;
    private AudioSource hitSource;
    private BulletPool pool;
    private ParticleSystem bulletHitEffect;

    // Start is called before the first frame update
    void Start()
    {
        bulletHitEffect = GameObject.FindWithTag("BulletHitEffect").GetComponent<ParticleSystem>();
        pool = BulletPool.instance;
        rb = GetComponent<Rigidbody2D>();
        hitSource = bulletHitEffect.GetComponent<AudioSource>();
        //Destroy(gameObject, 2.5f);
        Invoke(nameof(DisableObject), 2.5f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision != null)
            PlayHitEffect(collision.GetContact(0).point);

        if(collision.gameObject.TryGetComponent(out EnemyAI enemy))
        {
            enemy.Knockback();
        }

        if(collision.gameObject.TryGetComponent(out Health health))
        {
            health.TakeDamage(25);
        }

        if (collision.transform.CompareTag("Enemy"))
        {
            hitSource.PlayOneShot(zombieHitSound);
        }
        else
        {
            hitSource.PlayOneShot(hitSound);
        }

        //Destroy(gameObject);
        DisableObject();
    }

    private void DisableObject()
    {
        pool.bulletPool.Release(this);
        //gameObject.SetActive(false);
    }

    private void PlayHitEffect(Vector2 hitSpot)
    {
        bulletHitEffect.Stop();
        bulletHitEffect.transform.position = hitSpot;
        bulletHitEffect.Play();
    }
}
