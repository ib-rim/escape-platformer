using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDeath : MonoBehaviour {
    
    private static int deathsCounter = 0;
    public Text deathsText;
    public bool invincible;

    private void Awake() {
        setDeathsText();
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

            if (other.gameObject.CompareTag("Enemy"))
            {
                death();
            }

            if (other.gameObject.CompareTag("Arrow"))
            {
                death();
            }
        }
    }

    private void death() {
        //deathsCounter += 1;
        setDeathsCounter(deathsCounter+1);
        //deathsText.text = $"Deaths: {deathsCounter.ToString()}";
        setDeathsText();
        LevelManager.instance.Respawn();
    }

    public void setDeathsCounter(int deaths) {
        deathsCounter = deaths;
    }

    public void setDeathsText() {
        deathsText.text = $"Deaths: {deathsCounter.ToString()}";
    }
}
