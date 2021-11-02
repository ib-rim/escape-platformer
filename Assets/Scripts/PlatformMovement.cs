using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    private Vector3 startPos;
    private Vector3 toPos;
    private Vector3 fromPos;
    private Vector3 rightBound;
    private Vector3 leftBound;
    private float distance = 5.0f; // distance
    private float duration = 3.0f; // how long we want the movement to take
    private float elapsed = 0.0f; // used to track the progress of our movement
    //public bool moveRightFirst = false;

    private void Start()
    {
        rightBound = transform.position;
        fromPos = rightBound;

        leftBound = new Vector3(transform.position.x - distance, transform.position.y, 0);
        toPos = leftBound;
    }

    void Update()
    {
        float frac = elapsed / duration; // track progress of movement
        transform.position = Vector3.Lerp(fromPos, toPos, frac);
        elapsed += Time.deltaTime; // progress should be smooth
        print(frac);

        if (frac >= 1.0f)
        {
            if (toPos == rightBound) // once right bound is reached
            {
                print("switch to the right");
                elapsed = 0.0f; // reset progress
                toPos = leftBound; // switch directions
                fromPos = rightBound;
            }
            else if (toPos == leftBound) // once left bound is reached
            {
                print("switch to the left");
                elapsed = 0.0f; // reset progress
                toPos = rightBound; // switch directions
                fromPos = leftBound;
            }
        }
    }

}
