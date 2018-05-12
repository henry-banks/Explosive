using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Destroys explosive when it explodes.
public class DestroySelf : ExplosionEffect {

    public override void Effect()
    {
        if (GetComponent<Explodable>() && !GetComponent<Explodable>().hasExploded)
            GetComponent<Explodable>().Explode();
    }
}
