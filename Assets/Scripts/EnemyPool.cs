using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyPool : NetworkBehaviour
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
        if (MainManager.players == MainManager.Players.Coop && IsServer)
        {
            EnemyAI enemy = Instantiate(enemyPrefab);
            enemy.GetComponent<NetworkObject>().Spawn(true);
            return enemy;
        }
        else
        {
            return Instantiate(enemyPrefab);
        }
            
        
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
