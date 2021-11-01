using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDeath : MonoBehaviour {
    
    private static int deathsCounter = 0;
    public Text deathsText;
    public bool invincible;

    private void Start() {
        deathsText.text = "Deaths: " + deathsCounter.ToString();
    }

    private void OnCollisionStay2D(Collision2D other) {

        if(!invincible) {

            if (other.gameObject.CompareTag("FallThreshold"))
            {
                death();
            }
            
            if (other.gameObject.CompareTag("Spikes"))
            {   
                death();
            }
        }
    }

    private void death() {
        deathsCounter += 1;
        deathsText.text = "Deaths: " + deathsCounter.ToString();
        LevelManager.instance.Respawn();
    }
}
