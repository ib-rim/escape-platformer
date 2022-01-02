using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.EventSystems;

public class LevelButton : MonoBehaviour, ISelectHandler
{   
    public LevelSelectController levelSelectController;

    public String level;

    //For keyboard navigation of level select
    public void OnSelect(BaseEventData eventData)
    {   
        int previousLevelNum = Int32.Parse(Regex.Match(level, @"\d+").Value) - 1; 
        if (previousLevelNum == 0) {
            previousLevelNum = 1;
        }
        if(PlayerPrefs.GetString($"Level{previousLevelNum}") == "complete") {
            levelSelectController.showLevel(level);
        }
    }
}
