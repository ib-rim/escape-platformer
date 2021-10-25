using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDeath : MonoBehaviour {
    
    private int livesRemaining = 3;
    public Text livesText;

    private void Start() {
        livesText.text = "Lives: " + livesRemaining.ToString();
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "FallThreshold")
        {
            LevelManager.instance.Respawn();
        }
        else if (other.gameObject.tag == "Spikes")
        {
            livesRemaining -= 1;
            livesText.text = "Lives: " + livesRemaining.ToString();
        }
    }
}
