using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectibles : MonoBehaviour
{
    //Keep same instance of Collectibles object even when scene reloads/player dies
    public static Collectibles instance;

    void Awake() {

        DontDestroyOnLoad(gameObject);

        if(instance == null) {
            instance = this;
        }
        else {
            Destroy(gameObject);
        }
        
    }
}
