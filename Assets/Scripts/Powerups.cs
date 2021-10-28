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
    Color slowColor = new Color32(215, 190, 137, 255);
    Color invincibilityColor = new Color32(236, 225, 0, 255);

    public float powerupTime = 3;

    void Start() {
        rend = GetComponent<SpriteRenderer>();
        player = GetComponent<PlayerController>();
    }

    void OnTriggerEnter2D(Collider2D other) {

        if(other.gameObject.tag == "JumpBoost") {
            other.gameObject.SetActive(false);
            StartCoroutine(boostJump());
        }   

        if(other.gameObject.tag == "SpeedBoost") {
            other.gameObject.SetActive(false);
            StartCoroutine(boostSpeed());
        } 

        if(other.gameObject.tag == "Slow") {
            other.gameObject.SetActive(false);
            StartCoroutine(slow());
        } 

        if(other.gameObject.tag == "Invincibility") {
            other.gameObject.SetActive(false);
            StartCoroutine(setInvincible());
        } 
    }

    IEnumerator boostJump() {
        player.jumpSpeed = 8f;
        rend.material.color = jumpColor;
        yield return new WaitForSeconds(powerupTime);
        player.jumpSpeed = PlayerController.defaultJumpSpeed;
        rend.material.color = playerColor;
    }

    IEnumerator boostSpeed() {
        player.moveSpeed = 8f;
        rend.material.color = speedColor;
        yield return new WaitForSeconds(powerupTime);
        player.moveSpeed = PlayerController.defaultMoveSpeed;
        rend.material.color = playerColor;
    }

    IEnumerator slow() {
        player.moveSpeed = 2f;
        player.jumpSpeed = 2f;
        rend.material.color = slowColor;
        yield return new WaitForSeconds(powerupTime);
        player.moveSpeed = PlayerController.defaultMoveSpeed;
        player.jumpSpeed = PlayerController.defaultJumpSpeed;
        rend.material.color = playerColor;
    }

    IEnumerator setInvincible() {
        rend.material.color = invincibilityColor;
        GetComponent<PlayerDeath>().invincible = true;
        yield return new WaitForSeconds(powerupTime);
        GetComponent<PlayerDeath>().invincible = false;
        rend.material.color = playerColor;
    }
}
