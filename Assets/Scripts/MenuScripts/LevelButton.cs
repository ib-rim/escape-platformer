using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LevelButton : MonoBehaviour, ISelectHandler
{   
    public LevelSelectController levelSelectController;

    public String level;

    //For keyboard navigation of level select
    public void OnSelect(BaseEventData eventData)
    {
        if(PlayerPrefs.GetString(level) == "complete") {
            levelSelectController.showLevel(level);
        }
    }
}
