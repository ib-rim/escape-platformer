using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCollisions : MonoBehaviour
{
    // Code for all of the Player's non-fatal collisions 
    
    private static int collectiblesCounter = 0;
    private static int collectiblesTotal;
    public Text collectiblesText;
    
    public Rigidbody2D rb;
    private static float bounceSpeed = 15.0f;
    
    public float pitfallDelayTime = 1.5f; 

    public Text winText;

    private void Start()
    {
        collectiblesTotal = GameObject.Find("Collectibles").transform.childCount;
        collectiblesText.text = $"Collectibles: {collectiblesCounter.ToString()} / {collectiblesTotal}";
        winText.text = "";
    }


    IEnumerator PitfallDelay(GameObject pitfall) {
        yield return new WaitForSeconds(pitfallDelayTime);
        pitfall.SetActive(false);
        
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Pitfall"))
        {
            StartCoroutine(PitfallDelay(collision.gameObject));
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("BouncyPlatform")) //if player is on bouncy platform
        {
            rb.velocity = new Vector2(rb.velocity.x, bounceSpeed);
        }
        
        if (other.gameObject.tag == "Checkpoint")  //if player collides with checkpoint
        {   
            LevelManager.instance.setRespawnPoint(other.gameObject.transform.position); //set position of respawn to position of checkpoint.

            other.gameObject.transform.Find("CheckpointMiddle").GetComponent<SpriteRenderer>().material.color = Color.cyan; //change colour of checkpoint to show that player has gone through
        }

        if (other.gameObject.CompareTag("Collectible")) //if player collides with collectible.
        {
            other.gameObject.SetActive(false); //hide collectible.
            collectiblesCounter += 1; //increment number of collectibles by 1.
            collectiblesText.text = $"Collectibles: {collectiblesCounter.ToString()} / {collectiblesTotal}";

        }

        if (other.gameObject.CompareTag("TargetPoint")) //if player reaches the target point
        {
            winText.text = "LEVEL COMPLETE"; //display text Level Complete 

            LevelManager.instance.setRespawnPoint(other.gameObject.transform.position); //set respawn position to target point

            other.gameObject.transform.Find("TargetpointMiddle").GetComponent<SpriteRenderer>().material.color = Color.cyan; //change colour of target point to show player has passed through

        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("TwoWayPlatform")) //if player collides with two way platform
        {   
            if(PlayerController.moveValue.y < 0) { //if player is below two way platform
                //player can move through platform to get on top of it.
                other.gameObject.GetComponent<BoxCollider2D>().enabled = false; 
            }

            if(PlayerController.moveValue.y > 0) { //if player is above moving platform
                //player will stand on platform
                other.gameObject.GetComponent<BoxCollider2D>().enabled = true;
            }
        }
    }
}
