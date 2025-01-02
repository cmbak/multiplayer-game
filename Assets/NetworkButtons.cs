using Unity.Netcode;
using UnityEngine;

public class NetworkButtons : MonoBehaviour
{
    NetworkManager ntwkManager;

    private void Awake()
    {
        ntwkManager = GetComponent<NetworkManager>();
    }

    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10, 10, 300, 300));
        if (!NetworkManager.Singleton.IsClient && !NetworkManager.Singleton.IsServer)
        {
            if (GUILayout.Button("Host")) NetworkManager.Singleton.StartHost();
            if (GUILayout.Button("Server")) NetworkManager.Singleton.StartServer();
            if (GUILayout.Button("Client")) NetworkManager.Singleton.StartClient();
        }

        GUILayout.EndArea();
    }

     void SubmitNewPosition()
    {
        if (GUILayout.Button(ntwkManager.IsServer ? "Move" : "Request Position Change"))
        {
            var playerObject = ntwkManager.SpawnManager.GetLocalPlayerObject();
            var player = playerObject.GetComponent<PlayerMovement>();
            player.Move();
        }
    }
}