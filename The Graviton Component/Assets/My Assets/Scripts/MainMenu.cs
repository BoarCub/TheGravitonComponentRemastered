using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    //Volume Slider Object
    public Slider volumeSlider;

    //PlayerPrefs Game Object
    public GameObject PlayerPrefsObject;

    private MusicManager musicManager;

    //Use this for initialization
    void Start()
    {
        musicManager = GameObject.FindObjectOfType<MusicManager>();

        //Gets Volume
        musicManager.SetVolume(PlayerPrefsObject.GetComponent<PlayerPrefsManager>().GetMasterVolume());
    }

    //Called Once Per Frame
    void Update()
    {
        musicManager.SetVolume(volumeSlider.value);
    }

    //Methods Run Based On UI
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
	
    //Volume Slider
    public void SetVolume(float volume)
    {

    }

    //Saves Data to PlayerPrefs
    public void SaveToPlayerPrefs()
    {
        PlayerPrefsObject.GetComponent<PlayerPrefsManager>().SetMasterVolume(volumeSlider.value);
    }

    //Retrieves Data from PlayerPrefs
    public void RetrieveFromPlayerPrefs()
    {
        volumeSlider.value = PlayerPrefsObject.GetComponent<PlayerPrefsManager>().GetMasterVolume();
    }

    //Resets PlayerPrefs
    public void ResetPlayerPrefs()
    {
        //Calls Reset
        PlayerPrefsObject.GetComponent<PlayerPrefsManager>().ResetPlayerPrefs();
        //Restarts Scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //Selects Button (Useful For Controller Support)
    public void SelectButton(Button button)
    {
        button.Select();
    }
}