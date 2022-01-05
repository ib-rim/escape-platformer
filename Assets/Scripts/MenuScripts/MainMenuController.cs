using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject optionsMenu;
    public GameObject controlsMenu;    
    public GameObject levelMenu;    

    private void Awake() {
        //Skip to level select screen when returning from level
        if (PlayerPrefs.GetString("SkipToLevels") == "true") {
            play();
            PlayerPrefs.SetString("SkipToLevels", "false");
        }
    }
    
    public void play()
    {
        //To level select screen
        mainMenu.SetActive(false);
        levelMenu.SetActive(true);

        //Select first Toggle on level select screen for keyboard navigation
        levelMenu.GetComponentInChildren<Toggle>().Select();
    }

    public void options()
    {
        //To options screen
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);

        //Select first Slider on options screen for keyboard navigation
        optionsMenu.GetComponentInChildren<Slider>().Select();
    }

    public void controls()
    {
        //To options screen
        mainMenu.SetActive(false);
        controlsMenu.SetActive(true);

        //Select first button on options screen for keyboard navigation
        controlsMenu.GetComponentInChildren<Button>().Select();
    }

    //Quit game
    public void exitGame()
    {
        Application.Quit();
    }
}