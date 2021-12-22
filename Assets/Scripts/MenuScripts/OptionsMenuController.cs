using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenuController : MonoBehaviour
{
    public GameObject mainMenu;
    public AudioMixer audioMixer;

    //method to set volume
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }

    //method to set quality
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    //method to set to fullscreen
    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    public void back()
    {
        this.gameObject.SetActive(false);
        mainMenu.SetActive(true);
        mainMenu.GetComponentInChildren<Button>().Select();
    }
}
