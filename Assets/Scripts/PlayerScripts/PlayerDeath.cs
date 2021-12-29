using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDeath : MonoBehaviour {
    
    private static int deathsCounter = 0;
    public Text deathsText;
    public bool invincible;

    public Animator player_animator;

    public PlayerController playerController;

    private void Awake() {
        setDeathsText();
        playerController = GetComponent<PlayerController>();
    }

    private void OnCollisionEnter2D(Collision2D other) {

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
        playerController.enabled = false;
        player_animator.SetBool("death", true);
        AudioManager.instance.PlaySFX("Death");
        setDeathsCounter(deathsCounter+1);
        setDeathsText();
        StartCoroutine("respawn");
    }

    IEnumerator respawn()
    {
        yield return new WaitForSeconds(0.3f);
        LevelManager.instance.Respawn();
    }

    public int getDeathsCounter() {
        return deathsCounter;
    }

    public void setDeathsCounter(int deaths) {
        deathsCounter = deaths;
    }

    public void setDeathsText() {
        deathsText.text = $"x {deathsCounter.ToString()}";
    }
}
