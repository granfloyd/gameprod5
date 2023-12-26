using Unity.Netcode;
using UnityEngine.UI;

public class testingNetcode : NetworkBehaviour
{
    public Button hostButton;
    public Button serverButton;
    public Button clientButton;

    public NetworkObject playerPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
        hostButton.onClick.AddListener(Host);
        serverButton.onClick.AddListener(Server);
        clientButton.onClick.AddListener(Client);
        
    }
    private void SpawnPlayerForClient(ulong clientId)
    {
        // Instantiate the player object
        var playerObject = Instantiate(playerPrefab);

        // Spawn the player object with ownership for the client
        playerObject.SpawnWithOwnership(clientId);
    }
    void Host()
    {
        NetworkManager.Singleton.StartHost();
    }

    void Server()
    {
        NetworkManager.Singleton.StartServer();
    }

    void Client()
    {
        NetworkManager.Singleton.StartClient();
        //NetworkManager.Singleton.OnClientConnectedCallback += SpawnPlayerForClient;
    }
}
