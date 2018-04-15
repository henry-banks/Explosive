using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class uiPlacement : MonoBehaviour {

    public MousePos mouse;
    public GameManager manager;

	public void PlaceObject()
    {
        Transform place = mouse.indicator.gameObject.transform;
        //+ new Vector3(0,2,0)
        Instantiate(manager.thingToPlace, place.position , new Quaternion());
        Debug.Log("spawned " + manager.thingToPlace.name);
    }
}
