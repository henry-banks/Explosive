using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Explodable))]
public abstract class ExplosionEffect : MonoBehaviour {

    public Explodable explodable;

	// Use this for initialization
	void Start () {
        explodable = GetComponent<Explodable>();
	}

    public abstract void Effect();
}
