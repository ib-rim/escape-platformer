using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDeath : MonoBehaviour {
    
    private int livesRemaining = 3;
    public Text livesText;
    public bool invincible;

    private void Start() {
        livesText.text = "Lives: " + livesRemaining.ToString();
    }

    private void OnCollisionStay2D(Collision2D other) {

        if(!invincible) {

            if (other.gameObject.tag == "FallThreshold")
            {
                LevelManager.instance.Respawn();
                livesRemaining -= 1;
                livesText.text = "Lives: " + livesRemaining.ToString();
            }
            
            if (other.gameObject.tag == "Spikes")
            {   
                LevelManager.instance.Respawn();
                livesRemaining -= 1;
                livesText.text = "Lives: " + livesRemaining.ToString();
            }
        }
    }
}
