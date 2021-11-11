using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public int levelToLoadInt;
    public string levelToLoadStr;
    public bool useIntToLoad = false;

    void Start()
    {
        
    }

    void Update()
    {
        
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
