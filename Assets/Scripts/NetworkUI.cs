using UnityEngine;
using UnityEngine.UI;

public class NetworkUI : MonoBehaviour
{
    public NetworkingManager networkManager;
    public Button hostButton;
    public Button joinButton;
    public InputField addressInput;

    private void Start()
    {
        hostButton.onClick.AddListener(StartHost);
        joinButton.onClick.AddListener(StartClient);
    }

    private void StartHost()
    {
        networkManager.StartHost();
    }

    private void StartClient()
    {
        networkManager.networkAddress = addressInput.text;
        networkManager.StartClient();
    }
}
