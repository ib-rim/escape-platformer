using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCollectibles : MonoBehaviour {

    private int collectiblesCounter = 0;
    public Text collectiblesText;

    private void Start()
    {
        collectiblesText.text = "Collectibles: " + collectiblesCounter.ToString();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Collectible")
        {
            collision.gameObject.SetActive(false);
            collectiblesCounter += 1;
            collectiblesText.text = "Collectibles: " + collectiblesCounter.ToString();
        }
    }
}
