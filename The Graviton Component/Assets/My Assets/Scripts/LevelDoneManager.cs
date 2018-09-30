using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelDoneManager : MonoBehaviour {

    //Controls Actions From Level Complete UI
	public void MainMenu ()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Start Menu");
    }

    public void Retry()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void NextSector()
    {
        Time.timeScale = 1;

        //Loads Next Scene
        if (SceneManager.sceneCountInBuildSettings-1> SceneManager.GetActiveScene().buildIndex)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //Goes To Main Menu If There Are No More Scenes In The Build Index
        else
            SceneManager.LoadScene("Start Menu");
    }

}
