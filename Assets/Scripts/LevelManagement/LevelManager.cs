using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {
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
            Destroy(gameObject); //prevent multiple players from spawning.
        }

        Debug.Log(levelStart);
        if(levelStart) { //if new level
            respawnPoint = playerPrefab.transform.position; //move player to respawn point
        }
        playerPrefab.GetComponent<Rigidbody2D>().position = respawnPoint;
        
   }

    public void Respawn() {
        Scene scene = SceneManager.GetActiveScene(); 
        SceneManager.LoadScene(scene.name);
    }

    public void setRespawnPoint(Vector2 newRespawnPoint) {
        respawnPoint = newRespawnPoint;
        levelStart = false;
    }
}
