using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectController : MonoBehaviour
{
    public GameObject mainMenu;

    public GameObject level1Button;
    public GameObject level2Button;
    public GameObject level3Button;
    public GameObject level4Button;

    void OnEnable()
    {
        //Check which levels are unlocked
        if(PlayerPrefs.GetString("Level1") == "complete") {
            level2Button.GetComponent<Button>().interactable = true;
        }
        else {
            level2Button.GetComponent<Button>().interactable = false;
        }
        if(PlayerPrefs.GetString("Level2") == "complete") {
            level3Button.GetComponent<Button>().interactable = true;
        }
        else {
            level3Button.GetComponent<Button>().interactable = false;
        }
        if(PlayerPrefs.GetString("Level3") == "complete") {
            level4Button.GetComponent<Button>().interactable = true;
        }
        else {
            level4Button.GetComponent<Button>().interactable = false;
        }
        level1Button.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = $"Level 1:     Deaths: {PlayerPrefs.GetInt("Level1Deaths")}   Collectibles: {PlayerPrefs.GetInt("Level1Collectibles")}";
        level2Button.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = $"Level 2:     Deaths: {PlayerPrefs.GetInt("Level2Deaths")}   Collectibles: {PlayerPrefs.GetInt("Level2Collectibles")}";
        level3Button.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = $"Level 3:     Deaths: {PlayerPrefs.GetInt("Level3Deaths")}   Collectibles: {PlayerPrefs.GetInt("Level3Collectibles")}";
        level4Button.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = $"Level 4:     Deaths: {PlayerPrefs.GetInt("Level4Deaths")}   Collectibles: {PlayerPrefs.GetInt("Level4Collectibles")}";
    }

    //Loads selected level (specified in editor)
    public void loadLevel(string level)
    {
        SceneManager.LoadScene(level);
    }

    public void back()
    {
        //To main menu
        this.gameObject.SetActive(false);
        mainMenu.SetActive(true);

        //Select first button on main menu for keyboard navigation
        mainMenu.GetComponentInChildren<Button>().Select();
    }
}
