using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class NPCController : MonoBehaviour
{
    public Line[] dialogue;
    public int index = 0;

    public PlayerInput playerInput;
    public InputActionAsset actions;

    public GameObject dialoguePanel;
    public GameObject character;
    public GameObject text;
    public GameObject talkKey;

    public bool canTalk;
    public bool talking;

    private void Awake() {
        actions = playerInput.actions;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canTalk = true;
            talkKey.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canTalk = false;
            talkKey.SetActive(false);
        }
    }

    public void talk(InputAction.CallbackContext context)
    {   
        if(context.started && canTalk) {
            
            if(!talking) {
                Time.timeScale = 0f;
                actions.FindActionMap("Player").FindAction("Move").Disable();
                actions.FindActionMap("Player").FindAction("Jump").Disable();
                actions.FindActionMap("Player").FindAction("Crouch").Disable();
                actions.FindActionMap("Player").FindAction("Pause").Disable();

                dialoguePanel.SetActive(true);
                talking = true;
            }

            if (talking) {
                if(index >= dialogue.Length) {
                    dialoguePanel.SetActive(false);
                    actions.FindActionMap("Player").FindAction("Move").Enable();
                    actions.FindActionMap("Player").FindAction("Jump").Enable();
                    actions.FindActionMap("Player").FindAction("Crouch").Enable();
                    actions.FindActionMap("Player").FindAction("Pause").Enable();

                    Time.timeScale = 1f;
                    talking = false;
                    index = 0;
                }
                else {
                    character.GetComponent<Image>().sprite = dialogue[index].character;
                    text.GetComponent<TMPro.TextMeshProUGUI>().text = dialogue[index].text;
                    index++;
                }
            }
        }
    }

    private void Update() {
        
        //Show interact key if in range of NPC
        // if(canTalk) {
            
        // }
    }
}
