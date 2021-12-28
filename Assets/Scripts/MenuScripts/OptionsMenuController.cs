using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenuController : MonoBehaviour
{
    public GameObject mainMenu;
    public AudioMixer audioMixer;
    public GameObject musicSlider;
    public GameObject SFXSlider;

    private void Start() {
        setMusicSliderValue();
        setSFXSliderValue();
    }

    //Set music volume
    public void SetMusicVolume(float value)
    {   
        float volume = Mathf.Log10(value) * 20; 
        AudioManager.instance.SetMusicVolume(volume);
    }

    public void setMusicSliderValue() {
        bool result = audioMixer.GetFloat("MusicVol", out float volume);
        float value = Mathf.Pow(10, volume / 20);
        musicSlider.GetComponent<Slider>().value = value;
    }

    //Set SFX volume
    public void SetSFXVolume(float value)
    {   
        float volume = Mathf.Log10(value) * 20; 
        AudioManager.instance.SetSFXVolume(volume);
    }

    public void setSFXSliderValue() {
        bool result = audioMixer.GetFloat("SFXVol", out float volume);
        float value = Mathf.Pow(10, volume / 20);
        SFXSlider.GetComponent<Slider>().value = value;
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
