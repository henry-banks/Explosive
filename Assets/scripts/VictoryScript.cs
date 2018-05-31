﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class VictoryScript : MonoBehaviour {

    public string NextLevelName;
    //How often to check for a victory.  Set to larger values if the check is complex.
    public float CheckDelay = 0f;
    //A delay after victory is met and before the actual level transition in which the Victory effect can be seen;
    public float PostVictoryDelay = 2f;

    //True if win
    public bool isVictory;

    //Make sure effect is only called once.
    private bool effectRun = false;
    
    //A check that, if returned true, will congratulate the player and transition to the next level.
    protected abstract bool VictoryCondition();

    //Effect that is played when the victory condition is met.
    public virtual void VictoryEffect()
    {
        Debug.Log("Victory!");
    }

    public IEnumerator VictoryCheck()
    {
        yield return new WaitForSeconds(CheckDelay);
        //Set isVictory to true ONCE so it isn't accidentally unset.
        if (!isVictory)
        {
            isVictory = VictoryCondition();
        }
        else if(isVictory && !effectRun)
        {
            //Also only call this once.
            effectRun = true;

            VictoryEffect();
            StartCoroutine(DelayedLevelTransition());
        }
    }

    private IEnumerator DelayedLevelTransition()
    {
        yield return new WaitForSeconds(PostVictoryDelay);
        LevelTransition();
    }

    private void LevelTransition()
    {
        SceneManager.LoadScene(NextLevelName);
    }
}
