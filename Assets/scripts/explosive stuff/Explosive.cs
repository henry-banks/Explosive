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

    //All effects that this explosive executes when it explodes.
    ExplosionEffect[] effects;

    //All objects caught in the explosion.
    public Collider[] explosionObjs;

	// Use this for initialization
	void Start () {
        overlappingObjects = new List<Explodable>();
        rb = GetComponent<Rigidbody>();

        exBound = GetComponent<SphereCollider>();
        exBound.radius = explosionRadius;
        exBound.enabled = false;

        effects = GetComponents<ExplosionEffect>();
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

    public void Explode(bool ignoreDelay = false)
    {
        //Let everyone know we've EXPLODED!
        exBound.enabled = true;
        hasExploded = true;

        //Get all the other things we're exploding.
        explosionObjs = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach(ExplosionEffect e in effects)
        {
            e.Effect();
        }

        if (GetComponent<Explodable>() && !GetComponent<Explodable>().hasExploded)
            GetComponent<Explodable>().Explode(0,ignoreDelay);
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
