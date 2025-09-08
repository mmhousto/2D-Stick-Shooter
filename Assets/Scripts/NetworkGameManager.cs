using Cinemachine;
using Unity.Netcode;
using UnityEngine;

public class NetworkGameManager : NetworkBehaviour
{
    private NetworkObject[] playerObjects;
    public GameObject playerPrefab;
    public Transform[] spawnPoints;
    public Transform[] enemySpawnPoints;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (IsOwner && MainManager.players == MainManager.Players.Coop)
        {
            SpawnPlayersRpc();
        }
        else
        {
            GameObject player = Instantiate(playerPrefab, spawnPoints[0].position, Quaternion.identity);
            enemySpawnPoints[1].SetParent(player.transform);
            enemySpawnPoints[1].localPosition = Vector3.zero;
            enemySpawnPoints[1].gameObject.SetActive(true);
        }
    }

    [Rpc(SendTo.Server)]
    public void SpawnPlayersRpc()
    {
        int i = 0;
        playerObjects = new NetworkObject[NetworkManager.Singleton.ConnectedClients.Count];
        foreach (NetworkClient client in NetworkManager.Singleton.ConnectedClientsList)
        {
            GameObject player = NetworkManager.Instantiate(playerPrefab, spawnPoints[i].position, Quaternion.identity);
            playerObjects[i] = player.GetComponent<NetworkObject>();
            playerObjects[i].SpawnAsPlayerObject(client.ClientId, true);

            if(MainManager.mode == MainManager.Mode.Endless)
            {
                enemySpawnPoints[i].SetParent(player.transform);
                enemySpawnPoints[i].localPosition = Vector3.zero;
                enemySpawnPoints[i].gameObject.SetActive(true);
            }

            i++;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }


}
