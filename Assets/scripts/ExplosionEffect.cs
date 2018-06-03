using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EffectState
{
    IMMEDIATE,PREDELAY,EXPLOSION,PREDEATH
};

[RequireComponent(typeof(Explodable))]
public abstract class ExplosionEffect : MonoBehaviour {

    public Explosive explosive;
    //When to start the effect
    public EffectState explodeState = EffectState.EXPLOSION;

    //Makes sure effect is done only once.
    protected bool hasExploded = false;

	// Use this for initialization
	void Start () {
        if(GetComponent<Explosive>()) explosive = GetComponent<Explosive>();
	}

    public abstract void Effect();
}
