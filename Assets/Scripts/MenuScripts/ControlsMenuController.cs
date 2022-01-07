using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class ControlsMenuController : MonoBehaviour
{
    public GameObject mainMenu;

    public InputAction backAction;

    private void Start()
    {   
        //Allow back to be performed with input selected in editor
        backAction.performed += _ => back();
    }

    private void OnEnable() {
        backAction.Enable();
    }

    private void OnDisable() {
        backAction.Disable();
    }

    public void back()
    {
        //To main menu
        this.gameObject.SetActive(false);
        mainMenu.SetActive(true);

        //Select first button on main menu for keyboard navigation
        mainMenu.GetComponentInChildren<Button>().Select();
    }
}
