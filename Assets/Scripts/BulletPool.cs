using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BulletPool : MonoBehaviour
{
    public static BulletPool instance;

    public Bullet bulletPrefab;
    public ObjectPool<Bullet> bulletPool;
    private int spawnAmount = 20;

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        bulletPool = new ObjectPool<Bullet> (CreateBullet, OnGet, OnRelease, OnEnd, false, spawnAmount, 40);
    }

    private Bullet CreateBullet()
    {

        return Instantiate(bulletPrefab);
    }

    private void OnGet(Bullet bullet)
    {
        bullet.gameObject.SetActive(true);
    }

    private void OnRelease(Bullet bullet)
    {
        bullet.rb.linearVelocity = Vector3.zero;
        bullet.gameObject.SetActive(false);
    }

    private void OnEnd(Bullet bullet)
    {
        Destroy(bullet);
    }
}
