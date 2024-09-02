using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] spawns;
    private EnemyPool enemyPool;
    private List<int> spawnedAtIndexes = new List<int>();

    // Start is called before the first frame update
    void Start()
    {
        spawns = GameObject.FindGameObjectsWithTag("SpawnPoint");
        enemyPool = EnemyPool.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if(enemyPool.enemyPool.CountActive < enemyPool.spawnAmount) 
        {
            int index = Random.Range(0, spawns.Length);
            if (spawnedAtIndexes.Contains(index) == false)
            {
                var clone = enemyPool.enemyPool.Get();
                clone.agent.enabled = false;
                clone.transform.position = spawns[index].transform.position;
                clone.agent.enabled = true;
                clone.indexSpawnedAt = index;
                clone.SetSpawner(this);
                spawnedAtIndexes.Add(index);
            }
            
        }
        
    }

    public void ResetSpawn(int element)
    {
        StartCoroutine(ResetSpawnAfterTime(element));
    }

    IEnumerator ResetSpawnAfterTime(int element)
    {
        yield return new WaitForSeconds(10f);
        spawnedAtIndexes.Remove(element);
    }


}
