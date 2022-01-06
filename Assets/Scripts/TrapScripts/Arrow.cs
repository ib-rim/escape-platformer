using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Arrow : MonoBehaviour
{
    public float speed = 20f;
    public int direction = -1;
    public Rigidbody2D rb;

    public Sprite leftArrow;
    public Sprite rightArrow;

    void Start()
    {   
        if(SceneManager.GetActiveScene().name.Contains("Easy")) {
            speed /= 2;
        }

        //Change sprite depending on direction
        if (direction == 1)
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = rightArrow;
        }
        else
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = leftArrow;
        }
        rb.velocity = direction * transform.right * speed;
        StartCoroutine(lifetime());
    }

    //Destroy after travelling for some time
    IEnumerator lifetime()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }

    //Destroy on collision with anything
    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }

}
