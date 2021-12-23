using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenuController : MonoBehaviour
{
    public GameObject mainMenu;
    public AudioMixer audioMixer;

    //Set volume
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }

    //Set quality
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    //Set to fullscreen
    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    //Clear save data by clearing playerprefs
    public void clearSaveData()
    {
        PlayerPrefs.DeleteAll();
    }

    public void back()
    {
        //To main menu
        this.gameObject.SetActive(false);
        mainMenu.SetActive(true);

        //Select first button on main menu for keyboard navigation
        mainMenu.GetComponentInChildren<Button>().Select();
    }
}
