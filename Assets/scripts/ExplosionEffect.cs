using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Explodable))]
public abstract class ExplosionEffect : MonoBehaviour {

    public Explosive explosive;

	// Use this for initialization
	void Start () {
        explosive = GetComponent<Explosive>();
	}

    public abstract void Effect();
}
