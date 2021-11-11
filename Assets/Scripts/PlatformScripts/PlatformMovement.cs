using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    private Vector3 toPos; // used to track position as platform moves
    private Vector3 fromPos;
    private Vector3 rightBound; // bounds are fixed values calculated on Start
    private Vector3 leftBound;
    public float distance = 5.0f;
    public float duration = 3.0f;
    private float elapsed = 0.0f; // used to track the progress of our movement
    public bool moveRightFirst = false; // can choose initial direction in Inspector
    PlayerController player;

    private void Start()
    {
        if (moveRightFirst) // set the inital direction
        {
            leftBound = transform.position;
            fromPos = leftBound;

            rightBound = new Vector3(transform.position.x + distance, transform.position.y, 0);
            toPos = rightBound;
        }
        else 
        {
            rightBound = transform.position;
            fromPos = rightBound;

            leftBound = new Vector3(transform.position.x - distance, transform.position.y, 0);
            toPos = leftBound;
        }
    }

    void Update() // update platform's position using linear interpolation
    {
        float frac = elapsed / duration; // track progress of current lap
        transform.position = Vector3.Lerp(fromPos, toPos, frac);
        elapsed += Time.deltaTime; // ensure the movement is smooth

        if (frac >= 1.0f) // once current lap is complete...
        {
            elapsed = 0.0f; // ...reset progress tracking
            if (toPos == rightBound) // ...and switch directions
            {
                toPos = leftBound;
                fromPos = rightBound;
            }
            else if (toPos == leftBound)
            {
                toPos = rightBound;
                fromPos = leftBound;
            }
        }
    }


    //code to make player move with platform
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            collision.gameObject.transform.parent = transform;
    }

    //code to stop making player move with platform
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            collision.gameObject.transform.parent = null;
    }

}
