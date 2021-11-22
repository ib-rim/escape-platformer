using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{   
    public PlayerDeath playerDeath;
    public PlayerCollisions playerCollisions;
    public Timer timer;

    public int levelToLoadInt;
    public string levelToLoadStr;
    public bool useIntToLoad = false;
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

        if (useIntToLoad)
        {
            SceneManager.LoadScene(levelToLoadInt);   
        }
        else
        {
            SceneManager.LoadScene(levelToLoadStr);
        }
    }
}
