using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManagement : MonoBehaviour {

    //UI GameObjects
    public GameObject completeLevelUI;
    public GameObject gameOverUI;

    //Buttons To Select
    public Button completeLevelButton;
    public Button gameOverButton;

    bool gameHasEnded = false;

	// Use this for initialization
	void Awake () {
        
    }

    public void GameOver()
    {
        if (!gameHasEnded)
        {
            gameHasEnded = true;
            LoseGame();
        }
    }
	
    //Called Whenever An Enemy Is Killed
	public void isEnemiesEmpty()
    {
        if(Enemy.enemies.Count <= 0)
        {
            try
            {
                //Calls Methpd After 0.25 seconds
                Invoke("WinGame", 0.25f);
            }
            catch
            {

            }
        }
    }

    //Win State
    public void WinGame()
    {
        if (!gameHasEnded)
        {
            gameHasEnded = true;
            Time.timeScale = 0;
            completeLevelUI.SetActive(true);
            completeLevelButton.Select();
        }
    }

    //Lose State
    public void LoseGame()
    {
        Time.timeScale = 0;
        gameOverUI.SetActive(true);
        gameOverButton.Select();
    }

}