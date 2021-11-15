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
    

    private void Awake()
    {
        collectiblesTotal = GameObject.Find("Collectibles").transform.childCount;
        //collectiblesText.text = $"Collectibles: {collectiblesCounter.ToString()} / {collectiblesTotal}";
        setCollectiblesText();
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

        /*
        if (collision.gameObject.CompareTag("TriggerArrowsTrap")) 
        {
            Debug.Log("Shooting arrows");
            TrapTriggered.instance.shootArrows();
        }*/
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("BouncyPlatform"))
        {
            rb.velocity = new Vector2(rb.velocity.x, bounceSpeed);
        }
        
        if (other.gameObject.tag == "Checkpoint") 
        {   
            LevelManager.instance.setRespawnPoint(other.gameObject.transform.position);
            other.gameObject.transform.Find("CheckpointMiddle").GetComponent<SpriteRenderer>().material.color = Color.cyan;
        }

        if (other.gameObject.CompareTag("Collectible"))
        {
            other.gameObject.SetActive(false);
            //collectiblesCounter += 1;
            setCollectiblesCounter(collectiblesCounter+1);
            setCollectiblesText();
            //collectiblesText.text = $"Collectibles: {collectiblesCounter.ToString()} / {collectiblesTotal}";
        }

        if (other.gameObject.CompareTag("TargetPoint"))
        {
            winText.text = "LEVEL COMPLETE";
            LevelManager.instance.setRespawnPoint(other.gameObject.transform.position);
            other.gameObject.transform.Find("TargetpointMiddle").GetComponent<SpriteRenderer>().material.color = Color.cyan;
        }

        
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("TwoWayPlatform"))
        {   
            if(PlayerController.moveValue.y < 0) {
                other.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            }

            if(PlayerController.moveValue.y > 0) {
                other.gameObject.GetComponent<BoxCollider2D>().enabled = true;
            }
        }
    }

    public void setCollectiblesCounter(int collectibles) {
        collectiblesCounter = collectibles;
    }

    public void setCollectiblesText() {
        collectiblesText.text = $"Collectibles: {collectiblesCounter.ToString()} / {collectiblesTotal}";
    }

    public void triggerArrows() {

    }
}
