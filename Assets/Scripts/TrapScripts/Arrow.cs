using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float speed = 20f;
    public int direction = -1;
    public Rigidbody2D rb;

    public Sprite leftArrow;
    public Sprite rightArrow;

    void Start()
    {
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

    IEnumerator lifetime()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }

}
