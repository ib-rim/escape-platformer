using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
    // Code for all of the Player's non-fatal collisions 
    //private GameObject target=null;
    //private Vector3 offset;

    public int pitfallLag = 3; 

    /*
    void start()
    {
        target = null;
    }*/

    IEnumerator PitfallLag() {
        yield return new WaitForSecondsRealtime(pitfallLag);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Pitfall")
        {
            StartCoroutine("PitfallLag");
            collision.gameObject.SetActive(false);
        }
    }

/*
    void onTriggerStay2D(Collider2D other) 
    {
        target = other.gameObject;
        offset = target.transform.position - transform.position;

    }

    void onTriggerExit2D(Collider2D other) 
    {
        target = null;
    }

    void LateUpdate(){
     if (target != null) {
         target.transform.position = transform.position+offset;
     }
    }*/
}
