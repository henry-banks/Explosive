using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackOverlaps : MonoBehaviour {

    public List<GameObject> currentCollisions = new List<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        currentCollisions.Add(other.gameObject);
    }
    private void OnTriggerExit(Collider other)
    {
        currentCollisions.Remove(other.gameObject);
    }


}
