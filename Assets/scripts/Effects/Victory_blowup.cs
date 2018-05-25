using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Victory_blowup : ExplosionEffect {

    public BlowThingsUp victory;

    public override void Effect()
    {
        victory.blownUpCount++;
    }
}
