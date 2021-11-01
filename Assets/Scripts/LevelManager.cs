using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {
    public static LevelManager instance;

    public static Vector2 respawnPoint = new Vector2(0, 3);
    public GameObject playerPrefab;

    private void Awake() {
        instance = this;
        playerPrefab.GetComponent<Rigidbody2D>().position = respawnPoint;
   }

    public void Respawn() {
        SceneManager.LoadScene("Main");
    }

    public void setRespawnPoint(Vector2 newRespawnPoint) {
        respawnPoint = newRespawnPoint;
    }
}
