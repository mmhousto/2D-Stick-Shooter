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
        if(enemyPool.enemyPool.CountActive < spawns.Length) 
        {
            int index = Random.Range(0, spawns.Length);
            if (spawnedAtIndexes.Contains(index) == false)
            {
                var clone = enemyPool.enemyPool.Get();
                clone.agent.enabled = false;
                clone.transform.position = spawns[index].transform.position;
                clone.agent.enabled = true;
                spawnedAtIndexes.Add(index);
                StartCoroutine(ResetSpawn(index));

            }
            
        }
        
    }

    IEnumerator ResetSpawn(int element)
    {
        yield return new WaitForSeconds(5f);
        spawnedAtIndexes.Remove(element);
    }
}
