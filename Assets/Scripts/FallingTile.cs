using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

enum TileState
{
    Normal,
    Yellow,
    Orange,
    Red,
}

public class FallingTile : NetworkBehaviour
{
    [SerializeField] private Material yellow;
    [SerializeField] private Material orange;
    [SerializeField] private Material red;
    private TileState state = TileState.Normal;
    private MeshRenderer meshRenderer;

    //[SerializeField] NetworkVariable<TileState> networkState =  new NetworkVariable<TileState>(TileState.Normal);

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }



    // Decrease 'health' of tile
    // Destory if players have walked on it too much
    [Rpc(SendTo.ClientsAndHost)]
    private void ChangeStateRpc()
    {
        switch (state)
        {
            case TileState.Normal:
                state = TileState.Yellow;
                break;
            case TileState.Yellow:
                state = TileState.Orange;
                break;
            case TileState.Orange:
                state = TileState.Red;
                break;
            case TileState.Red:
                NetworkManager.Destroy(gameObject);
                //Destroy(gameObject);
                break;
        }
    }

    // Change material of tile depending on state
    [Rpc(SendTo.ClientsAndHost)]
    private void ChangeColourRpc()
    {
        switch (state)
        {
            case TileState.Yellow:
                meshRenderer.material = yellow;
                break;
            case TileState.Orange:
                meshRenderer.material = orange;
                break;
            case TileState.Red:
                meshRenderer.material = red;
                break;
        }
    }    

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ChangeStateRpc();
            ChangeColourRpc();
        }
    }
}
