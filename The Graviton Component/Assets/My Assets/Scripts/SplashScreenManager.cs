using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreenManager : MonoBehaviour {

    //Autoloads Next Level After This Much Time Passes
    public float autoLoadTime = 4f;

    // Use this for initialization
    void Start()
    {
        //Calls After Invoke Finishes
        Invoke("LoadStartMenu", autoLoadTime);
    }

    //Loads Start Menu
    public void LoadStartMenu()
    {
        //Loads Scene While Letting Current Scene Continue Processes
        SceneManager.LoadSceneAsync("Start Menu");
    }
}
