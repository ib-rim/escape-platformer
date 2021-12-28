using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Powerups : MonoBehaviour
{
    PlayerController player;
    SpriteRenderer rend;
    GameObject powerupsText;

    public Font font;

    Color playerColor = new Color32(255, 255, 255, 255);
    Color jumpColor = new Color32(125, 212, 144, 255);
    Color speedColor = new Color32(236, 108, 0, 255);
    Color slowFallColor = new Color32(201, 181, 179, 255);
    Color invincibilityColor = new Color32(236, 225, 0, 255);
    Color slowColor = new Color32(215, 190, 137, 255);

    private float powerupTime = 3;
    private int powerupsCount = 0;
    private Color mostRecentColor;

    void Start()
    {
        powerupsText = GameObject.Find("PowerupTimers");
        rend = GetComponent<SpriteRenderer>();
        player = GetComponent<PlayerController>();
    }

    GameObject newPowerupText(Color powerupColor)
    {

        //Create object for text and make a child of powerupsText
        GameObject powerupText = new GameObject("PowerupText");
        powerupText.transform.SetParent(powerupsText.transform);

        //Create Text component 
        Text text = powerupText.AddComponent<Text>();
        text.font = font;
        text.fontStyle = FontStyle.Bold;
        text.fontSize = 40;
        text.color = powerupColor;
        mostRecentColor = text.color;

        //Add text shadow for readability
        Shadow textShadow = powerupText.AddComponent<Shadow>();
        textShadow.effectColor = new Color32(0, 0, 0, 255);
        textShadow.effectDistance = new Vector2(2, -1);
        textShadow.useGraphicAlpha = true;

        powerupsCount += 1;

        //Set position of text object according to how many powerups are in effect
        if (powerupsCount == 1)
        {
            powerupText.GetComponent<RectTransform>().anchoredPosition = new Vector2(253, 160);
        }
        else
        {
            powerupText.GetComponent<RectTransform>().anchoredPosition = new Vector2(253, 160 - (40 * (powerupsCount - 1)));
        }

        //Set size of text object
        powerupText.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 20);
        powerupText.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 55);

        return powerupText;
    }

    public void changeColor()
    {
        powerupsCount -= 1;
        //Move all children of PowerupsText up by 40
        RectTransform[] rectTransforms = powerupsText.transform.GetComponentsInChildren<RectTransform>();
        for (int i = 1; i < rectTransforms.Length; i++)
        {
            rectTransforms[i].anchoredPosition = new Vector2(253, rectTransforms[i].anchoredPosition.y + 40);
        }

        if (powerupsCount > 0)
        {
            rend.color = mostRecentColor;
        }
        else
        {
            rend.color = playerColor;
            mostRecentColor = playerColor;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.tag == "JumpBoost")
        {
            other.gameObject.SetActive(false);
            StartCoroutine(JumpBoost());
            AudioManager.instance.PlaySFX("Powerup");
        }

        if (other.gameObject.tag == "SpeedBoost")
        {
            other.gameObject.SetActive(false);
            StartCoroutine(SpeedBoost());
            AudioManager.instance.PlaySFX("Powerup");
        }

        if (other.gameObject.tag == "SlowFall")
        {
            other.gameObject.SetActive(false);
            StartCoroutine(slowFall());
            AudioManager.instance.PlaySFX("Powerup");
        }

        if (other.gameObject.tag == "Invincibility")
        {
            other.gameObject.SetActive(false);
            StartCoroutine(Invincibility());
            AudioManager.instance.PlaySFX("Powerup");
        }

        if (other.gameObject.tag == "Slow")
        {
            other.gameObject.SetActive(false);
            StartCoroutine(Slow());
            AudioManager.instance.PlaySFX("Slow");
        }
    }

    IEnumerator JumpBoost()
    {
        //Increase jumpSpeed and player color
        player.jumpSpeed = PlayerController.defaultJumpSpeed * 1.5f;
        rend.color = jumpColor;

        //Change powerup timer text as timer decreases 
        GameObject powerupText = newPowerupText(jumpColor);
        Text text = powerupText.GetComponent<Text>();

        for (float t = powerupTime; t > 0; t -= Time.deltaTime)
        {

            text.text = Mathf.CeilToInt(t).ToString();
            yield return null;
        }

        //Remove text and change player color
        GameObject.Destroy(powerupText);
        changeColor();

        //Only reset if next powerup isn't jump or slow 
        if (mostRecentColor != jumpColor && mostRecentColor != slowColor)
        {
            player.jumpSpeed = PlayerController.defaultJumpSpeed;
        }
    }

    IEnumerator SpeedBoost()
    {
        //Increase moveSpeed and change player color
        player.moveSpeed = PlayerController.defaultMoveSpeed * 2;
        rend.color = speedColor;

        //Change powerup timer text as timer decreases 
        GameObject powerupText = newPowerupText(speedColor);
        Text text = powerupText.GetComponent<Text>();

        for (float t = powerupTime; t > 0; t -= Time.deltaTime)
        {
            text.text = Mathf.CeilToInt(t).ToString();
            yield return null;
        }

        //Remove text and change player color
        GameObject.Destroy(powerupText);
        changeColor();

        //Only reset if next powerup isn't speed or slow or slowFall
        if (mostRecentColor != speedColor && mostRecentColor != slowColor && mostRecentColor != slowFallColor)
        {
            player.moveSpeed = PlayerController.defaultMoveSpeed;
        }
    }

    IEnumerator slowFall()
    {
        //Change player color
        rend.color = slowFallColor;

        //Change powerup timer text as timer decreases 
        GameObject powerupText = newPowerupText(slowFallColor);
        Text text = powerupText.GetComponent<Text>();

        for (float t = powerupTime; t > 0; t -= Time.deltaTime)
        {

            //Decrease gravity when falling
            if (GetComponent<Rigidbody2D>().velocity.y < -0.1)
            {
                GetComponent<Rigidbody2D>().gravityScale = PlayerController.defaultGravity / 10;

            }
            else
            {
                GetComponent<Rigidbody2D>().gravityScale = PlayerController.defaultGravity;
            }

            text.text = Mathf.CeilToInt(t).ToString();
            yield return null;
        }

        //Remove text and change player color
        GameObject.Destroy(powerupText);
        changeColor();

        //Only reset gravity if next powerup isn't slowFall
        if (mostRecentColor != slowFallColor)
        {
            GetComponent<Rigidbody2D>().gravityScale = PlayerController.defaultGravity;
        }

    }

    IEnumerator Invincibility()
    {
        //Make player invincible and change player color
        GetComponent<PlayerDeath>().invincible = true;
        rend.color = invincibilityColor;

        //Change powerup timer text as timer decreases 
        GameObject powerupText = newPowerupText(invincibilityColor);
        Text text = powerupText.GetComponent<Text>();

        for (float t = powerupTime; t > 0; t -= Time.deltaTime)
        {

            text.text = Mathf.CeilToInt(t).ToString();
            yield return null;
        }

        //Remove text, remove invincibility and change player color
        GameObject.Destroy(powerupText);
        changeColor();

        //Only turn off invincibility if next powerup isn't invincibility
        if (mostRecentColor != invincibilityColor)
        {
            GetComponent<PlayerDeath>().invincible = false;
        }
        
    }

    IEnumerator Slow()
    {
        //Make player slow and change player color
        player.moveSpeed = PlayerController.defaultMoveSpeed / 2;
        player.jumpSpeed = PlayerController.defaultJumpSpeed / 2;
        rend.color = slowColor;


        //Change powerup timer text as timer decreases 
        GameObject powerupText = newPowerupText(slowColor);
        Text text = powerupText.GetComponent<Text>();

        for (float t = powerupTime; t > 0; t -= Time.deltaTime)
        {

            text.text = Mathf.CeilToInt(t).ToString();
            yield return null;
        }

        //Remove text and change player color
        GameObject.Destroy(powerupText);
        changeColor();

        //Only reset if next powerup isn't slow or jump or speed
        if (mostRecentColor != slowColor)
        {
            if (mostRecentColor != jumpColor)
            {
                player.jumpSpeed = PlayerController.defaultJumpSpeed;
            }
            if (mostRecentColor != speedColor)
            {
                player.moveSpeed = PlayerController.defaultMoveSpeed;
            }
        }
    }
}
