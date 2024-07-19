using Cinemachine;
using Mirror;
using UnityEngine;

public class CustomNetworkManager : NetworkManager
{
    [SerializeField] private CinemachineVirtualCamera virtual_Camera;

    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        base.OnServerAddPlayer(conn);
        Debug.Log("Player added: " + conn);

        // 플레이어가 생성된 후 로컬 플레이어에만 카메라를 붙입니다.
        GameObject player = conn.identity.gameObject;

        // 플레이어 오브젝트가 로컬 플레이어인지 확인
        if (conn.identity.isLocalPlayer)
        {
            // 카메라를 플레이어 오브젝트에 붙입니다.
            AttachCameraToPlayer(player);
        }
    }

    public override void OnServerDisconnect(NetworkConnectionToClient conn)
    {
        base.OnServerDisconnect(conn);
        Debug.Log("Player disconnected: " + conn);
    }

    public override void OnClientConnect()
    {
        base.OnClientConnect();
        Debug.Log("Client connected");
    }

    public override void OnClientDisconnect()
    {
        base.OnClientDisconnect();
        Debug.Log("Client disconnected");
    }

    private void AttachCameraToPlayer(GameObject player)
    {
        if (virtual_Camera != null)
        {
            // 플레이어에 카메라를 붙입니다.
            virtual_Camera.Follow = player.transform;
        }
        else
        {
            Debug.LogError("Player camera prefab is not assigned in the NetworkManager.");
        }
    }
}
