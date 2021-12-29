using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    public Sprite firstSprite;
    public Sprite secondSprite;
    public Sprite thirddSprite;

    private void Update() {
        int num = Random.Range(1,400);

        if(num == 2) {
            GetComponent<SpriteRenderer>().sprite = secondSprite;
            StartCoroutine("switchToFirst");
        }
        else if(num == 3) {
            GetComponent<SpriteRenderer>().sprite = thirddSprite;
            StartCoroutine("switchToFirst");
        }
    }

    IEnumerator switchToFirst()
    {
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().sprite = firstSprite;
    }
}
