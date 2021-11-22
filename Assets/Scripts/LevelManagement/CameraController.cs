using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset;

    void Start() {
        offset = new Vector3(7, 1.85f, -14);
    }

    //Follow player at fixed distance
    void LateUpdate() {
        transform.position = player.transform.position + offset;
    }
}
