using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Creates a blank object that everything gets welded to.
public class Welder : ExplosionEffect {

    //BLANK
    public GameObject template;
    
    public override void Effect()
    {
        GameObject obj = Instantiate(template, transform.position, transform.rotation);
        //WELD THINGS
        foreach(Collider c in explosive.explosionObjs)
        {
            if (c.GetComponent<Explodable>())
            {
                FixedJoint joint = obj.AddComponent<FixedJoint>();
                joint.connectedBody = c.GetComponent<Rigidbody>();
                c.transform.parent = obj.transform;
            }
        }
    }
}
