using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class ConnectUI : MonoBehaviour
{
    

    // Network! https://www.youtube.com/watch?v=2OLUdPkkQPI
    public void ConnectHost()
    {
        NetworkManager.Singleton.StartHost();
    }

    public void ConnectClient()
    {
        NetworkManager.Singleton.StartClient();
    }
}
