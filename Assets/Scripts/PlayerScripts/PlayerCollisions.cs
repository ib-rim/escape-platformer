using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
    // Code for all of the Player's non-fatal collisions 

    public int pitfallLag = 3; 

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
<<<<<<< HEAD

   

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
=======
>>>>>>> parent of 7ab57d2 (static platforms)
}
