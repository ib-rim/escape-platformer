using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectController : MonoBehaviour
{
    public GameObject mainMenu;

    public void loadLevel(string level)
    {
        SceneManager.LoadScene(level);
    }

    public void back()
    {
        this.gameObject.SetActive(false);
        mainMenu.SetActive(true);
        mainMenu.GetComponentInChildren<Button>().Select();
    }
}
