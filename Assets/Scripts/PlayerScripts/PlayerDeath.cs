using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDeath : MonoBehaviour {
    
    private static int deathsCounter = 0;
    public Text deathsText;
    public bool invincible;

    private void Start() {
        deathsText.text = $"Deaths: {deathsCounter.ToString()}";
    }

    private void OnCollisionStay2D(Collision2D other) {

        if(!invincible) { //if player does not have invincibility. 

            if (other.gameObject.CompareTag("FallThreshold")) //if player falls on lava
            {
                death(); //player dies.
            }
            
            if (other.gameObject.CompareTag("Spikes")) //if player touches spikes
            {   
                death();
            }

            if (other.gameObject.CompareTag("Enemy")) //if player touches enemy
            {
                death();
            }
        }
    }

    private void death() {
        deathsCounter += 1; //increment death counter
        deathsText.text = $"Deaths: {deathsCounter.ToString()}";
        LevelManager.instance.Respawn(); //respawn player 
    }
}
