using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class explosionButton : MonoBehaviour {

    public Text eText;

    public LevelManager lm;
    bool isExploding = false;

	public void ExplodeEverything()
    {
        if (lm.isPaused)
            isExploding = true;

        foreach (GameObject e in GameObject.FindGameObjectsWithTag("StartExplode"))
            e.GetComponent<Explosive>().Explode(true);
    }

    public void Update()
    {
        //Set lm if not set.
        if (!lm)
            lm = GameManager.Instance.levelManager;

        if (lm.isPaused && isExploding)
        {
            eText.text = "EXPLODING";
        }
        else if(!lm.isPaused && isExploding)
        {
            eText.text = "EXPLODE";
            isExploding = false;
        }
    }
}
