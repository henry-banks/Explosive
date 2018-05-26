using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosionButton : MonoBehaviour {

	public void ExplodeEverything()
    {
        foreach (GameObject e in GameObject.FindGameObjectsWithTag("StartExplode"))
            e.GetComponent<Explosive>().Explode(true);
    }
}
