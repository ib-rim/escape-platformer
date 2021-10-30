using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
    // Code for all of the Player's non-fatal collisions 
    
    public int pitfallLag = 3; 

        yield return new WaitForSecondsRealtime(pitfallLag);
    IEnumerator PitfallDelay(GameObject pitfall) {
        pitfall.SetActive(false);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Pitfall")
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
