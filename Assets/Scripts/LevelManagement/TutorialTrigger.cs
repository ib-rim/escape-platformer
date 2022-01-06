using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    public GameObject keys;

    //Show tutorial keys when in certain area
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            keys.SetActive(true);
        }
    }

    //Hide tutorial keys when not in area
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            keys.SetActive(false);
        }
    }
}
