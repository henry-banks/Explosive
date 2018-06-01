using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*https://answers.unity.com/questions/711749/slow-time-for-a-single-rigid-body.html */

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
                e.GetComponent<Explodable>().SlowDown(1/slowAmount, slowTime);
            }
        }
    }
}
