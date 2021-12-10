using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject optionsMenu;
    public GameObject levelMenu;

    public void startGame() {
        SceneManager.LoadScene("Main");
    }

    public void options() {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }

    public void optionsBack() {
        optionsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

     public void levelBack() {
        levelMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void levelSelect() {
        mainMenu.SetActive(false);
        levelMenu.SetActive(true);
    }

    public void exitGame() {
        Debug.Log("Quit");
        Application.Quit();
    }
}