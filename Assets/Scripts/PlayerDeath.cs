using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour {

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "FallThreshold")
        {
            Destroy(gameObject);
            LevelManager.instance.Respawn();
        }
    }

}
