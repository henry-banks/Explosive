using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Explodes things it hits.
public class Chain : ExplosionEffect {

    public float explosionDelay = 0.25f;
    //If true, chain effect only applies to other bombs.
    public bool onlyChainBombs = false;

    public override void Effect()
    {
        //If we've exploded, DON'T DO THIS.
        if (hasExploded)
            return;

        foreach (Collider c in explosive.explosionObjs)
        {

            if (c.gameObject.GetComponent<Explodable>() != null)
            {
                if (onlyChainBombs)
                {
                    if (c.GetComponent<Explosive>())
                    { c.gameObject.GetComponent<Explodable>().Explode(explosionDelay); }
                }
                else
                    c.gameObject.GetComponent<Explodable>().Explode(explosionDelay);
            }
        }
        hasExploded = true;
    }
}
