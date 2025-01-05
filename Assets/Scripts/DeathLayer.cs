using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathLayer : MonoBehaviour
{
    private bool showDeathMsg;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Destroy(other);
            //SceneManager.LoadScene("Death");
        }
    }

    //private void OnGUI()
    //{
    //    if (showDeathMsg)
    //    {
    //        GUI.TextField(new Rect(10, 10, 150, 100), "You Died! Thanks for playing!");
    //    }
    //}
}
