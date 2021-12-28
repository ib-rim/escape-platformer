using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectController : MonoBehaviour
{
    public GameObject mainMenu;

    public GameObject level1Button;
    public GameObject level1CollectiblesText;
    public GameObject level1DeathsText;
    public GameObject level1TimeText;

    public GameObject level2Button;
    public GameObject level2CollectiblesText;
    public GameObject level2DeathsText;
    public GameObject level2TimeText;

    public GameObject level3Button;
    public GameObject level3CollectiblesText;
    public GameObject level3DeathsText;
    public GameObject level3TimeText;

    public GameObject level4Button;
    public GameObject level4CollectiblesText;
    public GameObject level4DeathsText;
    public GameObject level4TimeText;

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
        
        level1CollectiblesText.GetComponent<TMPro.TextMeshProUGUI>().text  = $"x {PlayerPrefs.GetInt("Level1Collectibles")} / 9";
        level1DeathsText.GetComponent<TMPro.TextMeshProUGUI>().text  = $"x {PlayerPrefs.GetInt("Level1Deaths")}";
        level1TimeText.GetComponent<TMPro.TextMeshProUGUI>().text  = PlayerPrefs.GetString("Level1Time");

        level2CollectiblesText.GetComponent<TMPro.TextMeshProUGUI>().text  = $"x {PlayerPrefs.GetInt("Level2Collectibles")} / 11";
        level2DeathsText.GetComponent<TMPro.TextMeshProUGUI>().text  = $"x {PlayerPrefs.GetInt("Level2Deaths")}";
        level2TimeText.GetComponent<TMPro.TextMeshProUGUI>().text  = PlayerPrefs.GetString("Level2Time");

        level3CollectiblesText.GetComponent<TMPro.TextMeshProUGUI>().text  = $"x {PlayerPrefs.GetInt("Level3Collectibles")} / 5";
        level3DeathsText.GetComponent<TMPro.TextMeshProUGUI>().text  = $"x {PlayerPrefs.GetInt("Level3Deaths")}";
        level3TimeText.GetComponent<TMPro.TextMeshProUGUI>().text  = PlayerPrefs.GetString("Level3Time");

        level4CollectiblesText.GetComponent<TMPro.TextMeshProUGUI>().text  = $"x {PlayerPrefs.GetInt("Level4Collectibles")} / 6";
        level4DeathsText.GetComponent<TMPro.TextMeshProUGUI>().text  = $"x {PlayerPrefs.GetInt("Level4Deaths")}";
        level4TimeText.GetComponent<TMPro.TextMeshProUGUI>().text  = PlayerPrefs.GetString("Level4Time");
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
