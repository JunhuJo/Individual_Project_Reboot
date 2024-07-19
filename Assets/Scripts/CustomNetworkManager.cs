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

        // �÷��̾ ������ �� ���� �÷��̾�� ī�޶� ���Դϴ�.
        GameObject player = conn.identity.gameObject;

        // �÷��̾� ������Ʈ�� ���� �÷��̾����� Ȯ��
        if (conn.identity.isLocalPlayer)
        {
            // ī�޶� �÷��̾� ������Ʈ�� ���Դϴ�.
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
            // �÷��̾ ī�޶� ���Դϴ�.
            virtual_Camera.Follow = player.transform;
        }
        else
        {
            Debug.LogError("Player camera prefab is not assigned in the NetworkManager.");
        }
    }
}
