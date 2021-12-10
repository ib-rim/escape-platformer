using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class OptionsMenu : MonoBehaviour
{

    public AudioMixer audioMixer;
   
   //method to set volume
   public void SetVolume (float volume) {
       Debug.Log(volume);

       audioMixer.SetFloat("volume", volume);
   }

   //method to set quality
   public void SetQuality (int qualityIndex)
   {
       QualitySettings.SetQualityLevel(qualityIndex);
   }

   //method to set to fullscreen
   public void SetFullScreen (bool isFullScreen) {
       Screen.fullScreen = isFullScreen;
   }
}
