using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PauseMenuController : MonoBehaviour
{

    public static bool GameIsPaused = false;

    public GameObject pauseMenu;
    public GameObject optionsMenu; 
    public GameObject pausePanel;

    public PlayerDeath playerDeath;
    public PlayerCollisions playerCollisions;
    public Timer timer;

    private GameObject collectibles;



    private void Awake() {
        playerDeath = GameObject.Find("Player (0)").GetComponent<PlayerDeath>();
        playerCollisions = GameObject.Find("Player (0)").GetComponent<PlayerCollisions>();
        timer = GameObject.Find("TimerText").GetComponent<Timer>();
        collectibles = GameObject.Find("Collectibles");   

    }



    //resume game
    public void resume()
    {
       pauseMenu.SetActive(false);
       pausePanel.SetActive(false);
       Time.timeScale = 1f;
       GameIsPaused = false;
    }
    public void pause()
    {
        pausePanel.SetActive(true);
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    //restart game 
    public void restart()
    {
        //reload current scene
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

        pauseMenu.SetActive(false);
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;

        Scene scene = SceneManager.GetActiveScene(); 
        SceneManager.LoadScene(scene.name);
    }

    //change options
    public void options()
    {
        //To options screen
        pauseMenu.SetActive(false);
        optionsMenu.SetActive(true);

        //Select first button on options screen for keyboard navigation
        optionsMenu.GetComponentInChildren<Slider>().Select();
    }

    //Quit game
    public void returnMainMenu()
    {

         //reload current scene
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

        pauseMenu.SetActive(false);
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;

        PlayerPrefs.SetString("SkipToLevels", "true");
        SceneManager.LoadScene("MainMenu");
    }
}