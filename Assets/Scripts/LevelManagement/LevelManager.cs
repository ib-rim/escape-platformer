using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    //Keep same instance of LevelManager object even when scene reloads/player dies
    public static LevelManager instance;

    public static Vector2 respawnPoint;

    private static GameObject playerPrefab;

    public static bool levelStart = true;

    public static PlayerDeath playerDeath;
    public static PlayerCollisions playerCollisions;
    public static Timer timer;

    private static GameObject collectibles;

    private void Awake()
    {
        playerPrefab = GameObject.Find("Player (0)");
        playerDeath = playerPrefab.GetComponent<PlayerDeath>();
        playerCollisions = playerPrefab.GetComponent<PlayerCollisions>();
        timer = GameObject.Find("TimerText").GetComponent<Timer>();
        collectibles = GameObject.Find("Collectibles");

        DontDestroyOnLoad(gameObject);

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
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

        //Load Main Menu and mark level as complete for next level unlock
        PlayerPrefs.SetString($"{level}", "complete");

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
