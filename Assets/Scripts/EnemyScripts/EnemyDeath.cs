using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{   
    //Destroy enemy on collision with obstacles
     private void OnCollisionStay2D(Collision2D other) 
     {
          if (other.gameObject.CompareTag("FallThreshold"))
            {
                Destroy(gameObject);
            }
            
            if (other.gameObject.CompareTag("Spikes"))
            {   
                Destroy(gameObject);
            }
     }
}
