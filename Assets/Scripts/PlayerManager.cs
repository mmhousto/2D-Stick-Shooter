using Cinemachine;
using Unity.Netcode;
using UnityEngine;

public class PlayerManager : NetworkBehaviour
{

    private CinemachineVirtualCamera playerCam;

    private void Update()
    {
        if (playerCam == null) playerCam = GameObject.FindWithTag("PlayerCam").GetComponent<CinemachineVirtualCamera>();

        if (playerCam != null && playerCam.Follow != transform)
        {
            playerCam.Follow = transform;
        }
        
    }
}
