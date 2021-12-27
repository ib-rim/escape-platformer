using UnityEngine.Audio;
using System;
using UnityEngine;
 using UnityEngine.SceneManagement;


public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    void Awake () {
        foreach(Sound s in sounds) 
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public void Play (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }

    void Start() {

        // Create a temporary reference to the current scene.
        Scene currentScene = SceneManager.GetActiveScene ();
 
        // Retrieve the name of this scene.
        string sceneName = currentScene.name;

        //main menu music
        if (sceneName == "MainMenu") {
            Play("MainMenuTheme");
        }
        //level 1 music
        else if(sceneName == "Level1") {
            Play("Level1Music");
        }
        //level 2 music
        else if(sceneName == "Level2") {
            Play("Level2Music");
        }
        //level 3 music
        else if(sceneName == "Level3") {
            Play("Level3Music");
        }
        //level 4 music
        else if(sceneName == "Level4") {
            Play("Level4Music");
        }
    }
}
