using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
    // Code for all of the Player's non-fatal collisions 
    
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
        if(other.gameObject.tag == "Checkpoint") 
        {   
            LevelManager.instance.setRespawnPoint(other.gameObject.transform);

            other.gameObject.transform.Find("CheckpointMiddle").GetComponent<SpriteRenderer>().material.color = Color.cyan;
        }
    }
}
