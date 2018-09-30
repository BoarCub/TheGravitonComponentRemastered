using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsManager : MonoBehaviour {

    //Player Prefs allows Unity to store player preferences between sessions on any platform
    //Up to 1 MB

    //When A Level Is Unlocked, It Means That The Level Is Completed

    //Some Centralized PlayerPrefs Keys
    const string MASTER_VOLUME_KEY = "master_volume";
    const string LEVEL_KEY = "level_unlocked_";

    //Initialization
    void Start()
    {
        if (!PlayerPrefs.HasKey(MASTER_VOLUME_KEY))
        {
            SetMasterVolume(0.5f);
        }
    }

    //Sets Master Volume
    public void SetMasterVolume(float volume)
    {
        //Decimal Volume Scale, Checks If Parameter Follows Scale
        if (volume > 0f && volume < 1f)
            PlayerPrefs.SetFloat(MASTER_VOLUME_KEY, volume);
    }

    //Gets Master Volume
    public float GetMasterVolume()
    {
        return PlayerPrefs.GetFloat(MASTER_VOLUME_KEY);
    }

    //Unlocks Level
    public void UnlockLevel (int level)
    {
        //Concatenates Level Key Name and Sets Int to 1
        //1 is for True and 0 is for False, boolean is not supported
        PlayerPrefs.SetInt(LEVEL_KEY + level.ToString(), 1);
    }

    //Checks If Level Is Unlocked
    public bool IsLevelUnlocked(int level)
    {
        //Returns Boolean
        return PlayerPrefs.GetInt(LEVEL_KEY + level.ToString()) == 1;
    }

    //Resets All PlayerPrefsData
    public void ResetPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
}