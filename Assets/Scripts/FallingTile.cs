using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

enum TileState
{
    Normal,
    Yellow,
    Orange,
    Red,
}

public class FallingTile : MonoBehaviour
{
    [SerializeField] private Material yellow;
    [SerializeField] private Material orange;
    [SerializeField] private Material red;
    private TileState state = TileState.Normal;
    private MeshRenderer meshRenderer;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    // Decrease 'health' of tile
    // Destory if players have walked on it too much
    private void ChangeState()
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
                Destroy(gameObject);
                break;
        }
    }

    // Change material of tile depending on state
    private void ChangeColour()
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
            ChangeState();
            ChangeColour();
        }
    }
}
