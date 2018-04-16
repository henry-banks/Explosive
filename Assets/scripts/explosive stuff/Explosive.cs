using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Explodable))]
[RequireComponent(typeof(SphereCollider))]

public class Explosive : MonoBehaviour {

    //How big the epxlosion is
    public float explosionRadius = 1;
    public float explosionDelay = 0.25f;

    Rigidbody rb;
    SphereCollider exBound;

    List<Explodable> overlappingObjects;
    //Used to prevent infinite loop in Explodable/Explosive explosions
    public bool hasExploded = false;

	// Use this for initialization
	void Start () {
        overlappingObjects = new List<Explodable>();
        rb = GetComponent<Rigidbody>();

        exBound = GetComponent<SphereCollider>();
        exBound.radius = explosionRadius;
        exBound.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {

	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Explodable>())
            if (!overlappingObjects.Contains(other.gameObject.GetComponent<Explodable>()))
            {
                overlappingObjects.Add(other.gameObject.GetComponent<Explodable>());
                Debug.Log("Added " + other.gameObject.name);
            }
    }
    private void OnTriggerExit(Collider other)
    {
        //I don't want to search the entire list every time something exits the explosion.
        if (other.gameObject.GetComponent<Explodable>())
            if (overlappingObjects.Contains(other.gameObject.GetComponent<Explodable>()))
                overlappingObjects.Add(other.gameObject.GetComponent<Explodable>());
    }

    public void Explode()
    {
        exBound.enabled = true;

        Collider[] objs = Physics.OverlapSphere(transform.position, explosionRadius);
        hasExploded = true;
        foreach (Collider c in objs)
            if (c.gameObject.GetComponent<Explodable>() != null)
                c.gameObject.GetComponent<Explodable>().Explode(explosionDelay);

        //foreach (Explodable e in overlappingObjects)
        //    if(e) e.Explode(explosionDelay);

        if (GetComponent<Explodable>() && !GetComponent<Explodable>().hasExploded)
            GetComponent<Explodable>().Explode();
    }

    public void OnDestroy()
    {
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
