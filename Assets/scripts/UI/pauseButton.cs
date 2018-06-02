using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pauseButton : MonoBehaviour {

    public Text pauseText;
    bool localPause = false;

    public LevelManager lm;

    public void Toggle()
    {
        //Switch it around.
        lm.isPaused = !lm.isPaused;
    }

    public void Update()
    {

        if (!lm)
            lm = GameManager.Instance.levelManager;

        //If we just paused.
        if(lm.isPaused && !localPause)
        {
            pauseText.text = "UNPAUSE";
            localPause = true;
        }
        else if(!lm.isPaused && localPause)
        {
            pauseText.text = "PAUSE";
            localPause = false;
        }

    }
}
