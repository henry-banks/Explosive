using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Camera))]

public class FloatCam : MonoBehaviour {

    Camera cam;

	// Use this for initialization
	void Start () {
        cam = GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
		//if(Input.GetKey(KeyCode.E))
  //      {
  //          foreach (GameObject e in GameObject.FindGameObjectsWithTag("StartExplode"))
  //              e.GetComponent<Explosive>().Explode();
  //      }
	}
}
