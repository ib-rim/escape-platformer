using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    //Keep same instance of LevelManager object even when scene reloads/player dies
    public static LevelManager instance;

    public static Vector2 respawnPoint;
    private GameObject playerPrefab;
    
    public static bool levelStart = true; 

    private void Awake() {

        playerPrefab = GameObject.Find("Player (0)");
        DontDestroyOnLoad(gameObject);

        if(instance == null) {
            instance = this;
        }
        else {
            Destroy(gameObject);
        }

        //Set respawn to start of level when level starts
        if(levelStart) {
            respawnPoint = playerPrefab.transform.position;
        }
        playerPrefab.GetComponent<Rigidbody2D>().position = respawnPoint;
        
   }

    //Reload scene to respawn player
    public void Respawn() {
        Scene scene = SceneManager.GetActiveScene(); 
        SceneManager.LoadScene(scene.name);
    }

    public void setRespawnPoint(Vector2 newRespawnPoint) {
        respawnPoint = newRespawnPoint;
        levelStart = false;
    }
}
