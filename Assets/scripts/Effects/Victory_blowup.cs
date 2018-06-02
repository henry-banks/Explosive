using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Victory_blowup : ExplosionEffect {

    public BlowThingsUp victory;

    private bool hasThingBeenDone = false;

    private void Awake()
    {
        explodeState = EffectState.PREDEATH;
    }

    public override void Effect()
    {
        if (!hasThingBeenDone)
        {
            victory.blownUpCount++;
            hasThingBeenDone = true;
        }
    }
}
