using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public int levelToLoadInt;
    public string levelToLoadStr;
    public bool useIntToLoad = false;

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
