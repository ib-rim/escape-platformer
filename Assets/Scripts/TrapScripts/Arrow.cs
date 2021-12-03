using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float speed = 20f;
    public int direction = -1;
    public Rigidbody2D rb;

    void Start()
    {
        rb.velocity = direction * transform.right * speed;
        StartCoroutine(lifetime());
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }

    IEnumerator lifetime()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {   
        if(!collision.gameObject.CompareTag("Player")) {
            Destroy(gameObject);   
        }
    }
    
}
