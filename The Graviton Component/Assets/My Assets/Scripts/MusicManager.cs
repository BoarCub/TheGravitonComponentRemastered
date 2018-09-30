using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour {

    //Stores sceneName and corresponding Audio Clip
    //Example: Entry 0 in levelAudioClip pairs with Entry 0 in levelSceneName
    public AudioClip[] levelAudioClip;
    public string[] levelSceneName;

    public AudioSource audioSource;

    AudioClip thisLevelMusic;

    // Use this for initialization
    void Awake ()
    {
        //This gameobject stays for all scenes
        DontDestroyOnLoad(gameObject);
    }

    //Changes Volume From Parameter
    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }

    //Start listening for scene change
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    //Stop listening for scene change
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        for (int i = 0; i < levelSceneName.Length; i++)
        {
            if (levelSceneName[i] == SceneManager.GetActiveScene().name)
            {
                if (levelAudioClip[i] != audioSource.clip)
                {
                    audioSource.clip = levelAudioClip[i];
                    audioSource.loop = true;
                    audioSource.Play();
                }
            }
        }
    }
}
