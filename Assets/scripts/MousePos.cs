using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePos : MonoBehaviour {

    public Camera cam;
    public GameObject indicator;
    Ray ray;

    RaycastHit hit;
    Transform objHit;
    public Vector3 pos;

	// Update is called once per frame
	void Update () {
        ray = cam.ScreenPointToRay(Input.mousePosition);

        //Debug.Log(LayerMask.LayerToName(8));

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            objHit = hit.transform;
            pos = hit.point;
            //Debug.Log(ray.origin + "\n" + pos);

            //Debug.DrawLine(ray.origin, hit.point);

            indicator.transform.position = new Vector3(pos.x, indicator.transform.position.y, pos.z);
            //indicator.transform.position += new Vector3(0, 1, 0);
            //Debug.Log("raycast success"); 
        }
        //else { Debug.Log("raycast fail"); }
    }
}
