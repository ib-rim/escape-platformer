using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.SceneManagement;

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

    public Sprite litTorch;
    public Sprite emptyChest;

    ParticleSystem pitfallParticles;

    private void Awake()
    {   
        if(SceneManager.GetActiveScene().name.Contains("Easy")) {
            pitfallDelayTime *= 2;
        }
        collectiblesTotal = GameObject.Find("Collectibles").transform.childCount;
        setCollectiblesText();
        winText.text = "";
    }


    IEnumerator PitfallDelay(GameObject pitfall)
    {   
        yield return new WaitForSeconds(pitfallDelayTime);
        pitfallParticles.Clear();
        pitfallParticles.Stop();
        pitfall.SetActive(false);
        AudioManager.instance.PlaySFX("Pitfall");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Pitfall"))
        {   
            pitfallParticles = collision.gameObject.GetComponent<ParticleSystem>();
            pitfallParticles.Play();
            StartCoroutine(PitfallDelay(collision.gameObject));
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("BouncyPlatform"))
        {
            rb.velocity = new Vector2(rb.velocity.x, bounceSpeed);
            AudioManager.instance.PlaySFX("BouncyJump");
        }

        if (other.gameObject.tag == "Checkpoint")
        {   
            if(other.gameObject.GetComponent<SpriteRenderer>().sprite != litTorch) {
                LevelManager.instance.setRespawnPoint(other.gameObject.transform.position);
                other.gameObject.GetComponent<SpriteRenderer>().sprite = litTorch;
                other.gameObject.GetComponent<Light2D>().enabled = true;
                AudioManager.instance.PlaySFX("Checkpoint");
            }
        }

        if (other.gameObject.CompareTag("Collectible"))
        {
            if(other.gameObject.GetComponent<SpriteRenderer>().sprite != emptyChest) {
                other.gameObject.GetComponent<SpriteRenderer>().sprite = emptyChest;
                other.gameObject.GetComponent<Light2D>().enabled = false;
                setCollectiblesCounter(collectiblesCounter + 1);
                setCollectiblesText();
                AudioManager.instance.PlaySFX("Collectible");
            }
        }

        if (other.gameObject.CompareTag("TargetPoint"))
        {
            winText.text = "LEVEL COMPLETE";
            LevelManager.instance.EndLevel();
        }

        if (other.gameObject.CompareTag("TwoWayPlatform"))
        {
            other.gameObject.GetComponent<BoxCollider2D>().enabled = true;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("TwoWayPlatform"))
        {
            //Allow player to press down to move through platform
            if (PlayerController.moveValue.y < 0)
            {
                other.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            }

            //Allow player to land on platform when jumping up
            if (PlayerController.moveValue.y > 0)
            {
                other.gameObject.GetComponent<BoxCollider2D>().enabled = true;
            }
        }
    }

    public int getCollectiblesCounter() {
        return collectiblesCounter;
    }

    public void setCollectiblesCounter(int collectibles)
    {
        collectiblesCounter = collectibles;
    }

    public void setCollectiblesText()
    {
        collectiblesText.text = $"x {collectiblesCounter.ToString()} / {collectiblesTotal}";
    }
}
