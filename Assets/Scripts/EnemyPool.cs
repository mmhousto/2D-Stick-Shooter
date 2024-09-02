using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyPool : MonoBehaviour
{
    public static EnemyPool instance;

    public EnemyAI enemyPrefab;
    public ObjectPool<EnemyAI> enemyPool;

    public int spawnAmount = 20;
    public int spawnMax = 100;

    private void Awake()
    {
        if (instance != null && instance != this)
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
        enemyPool = new ObjectPool<EnemyAI>(CreateObject, OnGet, OnRelease, OnEnd, false, spawnAmount, spawnMax);
    }

    private EnemyAI CreateObject()
    {

        return Instantiate(enemyPrefab);
    }

    private void OnGet(EnemyAI enemy)
    {
        enemy.gameObject.SetActive(true);
    }

    private void OnRelease(EnemyAI enemy)
    {
        enemy.gameObject.SetActive(false);
    }

    private void OnEnd(EnemyAI enemy)
    {
        Destroy(enemy);
    }
}
