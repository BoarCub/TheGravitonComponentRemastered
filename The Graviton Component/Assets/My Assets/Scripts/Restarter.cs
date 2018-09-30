using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restarter : MonoBehaviour
{

    public LevelManagement LevelManager;

    // Use this for initialization
    void Start()
    {
        LevelManager = GameObject.FindObjectOfType<LevelManagement>();
    }

    //When Collider is Triggered
    private void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.tag == "Player")
        {
            LevelManager.GameOver();
        }
    }

}
