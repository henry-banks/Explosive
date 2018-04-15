using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class uiBombSelect : MonoBehaviour {

    public GameManager manager;
    public GameObject obj;

	public void onClicked()
    {
        manager.thingToPlace = obj;
    }
}
