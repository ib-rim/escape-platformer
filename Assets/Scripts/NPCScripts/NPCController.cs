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

    //Can only talk to NPC when in range
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canTalk = true;
            talkKey.SetActive(true);
        }
    }

    //Can not talk to NPC when not in range
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
            
            //On first input
            if(!talking) {
                Time.timeScale = 0f;
                actions.FindActionMap("Player").FindAction("Move").Disable();
                actions.FindActionMap("Player").FindAction("Jump").Disable();
                actions.FindActionMap("Player").FindAction("Crouch").Disable();
                actions.FindActionMap("Player").FindAction("Pause").Disable();

                dialoguePanel.SetActive(true);
                talking = true;
            }

            //On second input onwards
            if (talking) {  
                //End dialogue if no more lines remaining
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
                else { //Continue dialogue
                    character.GetComponent<Image>().sprite = dialogue[index].character;
                    text.GetComponent<TMPro.TextMeshProUGUI>().text = dialogue[index].text;
                    index++;
                }
            }
        }
    }
}
