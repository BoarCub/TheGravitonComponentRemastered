using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Obstacle : MonoBehaviour {

    public LevelManagement LevelManager;

    // Use this for initialization
    void Start()
    {
        LevelManager = GameObject.FindObjectOfType<LevelManagement>();
    }

	//When Collider is Triggered
    private void OnTriggerEnter2D(Collider2D obj)
    {
        if(obj.tag == "Player")
        {
            LevelManager.GameOver();
        }

        if (obj.tag == "Enemy")
        {
            Destroy(obj.gameObject);
        }
    }
}
