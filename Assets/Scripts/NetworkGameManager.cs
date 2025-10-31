using System.Collections.Generic;
using Unity.Netcode;
using UnityEditor.PackageManager;
using UnityEngine;

public class NetworkGameManager : NetworkBehaviour
{
    private List<NetworkObject> playerObjects = new List<NetworkObject>();
    public GameObject playerPrefab;
    public Transform[] spawnPoints;
    public Transform[] enemySpawnPoints;
    private bool spawnedPlayer;
    private int spawnedPlayerCount = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {


        if (MainManager.players == MainManager.Players.Coop)
        {
            //SpawnPlayerRpc();
            NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnectedCallback;
            NetworkManager.Singleton.OnClientDisconnectCallback += OnClientDisconnectCallback;
            SpawnPlayerRpc();
        }
        else if (MainManager.players == MainManager.Players.Solo && MainManager.mode == MainManager.Mode.Endless)
        {
            GameObject player = Instantiate(playerPrefab, spawnPoints[0].position, Quaternion.identity);
            enemySpawnPoints[0].SetParent(player.transform);
            enemySpawnPoints[0].localPosition = Vector3.zero;
            enemySpawnPoints[0].gameObject.SetActive(true);
        }
    }

    private void OnClientConnectedCallback(ulong clientId)
    {
        Debug.Log("Client Connected");
        if (clientId == NetworkManager.Singleton.LocalClientId && spawnedPlayer == false && IsClient)
        {
            spawnedPlayer = true;
            SpawnPlayerRpc();
        }

    }

    private void OnClientDisconnectCallback(ulong clientId)
    {

        if (clientId == NetworkManager.Singleton.LocalClientId)//OnClientConnectionNotification?.Invoke(clientId, ConnectionStatus.Disconnected);
            spawnedPlayer = false;
    }

    [Rpc(SendTo.Server)]
    public void SpawnPlayerRpc()
    {
        //playerObjects = new NetworkObject[NetworkManager.Singleton.ConnectedClients.Count];
        GameObject player = Instantiate(playerPrefab, spawnPoints[spawnedPlayerCount].position, Quaternion.identity);
        playerObjects.Add(player.GetComponent<NetworkObject>());
        playerObjects[spawnedPlayerCount].SpawnAsPlayerObject(playerObjects[spawnedPlayerCount].OwnerClientId, true);

        if (MainManager.mode == MainManager.Mode.Endless)
        {
            enemySpawnPoints[spawnedPlayerCount].SetParent(player.transform);
            enemySpawnPoints[spawnedPlayerCount].localPosition = Vector3.zero;
            enemySpawnPoints[spawnedPlayerCount].gameObject.SetActive(true);
        }

        spawnedPlayerCount++;

    }

    // Update is called once per frame
    void Update()
    {

    }


}
