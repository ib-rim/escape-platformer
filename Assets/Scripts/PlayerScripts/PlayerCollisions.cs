using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
    // Code for all of the Player's non-fatal collisions 

    public Rigidbody2D rb;
    private static float bounceSpeed = 12.0f;
    
    public float pitfallDelayTime = 1.5f; 


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
        if (other.gameObject.CompareTag("BouncyPlatform"))
        {
            rb.velocity = new Vector2(rb.velocity.x, bounceSpeed);
            print("bounce speed"+bounceSpeed);
        }
        if (other.gameObject.tag == "Checkpoint") 
        {   
            LevelManager.instance.setRespawnPoint(other.gameObject.transform);

            other.gameObject.transform.Find("CheckpointMiddle").GetComponent<SpriteRenderer>().material.color = Color.cyan;
        }
    }

    /*private void OnCollisionEnter(Collision collision)
    {
            rb.velocity = new Vector2(rb.velocity.x, PlayerController.defaultJumpSpeed);
    }*/

}
