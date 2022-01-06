using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDeath : MonoBehaviour {
    
    private static int deathsCounter = 0;
    public Text deathsText;
    public bool invincible;
    public bool dead;
    public bool shouldDie;

    public Animator player_animator;

    public PlayerController playerController;


    private void Awake() {
        setDeathsText();
        playerController = GetComponent<PlayerController>();
    }

    //Keep track of whether player has collided with an obstacle
    private void OnCollisionEnter2D(Collision2D other) {

        if (other.gameObject.CompareTag("FallThreshold"))
        {
            shouldDie = true;
        }
        
        if (other.gameObject.CompareTag("Spikes"))
        {   
            shouldDie = true;
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            shouldDie = true;
        }

        if (other.gameObject.CompareTag("Arrow"))
        {   
            shouldDie = true;
        }
    }

    //Keep track of whether player has left an obstacle
    private void OnCollisionExit2D(Collision2D other) {

        if (other.gameObject.CompareTag("FallThreshold"))
        {
            shouldDie = false;
        }
        
        if (other.gameObject.CompareTag("Spikes"))
        {   
            shouldDie = false;
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            shouldDie = false;
        }

        if (other.gameObject.CompareTag("Arrow"))
        {   
            shouldDie = false;
        }
    }

    private void FixedUpdate() {
        //If player is currently colliding with an obstacle and not invincible
        if(shouldDie && !invincible) {
            death();
        }
    }

    private void death() {
        //Only activate once
        if(!dead) {
            dead = true;
            playerController.enabled = false;
            player_animator.SetBool("death", true);
            AudioManager.instance.PlaySFX("Death");
            setDeathsCounter(deathsCounter+1);
            setDeathsText();
            StartCoroutine("respawn");
        } 
    }

    IEnumerator respawn()
    {
        yield return new WaitForSeconds(0.3f);
        LevelManager.instance.Respawn();
        dead = false;
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
