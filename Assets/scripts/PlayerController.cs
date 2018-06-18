﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour {

    public MousePos mouse;
    public GameManager manager;

    //How much heigher to place objects.    
    public float heightOffset = 0.5f;

    public float speed = 5f;
    public float rotSpeed = 5f;

    //If this is used, anything in the cameraLocations List will be overridden.
    public GameObject cameraLocOverride;

    //Arbitrary locations for the camera to go to.
    public List<Transform> cameraLocations;
    public int cameraLocIdx = 0;

    public Transform currentLoc { get { return cameraLocations[cameraLocIdx]; } }

    /* TAP LOGIC
     * ON TAP:
     *  IF TAP IS OVER UI DO THE UI DO NOT DO ANYTHING ELSE
     * 
     *  Check if double tap timer is counting down
     *  IF IT IS DO THE DOUBLE TAP
     *  IF NOT START THE TIMER
     *  
     *  If tap is swipe, check if it has moved appropriate distance
     *  IF SO MOVE CAMERA
     *  
     * 
     */ 

    //Tap stuff
    float doubleTapTimer = 0.0f;
    public float doubleTapTimeout = 0.3f;

    float holdTimer = 0f;
    public float holdTimeMax = 1f;

    bool canMove = true;
    public float moveThreshold = 50f;

    bool placeObject = true;

    //Shortcut
    Touch mainTouch { get { return Input.GetTouch(0); } }

    private void Awake()
    {
        //make sure cameraLocations is populated
        if (cameraLocations.Count <= 0)
        {
            cameraLocations.Add(transform);
        }

        //If there's a Camera Locations Override, use its children to populate the camera locations.
        if(cameraLocOverride)
        {
            cameraLocations.Clear();

            for(int i = 0; i < cameraLocOverride.transform.childCount; i++)
            {
                cameraLocations.Add(cameraLocOverride.transform.GetChild(i));
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!manager)
            manager = GameManager.Instance;


        /*******************
          Standalone stuff
        *******************/
//#if UNITY_STANDALONE
#if UNITY_EDITOR

        //click - buggy on android?
        if (Input.GetMouseButtonUp(0))
        {
            PlaceCheck();
        }
        //If we right-click a bomb, delete it.
        if(Input.GetMouseButtonUp(1))
        {
            RemoveObject();
        }
#endif

        /****************
          Android stuff
        ****************/
#if UNITY_ANDROID

        //IF THERE IS A TOUCH...
        if(Input.touchCount > 0)
        {

            switch(mainTouch.phase)
            {
                case TouchPhase.Began:
                    //If the timer is going, remove object.
                    if (doubleTapTimer > 0)
                    {
                        RemoveObject();
                        placeObject = false;
                    }
                    //Otherwise, restart the timer.
                    else
                    {
                        doubleTapTimer = doubleTapTimeout;
                        placeObject = true;
                    }

                    break;
                case TouchPhase.Stationary:
                    //If the touch is held, add to hold timer.
                    holdTimer += Time.deltaTime;

                    //If the hold time is done...
                    if(holdTimer >= holdTimeMax)
                    {
                        EditObject();
                        holdTimer = 0;
                    }

                    break;
                case TouchPhase.Moved:
                    //if we moved, see if we moved ENOUGH.
                    if(Mathf.Abs(mainTouch.deltaPosition.x) >= moveThreshold && canMove)
                    {
                        //If we did, move the camera
                        MoveCamera(mainTouch.deltaPosition.x > 0);

                        //Don't place object if we move the camera
                        placeObject = false;
                        //Also, make sure we can't move anymore
                        canMove = false;
                    }
                    //Debug.Log(mainTouch.deltaPosition.x);

                    break;
                case TouchPhase.Ended:
                    //reset hold timer
                    holdTimer = 0;
                    canMove = true;

                    break;
                default:
                    break;
            }
        }

        //Decrease timer if it is going.
        if (doubleTapTimer > 0)
        {
            doubleTapTimer -= Time.deltaTime;
            //if the timer is NOW done and there are no touches AND we can place an object, place bomb
            if (doubleTapTimer <= 0 && Input.touchCount <= 0 && placeObject)
            {
                PlaceCheck();
            }
        }
#endif

        transform.position = Vector3.Lerp(transform.position, currentLoc.position, 0.3f);
        transform.rotation = Quaternion.Lerp(transform.rotation, currentLoc.rotation, 0.2f);

        //NO ACTUAL CAMERA MOVE.

        //If middle mouse is held, WASD rotates.
        //if(Input.GetMouseButtonDown(2))
        //{
        //    float pitch = Input.GetAxis("Vertical") * Time.deltaTime * rotSpeed;
        //    float yaw = Input.GetAxis("Horizontal") * Time.deltaTime * rotSpeed;

        //    transform.Rotate(new Vector3(pitch, yaw, 0));
        //}
        //else
        //{
        //    transform.Translate(transform.forward * Input.GetAxis("Vertical") * Time.deltaTime * speed);
        //    transform.Translate(transform.right * Input.GetAxis("Horizontal") * Time.deltaTime * speed);
        //}

        //float vertical = Input.GetAxis("UpDown") * Time.deltaTime * speed;
        //transform.Translate(0, vertical, 0, Space.World);

        //IF THERE IS A POKE.
        //if(Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)
        //{
        //    tapCount++;

        //    if(tapCount > 0)
        //    {
        //        doubleTapTimer = doubleTapTimeout;
        //    }
        //    if(tapCount >= 2)
        //    {

        //    }
        //}

    }

    protected void PlaceCheck()
    {
        if (mouse.objHit && !EventSystem.current.IsPointerOverGameObject())
        {
            //If we clicked the floor, place a bomb.
            if (mouse.objHit.layer == 8)
            { PlaceObject(); }
        }
    }
    public void PlaceObject()
    {
        ObjLevelData data = manager.levelManager.placeableObjects[manager.thingIdx];
        if (data.currentUsed < data.maxUsed)
        {
            //increment number of things used;
            data.currentUsed++;
            Transform place = mouse.indicator.gameObject.transform;
            //+ new Vector3(0,2,0)
            GameObject thing = Instantiate(manager.thingToPlace, place.position + new Vector3(0,heightOffset + manager.thingToPlace.GetComponent<Renderer>().bounds.size.y), new Quaternion());
            thing.GetComponent<Explodable>().data = data;
            thing.GetComponent<Explodable>().removable = true;
            Debug.Log("spawned " + manager.thingToPlace.name);
        }
        else Debug.Log("Not enough" + manager.thingToPlace.name);
    }

    public void EditObject()
    {
        //If we hit a bomb that we placed, edit it.
        if (mouse.objHit && !EventSystem.current.IsPointerOverGameObject()
            && mouse.objHit.GetComponent<Explodable>() && mouse.objHit.GetComponent<Explodable>().removable)
        {
            Debug.Log("Editing" + mouse.objHit.name);
            //edit stuff
        }
    }

    public void RemoveObject()
    {
        if (mouse.objHit && !EventSystem.current.IsPointerOverGameObject())
        {
            //If we clicked a bomb, delete it.
            if (mouse.objHit.GetComponent<Explodable>() && mouse.objHit.GetComponent<Explodable>().removable)
            {
                Debug.Log("Destroyed " + mouse.objHit.name);
                Destroy(mouse.objHit);
            }
        }
    }

    public void MoveCamera(bool isLeft)
    {
        if (isLeft)
        {
            if (cameraLocIdx == 0)
                cameraLocIdx = cameraLocations.Count - 1;
            else
                cameraLocIdx--;
            Debug.Log("Moved camera left.");
        }
        else
        {
            if (cameraLocIdx + 1 < cameraLocations.Count)
                cameraLocIdx++;
            else cameraLocIdx = 0;
            Debug.Log("Moved camera right.");
        }
    }

}
