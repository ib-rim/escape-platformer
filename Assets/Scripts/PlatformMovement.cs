using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    private Vector3 toPos;
    private Vector3 fromPos;
    private Vector3 rightBound;
    private Vector3 leftBound;
    public float distance = 5.0f;
    public float duration = 3.0f;
    private float elapsed = 0.0f; // used to track the progress of our movement
    public bool moveRightFirst = false; // choose initial direction in Inspector

    private void Start()
    {
        print("moveRightFirst " + moveRightFirst);
        if (moveRightFirst)
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

    void Update()
    {
        float frac = elapsed / duration; // track progress of movement
        transform.position = Vector3.Lerp(fromPos, toPos, frac);
        elapsed += Time.deltaTime; // progress should be smooth

        if (frac >= 1.0f)
        {
            if (toPos == rightBound) // once right bound is reached
            {
                elapsed = 0.0f; // reset progress
                toPos = leftBound; // switch directions
                fromPos = rightBound;
            }
            else if (toPos == leftBound) // once left bound is reached
            {
                elapsed = 0.0f; // reset progress
                toPos = rightBound; // switch directions
                fromPos = leftBound;
            }
        }
    }

}
