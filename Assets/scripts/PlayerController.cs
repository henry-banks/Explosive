using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour {

    public MousePos mouse;
    public GameManager manager;

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        //click - buggy on android?
        if(Input.GetMouseButtonUp(0))
        {
            if(mouse.objHit && !EventSystem.current.IsPointerOverGameObject())
            {
                //If we clicked the floor, place a bomb.
                if(mouse.objHit.layer == 8)
                { PlaceObject(); }

                //If we clicked a bomb, delete it.
                else if(mouse.objHit.GetComponent<Explodable>())
                {
                    Destroy(mouse.objHit);
                }
            }
        }
    }

    public void PlaceObject()
    {
        Transform place = mouse.indicator.gameObject.transform;
        //+ new Vector3(0,2,0)
        Instantiate(manager.thingToPlace, place.position, new Quaternion());
        Debug.Log("spawned " + manager.thingToPlace.name);
    }

}
