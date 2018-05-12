using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowDown : ExplosionEffect {

    //Percentage from 0-100.  100 means total freeze.
    public float slowAmount;
    public float slowTime;

    public override void Effect()
    {

        foreach(Collider e in explosive.explosionObjs)
        {
            if(e.GetComponent<Explodable>())
            {
                e.GetComponent<Rigidbody>().drag = slowAmount;
                e.GetComponent<Explodable>().SlowDown(slowAmount, slowTime);
            }
        }
    }
}
