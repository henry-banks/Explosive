using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour {

    public MousePos mouse;
    public GameManager manager;

    //How much heigher to place objects.    
    public float heightOffset = 0.5f;

    public float speed = 5f;
    public float rotSpeed = 5f;

    //Arbitrary locations for the camera to go to.
    public List<Transform> cameraLocations;
    public int cameraLocIdx = 0;

    public Transform currentLoc { get { return cameraLocations[cameraLocIdx]; } }

    //Tap stuff
    private int tapCount = 0;
    private float doubleTapTimer = 0.0f;
    private float doubleTapTimeout = 0.5f;

    private void Awake()
    {
        //make sure cameraLocations is populated
        if (cameraLocations.Count <= 0)
        {
            cameraLocations.Add(transform);
        }
    }

    // Use this for initialization
    void Start () {

        manager = GameManager.Instance;
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
            }
        }
        //If we right-click a bomb, delete it.
        if(Input.GetMouseButtonUp(1))
        {
            if (mouse.objHit && !EventSystem.current.IsPointerOverGameObject())
            {
                //If we clicked a bomb, delete it.
                if (mouse.objHit.GetComponent<Explodable>())
                {
                    Destroy(mouse.objHit);
                }
            }
        }

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
            Debug.Log("spawned " + manager.thingToPlace.name);
        }
        else Debug.Log("Not enough" + manager.thingToPlace.name);
    }

}
