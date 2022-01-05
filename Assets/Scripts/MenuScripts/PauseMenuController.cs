using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PauseMenuController : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenu;
    public GameObject optionsMenu;
    public GameObject controlsMenu;
    public GameObject pausePanel;

    public PlayerDeath playerDeath;
    public PlayerCollisions playerCollisions;
    public Timer timer;

    public PlayerInput playerInput;
    public InputActionAsset actions;

    private GameObject collectibles;

    private void Awake()
    {
        playerDeath = GameObject.Find("Player (0)").GetComponent<PlayerDeath>();
        playerCollisions = GameObject.Find("Player (0)").GetComponent<PlayerCollisions>();
        playerInput = GameObject.Find("Player (0)").GetComponent<PlayerInput>();
        timer = GameObject.Find("TimerText").GetComponent<Timer>();
        collectibles = GameObject.Find("Collectibles");
        actions = playerInput.actions;
    }

    //resume game
    public void resume()
    {
        pauseMenu.SetActive(false);
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
        actions.FindActionMap("Player").FindAction("Move").Enable();
        actions.FindActionMap("Player").FindAction("Jump").Enable();
        actions.FindActionMap("Player").FindAction("Crouch").Enable();
        actions.FindActionMap("Player").FindAction("Talk").Enable();
        GameIsPaused = false;
    }

    //Player Input to activate pause menu (ESC)
    public void pause(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (!GameIsPaused)
            {
                //pause game and display pause menu
                pausePanel.SetActive(true);
                pauseMenu.SetActive(true);
                Time.timeScale = 0f;
                actions.FindActionMap("Player").FindAction("Move").Disable();
                actions.FindActionMap("Player").FindAction("Jump").Disable();
                actions.FindActionMap("Player").FindAction("Crouch").Disable();
                actions.FindActionMap("Player").FindAction("Talk").Disable();
                GameIsPaused = true;

                //Select first button on pause screen for keyboard navigation
                pauseMenu.GetComponentInChildren<Button>().Select();
            }
            else if (pauseMenu.activeInHierarchy)
            {
                resume();
            }
            else if (optionsMenu.activeInHierarchy)
            {
                optionsMenu.SetActive(false);
                pauseMenu.SetActive(true);

                //Select first button on pause menu for keyboard navigation
                pauseMenu.GetComponentInChildren<Button>().Select();
            }
        }
    }

    //restart game
    public void restart()
    {
        LevelManager.instance.reset();
        LevelManager.instance.Respawn();
        resume();

        AudioManager.instance.changeMusic();
    }

    //change options
    public void options()
    {
        //To options screen
        pauseMenu.SetActive(false);
        optionsMenu.SetActive(true);

        //Select first Slider on options screen for keyboard navigation
        optionsMenu.GetComponentInChildren<Slider>().Select();
    }

    //View controls
    public void controls()
    {
        //To controls screen
        pauseMenu.SetActive(false);
        controlsMenu.SetActive(true);

        //Select first button on controls screen for keyboard navigation
        controlsMenu.GetComponentInChildren<Button>().Select();
    }

    //Quit game
    public void returnMainMenu()
    {
        LevelManager.instance.reset();

        resume();

        PlayerPrefs.SetString("SkipToLevels", "true");
        SceneManager.LoadScene("MainMenu");
    }
}
