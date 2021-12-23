using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{   
    public PlayerDeath playerDeath;
    public PlayerCollisions playerCollisions;
    public Timer timer;

    public int level;
    private GameObject collectibles;

    private void Awake() {
        playerDeath = GameObject.Find("Player (0)").GetComponent<PlayerDeath>();
        playerCollisions = GameObject.Find("Player (0)").GetComponent<PlayerCollisions>();
        timer = GameObject.Find("TimerText").GetComponent<Timer>();
        collectibles = GameObject.Find("Collectibles");   
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            LoadScene();
        }
    }

    void LoadScene()
    {   
        int deathsCount = playerDeath.getDeathsCounter();
        int collectiblesCount = playerCollisions.getCollectiblesCounter();

        PlayerPrefs.SetInt($"Level{level}Deaths", PlayerPrefs.GetInt($"Level{level}Deaths") + deathsCount);
        if(collectiblesCount > PlayerPrefs.GetInt($"Level{level}Collectibles")) {
            PlayerPrefs.SetInt($"Level{level}Collectibles", collectiblesCount);
        }

        LevelManager.levelStart = true;
        
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

        //Load Main Menu and mark level as complete for next level unlock
        PlayerPrefs.SetString($"Level{level}", "complete");
        PlayerPrefs.SetString("SkipToLevels", "true");
        SceneManager.LoadScene("MainMenu");
    }
}
