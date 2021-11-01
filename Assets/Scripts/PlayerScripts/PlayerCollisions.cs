using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCollisions : MonoBehaviour
{
    // Code for all of the Player's non-fatal collisions 
    private static int collectiblesCounter = 0;
    public Text collectiblesText;

    public int pitfallLag = 3; 

    private void Start()
    {
        collectiblesText.text = "Collectibles: " + collectiblesCounter.ToString();
    }

    IEnumerator PitfallLag() {
        yield return new WaitForSecondsRealtime(pitfallLag);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Pitfall"))
        {
            StartCoroutine("PitfallLag");
            collision.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Checkpoint")) 
        {   
            LevelManager.instance.setRespawnPoint(other.gameObject.transform.position);

            other.gameObject.transform.Find("CheckpointMiddle").GetComponent<SpriteRenderer>().material.color = Color.cyan;
        }

        if (other.gameObject.CompareTag("Collectible"))
        {
            other.gameObject.SetActive(false);
            collectiblesCounter += 1;
            collectiblesText.text = "Collectibles: " + collectiblesCounter.ToString();
        }
    }
}
