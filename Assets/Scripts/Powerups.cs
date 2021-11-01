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
    Queue<Color> colorQueue = new Queue<Color>(); 

    public float powerupTime = 3;
    
    void Start() {
        rend = GetComponent<SpriteRenderer>();
        player = GetComponent<PlayerController>();
    }

    public void resetPlayer() {
        colorQueue.Dequeue();
        if(colorQueue.Count > 0) {
            rend.material.color = colorQueue.Peek();
        }
        else {
            rend.material.color = playerColor;
        }
    }

    void OnTriggerEnter2D(Collider2D other) {

        if(other.gameObject.tag == "JumpBoost") {
            other.gameObject.SetActive(false);
            StartCoroutine(JumpBoost());
        }   

        if(other.gameObject.tag == "SpeedBoost") {
            other.gameObject.SetActive(false);
            StartCoroutine(SpeedBoost());
        } 

        if(other.gameObject.tag == "SlowFall") {
            other.gameObject.SetActive(false);
            StartCoroutine(slowFall());
        } 

        if(other.gameObject.tag == "Invincibility") {
            other.gameObject.SetActive(false);
            StartCoroutine(Invincibility());
        }

        if(other.gameObject.tag == "Slow") {
            other.gameObject.SetActive(false);
            StartCoroutine(Slow());
        } 
    }

    IEnumerator JumpBoost() {
        player.jumpSpeed = 8f;
        rend.material.color = jumpColor;
        colorQueue.Enqueue(jumpColor);
        yield return new WaitForSeconds(powerupTime);
        player.jumpSpeed = PlayerController.defaultJumpSpeed;
        resetPlayer();
    }

    IEnumerator SpeedBoost() {
        player.moveSpeed = 8f;
        rend.material.color = speedColor;
        colorQueue.Enqueue(speedColor);
        yield return new WaitForSeconds(powerupTime);
        player.moveSpeed = PlayerController.defaultMoveSpeed;
        resetPlayer();
    }

    IEnumerator slowFall() {
        rend.material.color = slowFallColor;
        colorQueue.Enqueue(slowFallColor);
        for(float t = powerupTime; t > 0; t-=Time.deltaTime) {
            if (GetComponent<Rigidbody2D>().velocity.y < -0.1)
            {
                GetComponent<Rigidbody2D>().gravityScale = 0.1f;
            }
            yield return null;
        }
        GetComponent<Rigidbody2D>().gravityScale = 1f;
        resetPlayer();
    }

    IEnumerator Invincibility() {
        GetComponent<PlayerDeath>().invincible = true;
        rend.material.color = invincibilityColor;
        colorQueue.Enqueue(invincibilityColor);
        yield return new WaitForSeconds(powerupTime);
        GetComponent<PlayerDeath>().invincible = false;
        resetPlayer();
    }

    IEnumerator Slow() {
        player.moveSpeed = 2f;
        player.jumpSpeed = 2f;
        rend.material.color = slowColor;
        colorQueue.Enqueue(slowColor);
        yield return new WaitForSeconds(powerupTime);
        player.moveSpeed = PlayerController.defaultMoveSpeed;
        player.jumpSpeed = PlayerController.defaultJumpSpeed;
        resetPlayer();
    }
}
