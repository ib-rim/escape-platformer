using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerups : MonoBehaviour
{   
    PlayerController player; 
    SpriteRenderer rend;
    Color playerColor = new Color32(255, 255, 255, 255);
    Color jumpColor = new Color32(125, 212, 144, 255); 
    Color speedColor = new Color32(236, 108, 0, 255); 
    Color slowFallColor = new Color32(201, 181, 179, 255); 
    Color invincibilityColor = new Color32(236, 225, 0, 255);
    Color slowColor = new Color32(215, 190, 137, 255);

    public float powerupTime = 3;

    void Start() {
        rend = GetComponent<SpriteRenderer>();
        player = GetComponent<PlayerController>();
    }

    public void resetPlayer() {
        player.jumpSpeed = PlayerController.defaultJumpSpeed;
        player.moveSpeed = PlayerController.defaultMoveSpeed;
        GetComponent<PlayerDeath>().invincible = false;
        GetComponent<Rigidbody2D>().gravityScale = 1f;
        rend.material.color = playerColor;
    }

    void OnTriggerEnter2D(Collider2D other) {

        if(other.gameObject.tag == "JumpBoost") {
            other.gameObject.SetActive(false);
            player.jumpSpeed = 8f;
            rend.material.color = jumpColor;
            StartCoroutine(waitAndReset());
        }   

        if(other.gameObject.tag == "SpeedBoost") {
            other.gameObject.SetActive(false);
            player.moveSpeed = 8f;
            rend.material.color = speedColor;
            StartCoroutine(waitAndReset());
        } 

        if(other.gameObject.tag == "SlowFall") {
            other.gameObject.SetActive(false);
            rend.material.color = slowFallColor;
            StartCoroutine(slowFall());
        } 

        if(other.gameObject.tag == "Invincibility") {
            other.gameObject.SetActive(false);
            rend.material.color = invincibilityColor;
            GetComponent<PlayerDeath>().invincible = true;
            StartCoroutine(waitAndReset());
        }

        if(other.gameObject.tag == "Slow") {
            other.gameObject.SetActive(false);
            player.moveSpeed = 2f;
            player.jumpSpeed = 2f;
            rend.material.color = slowColor;
            StartCoroutine(waitAndReset());
        } 
    }

    IEnumerator waitAndReset() {
        yield return new WaitForSeconds(powerupTime);
        resetPlayer();
    }

    IEnumerator slowFall() {
        
        for(float t = powerupTime; t > 0; t-=Time.deltaTime) {
            if (GetComponent<Rigidbody2D>().velocity.y < -0.1)
            {
                GetComponent<Rigidbody2D>().gravityScale = 0.1f;
            }
            yield return null;
        }
        resetPlayer();
    }
}
