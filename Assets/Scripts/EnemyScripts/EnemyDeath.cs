using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
     private void OnCollisionStay2D(Collision2D other) 
     {
          if (other.gameObject.CompareTag("FallThreshold")) //if enemy touches lava
            {
                Destroy(gameObject); //kill enemy
            }
            
            if (other.gameObject.CompareTag("Spikes")) //if enemy touches spikes
            {   
                Destroy(gameObject); //kill enemy
            }
     }
}
