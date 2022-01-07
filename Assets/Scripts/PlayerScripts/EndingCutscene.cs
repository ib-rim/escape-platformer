using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class EndingCutscene : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed;

    public Animator player_animator;
    public Animator credits_animator;

    public Line[] dialogue;
    public int index = 0;

    public PlayerInput playerInput;

    public Sprite defaultSprite;
    public GameObject fadeImage;
    public GameObject credits;

    public GameObject dialoguePanel;
    public GameObject character;
    public GameObject text;

    private int collectiblesTotal; 

    void Start()
    {   
        //Change dialogue depending on how many collectibles collected
        collectiblesTotal = PlayerPrefs.GetInt("Level1Collectibles") + PlayerPrefs.GetInt("Level2Collectibles") + PlayerPrefs.GetInt("Level3Collectibles") + PlayerPrefs.GetInt("Level4Collectibles");
        dialogue[5].text = $"{collectiblesTotal} chests worth of gold.";
        if(collectiblesTotal >= 20) {
            dialogue[6].text = "Wow.";
            dialogue[7].text = "I don't need my sisters anymore. I can make my own guild.";
        }
        else if(collectiblesTotal >= 10) {
            dialogue[6].text = "I've got enough to prove I passed the trials.";
            dialogue[7].text = "And hopefully it's enough to earn my sisters' respect.";
        }
        else if(collectiblesTotal >= 1) {
            dialogue[6].text = "Not much, I missed quite a lot.";
            dialogue[7].text = "This might not be enough to prove I passed the trial, but I'm just grateful to be free.";
        }
        else if(collectiblesTotal == 0) {
            dialogue[6].text = "This is impossible. There were some chests I literally could not avoid.";
            dialogue[7].text = "I must have had some divine help with this misfortune.";
        }
        moveSpeed = 6f;
        AudioManager.instance.PlaySFX("Footsteps");
        StartCoroutine("StartCutscene");
    }

    public void anyInput(InputAction.CallbackContext context) {
        if(context.started && dialoguePanel.activeInHierarchy) {
            //End dialogue if no more lines
            if(index >= dialogue.Length) {
                dialoguePanel.SetActive(false);
                StartCoroutine("EndCutscene");
            }
            else {
                //Continue dialogue
                character.GetComponent<Image>().sprite = dialogue[index].character;
                text.GetComponent<TMPro.TextMeshProUGUI>().text = dialogue[index].text;
                index++;
            }
        }

        if(context.started && credits.activeInHierarchy) {
            //To main menu
            SceneManager.LoadScene("MainMenu");
        }
    }

    IEnumerator StartCutscene()
    {   
        //Move player forwards into position
        player_animator.Play("move");
        rb.velocity = new Vector2(moveSpeed, rb.velocity.y);

        yield return new WaitForSeconds(2);

        //Stop player and start dialogue
        rb.velocity = new Vector2(0 * moveSpeed, rb.velocity.y);
        player_animator.enabled = false;
        GetComponent<SpriteRenderer>().sprite = defaultSprite;
        dialoguePanel.SetActive(true);
    }

    IEnumerator EndCutscene()
    {   
        //Move player forwards off screen
        player_animator.enabled = true;
        player_animator.Play("move");
        rb.velocity = new Vector2(moveSpeed, rb.velocity.y);

        //Start fade to black
        fadeImage.GetComponent<Animator>().Play("FadeIn");

        yield return new WaitForSeconds(4);
        
        //Stop player and fade animation
        fadeImage.GetComponent<Animator>().enabled = false;
        rb.velocity = new Vector2(0 * moveSpeed, rb.velocity.y);
        player_animator.enabled = false;
        GetComponent<SpriteRenderer>().sprite = defaultSprite;

        //Roll credits
        credits.SetActive(true);
        credits_animator.Play("PlayCredits");
    }
}
