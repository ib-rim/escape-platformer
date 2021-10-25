using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerups : MonoBehaviour
{   
    PlayerController player; 
    SpriteRenderer rend;
    Color playerColor = new Color(255, 255, 255);
    Color jumpColor = new Color(0, 174, 236);
    Color speedColor = new Color(236, 225, 0);
    Color slowFallColor = new Color(144, 226, 201);
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

        if(other.gameObject.tag == "SlowFall") {
            other.gameObject.SetActive(false);
            StartCoroutine(slowFall());
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

    IEnumerator slowFall() {
        player.moveSpeed = 1f;
        rend.material.color = slowFallColor;
        yield return new WaitForSeconds(powerupTime);
        player.moveSpeed = PlayerController.defaultMoveSpeed;
        rend.material.color = playerColor;
    }
}
