using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class LevelManager : MonoBehaviour
{
    //Keep same instance of LevelManager object even when scene reloads/player dies
    public static LevelManager instance;

    public static Vector2 respawnPoint;

    private static GameObject playerPrefab;

    public static bool levelStart = true;

    public static bool shouldFade = true;

    public static PlayerDeath playerDeath;
    public static PlayerCollisions playerCollisions;
    public static Timer timer;

    private static GameObject collectibles;

    public static Animator fadeImageAnimator;

    public static PlayerInput playerInput;
    public static InputActionAsset actions;

    private void Awake()
    {
        playerPrefab = GameObject.Find("Player (0)");
        playerDeath = playerPrefab.GetComponent<PlayerDeath>();
        playerCollisions = playerPrefab.GetComponent<PlayerCollisions>();
        timer = GameObject.Find("TimerText").GetComponent<Timer>();
        collectibles = GameObject.Find("Collectibles");
        fadeImageAnimator = GameObject.Find("FadeImage").GetComponent<Animator>();
        playerInput = playerPrefab.GetComponent<PlayerInput>();
        actions = playerInput.actions;

        DontDestroyOnLoad(gameObject);

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        if(shouldFade) {
            fadeImageAnimator.SetBool("startFade", true);
            shouldFade = false;
        }

        //Set respawn to start of level when level starts
        if (levelStart)
        {
            respawnPoint = playerPrefab.transform.position;
        }
        playerPrefab.transform.position = respawnPoint;
    }

    //Reload scene to respawn player
    public void Respawn()
    {
        String sceneName = SceneManager.GetActiveScene().name;
        if(sceneName.Contains("Hard")) {
            reset();
        }
        SceneManager.LoadScene(sceneName);
    }

    public void setRespawnPoint(Vector2 newRespawnPoint)
    {
        respawnPoint = newRespawnPoint;
        levelStart = false;
    }

    public void EndLevel()
    {   
        //Disable player input
        actions.FindActionMap("Player").FindAction("Move").Disable();
        actions.FindActionMap("Player").FindAction("Jump").Disable();
        actions.FindActionMap("Player").FindAction("Crouch").Disable();
        actions.FindActionMap("Player").FindAction("Talk").Disable();
        actions.FindActionMap("Player").FindAction("Pause").Disable();

        int deathsCount = playerDeath.getDeathsCounter();
        int collectiblesCount = playerCollisions.getCollectiblesCounter();
        TimeSpan time = timer.getTimePlaying();

        String level = SceneManager.GetActiveScene().name.Substring(0, 6);

        if (!PlayerPrefs.HasKey($"{level}Deaths") || deathsCount < PlayerPrefs.GetInt($"{level}Deaths"))
        {
            PlayerPrefs.SetInt($"{level}Deaths", deathsCount);
        }
        if (collectiblesCount > PlayerPrefs.GetInt($"{level}Collectibles"))
        {
            PlayerPrefs.SetInt($"{level}Collectibles", collectiblesCount);
        }
        if (time < TimeSpan.Parse($"00:{PlayerPrefs.GetString($"{level}Time", "23:59:59")}"))
        {
            PlayerPrefs.SetString($"{level}Time", time.ToString("mm':'ss'.'ff"));
        }

        reset();

        //Mark level as complete for next level unlock
        PlayerPrefs.SetString($"{level}", "complete");

        StartCoroutine("fadeToNextScene");
        
    }

    //Fade to black and then load appropriate scene
    IEnumerator fadeToNextScene()
    {
        fadeImageAnimator.SetBool("endFade", true);
        yield return new WaitForSeconds(1.75f);
        if(SceneManager.GetActiveScene().name.Contains("Level4")) {
            SceneManager.LoadScene("Ending"); 
        }
        else {
            PlayerPrefs.SetString("SkipToLevels", "true");
            SceneManager.LoadScene("MainMenu");
        }
    }

    public void reset()
    {
        levelStart = true;
        shouldFade = true;

        //Reset collectibles for new level
        GameObject.Destroy(collectibles);

        //Reset deaths for new level
        playerDeath.setDeathsCounter(0);
        playerDeath.setDeathsText();

        //Reset collectibles for new level
        playerCollisions.setCollectiblesCounter(0);
        playerCollisions.setCollectiblesText();

        //Reset timer for new level
        timer.EndTimer();
    }
}
