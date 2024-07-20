using Cinemachine;
using Mirror;
using UnityEngine;

public class NetworkingManager : NetworkManager
{
    [SerializeField] private GameObject[] class_Prefabs;
    [SerializeField] private int class_Key;

    private void Start()
    {
        ClassSelect();
    }

    private void ClassSelect()
    {
        switch(class_Key)
        {
            case 0:
                playerPrefab = class_Prefabs[0];
                break;
            case 1:
                playerPrefab = class_Prefabs[1];
                break;
            case 2:
                playerPrefab= class_Prefabs[2];
                break;
        }
    }
    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        base.OnServerAddPlayer(conn);
        Debug.Log("Player added: " + conn);

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
}
