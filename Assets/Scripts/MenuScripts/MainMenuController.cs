using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject optionsMenu;
    public GameObject levelMenu;

    public void startGame()
    {
        mainMenu.SetActive(false);
        levelMenu.SetActive(true);
        levelMenu.GetComponentInChildren<Button>().Select();
    }

    public void options()
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);
        optionsMenu.GetComponentInChildren<Toggle>().Select();
    }

    public void exitGame()
    {
        Application.Quit();
    }
}