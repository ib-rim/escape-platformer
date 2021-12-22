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
                FindObjectOfType<AudioManager>().Play("LavaDeath");
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
        //play playerdeath sound effect
        //FindObjectOfType<AudioManager>().Play("Player Death");

        setDeathsCounter(deathsCounter+1);
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
