using UnityEngine.Audio;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{   
    //Keep same instance of AudioManager object even when scene reloads/player dies
    public static AudioManager instance;
    private static GameObject instanceGameObject;
    
    public static String mostRecentScene;

    public Sound[] music;
    public Sound[] SFX;

    private Sound footsteps;

    public AudioMixerGroup musicMixer;
    public AudioMixerGroup SFXMixer;
    public AudioMixer audioMixer;

    void Awake () {
        DontDestroyOnLoad(gameObject);

        if(instance == null) {
            instance = this;   
            instanceGameObject = gameObject;

            foreach(Sound m in music) 
            {
                m.source = gameObject.AddComponent<AudioSource>();
                m.source.clip = m.clip;
                m.source.outputAudioMixerGroup = musicMixer;
                m.source.volume = m.volume;
                m.source.pitch = m.pitch;
                m.source.loop = m.loop;
            }

            foreach(Sound s in SFX) 
            {
                s.source = gameObject.AddComponent<AudioSource>();
                s.source.clip = s.clip;
                s.source.outputAudioMixerGroup = SFXMixer;
                s.source.volume = s.volume;
                s.source.pitch = s.pitch;
                s.source.loop = s.loop;
            }

            footsteps = Array.Find(SFX, s => s.name == "Footsteps");
            mostRecentScene = SceneManager.GetActiveScene().name;
        }
        else {
            Destroy(gameObject);
        }
    }

    //Start music and change volumes to saved values 
    void Start() {
        changeMusic();
        float musicVolume = PlayerPrefs.GetFloat("MusicVol");
        float SFXVolume = PlayerPrefs.GetFloat("SFXVol");
        SetMusicVolume(musicVolume);
        SetSFXVolume(SFXVolume);
    }

    //Set music volume in audioMixer and playerPrefs
    public void SetMusicVolume(float volume)
    {   
        audioMixer.SetFloat("MusicVol", volume);
        PlayerPrefs.SetFloat("MusicVol", volume);
    }

    //Set SFX volume in audioMixer and playerPrefs
    public void SetSFXVolume(float volume)
    {   
        audioMixer.SetFloat("SFXVol", volume);
        PlayerPrefs.SetFloat("SFXVol", volume);
    }

    //Find and play given music 
    public void PlayMusic (string name)
    {
        Sound m = Array.Find(music, m => m.name == name);
        m.source.Play();
    }

    //Find and play given SFX 
    public void PlaySFX (string name)
    {
        Sound s = Array.Find(SFX, s => s.name == name);
        s.source.Play();
    }

    //Play footsteps SFX
    public void PlayFootsteps ()
    {
        footsteps.source.mute = false;
    }

    //Stop footsteps SFX
    public void StopFootsteps ()
    {
        footsteps.source.mute = true;
    }

    //Find and check if given music is playing
    public bool isPlaying (string name)
    {
        Sound m = Array.Find(music, m => m.name == name);
        return m.source.isPlaying;
    }

    public void StopAllMusic ()
    {
        foreach(Sound m in music) 
        {
            m.source.Stop();
        }
    }

    //Change music depending on current scene
    public void changeMusic() {

        string sceneName = SceneManager.GetActiveScene().name;

        StopAllMusic();

        if (sceneName == "MainMenu") {
            PlayMusic("MenuMusic");
        }
        else if(sceneName == "Level1") {
            PlayMusic("Level1Music");
        }
        else if(sceneName == "Level2") {
            PlayMusic("Level2Music");
        }
        else if(sceneName == "Level3") {
            PlayMusic("Level3Music");
        }
        else if(sceneName == "Level4") {
            PlayMusic("Level4Music");
        }
    }

    private void Update() {

        string sceneName = SceneManager.GetActiveScene().name;

        //Only change music on scene change
        if(mostRecentScene != sceneName && instanceGameObject == gameObject) {
            mostRecentScene = sceneName;
            changeMusic();
        }

        if(sceneName == "MainMenu" && footsteps.source.enabled) {
            StopFootsteps();
        }

        //Switch to looping version of music for level 1
        if(sceneName == "Level1" && !isPlaying("Level1Music") && !isPlaying("Level1MusicLoop")) {
            PlayMusic("Level1MusicLoop");
        }

        //Switch to looping version of music for level 3
        if(sceneName == "Level3" && !isPlaying("Level3Music") && !isPlaying("Level3MusicLoop")) {
            PlayMusic("Level3MusicLoop");
        }
    }
}
