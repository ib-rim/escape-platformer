using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDeath : MonoBehaviour {
    
    private int deathsCounter = 0;
    public Text deathsText;
    public bool invincible;

    private void Start() {
        deathsText.text = "Deaths: " + deathsCounter.ToString();
    }

    private void OnCollisionStay2D(Collision2D other) {

        if(!invincible) {

            if (other.gameObject.tag == "FallThreshold")
            {
                LevelManager.instance.Respawn();
                deathsCounter += 1;
                deathsText.text = "Deaths: " + deathsCounter.ToString();
            }
            
            if (other.gameObject.tag == "Spikes")
            {   
                LevelManager.instance.Respawn();
                deathsCounter += 1;
                deathsText.text = "Deaths: " + deathsCounter.ToString();
            }
        }
    }
}
