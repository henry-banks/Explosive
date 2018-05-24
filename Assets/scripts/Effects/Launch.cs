using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launch : ExplosionEffect {

    //How powerful the launch is.  Set to negative to suck things in.
    public float LaunchPower;
    //Force falloff?

    public override void Effect()
    {
        foreach(Collider e in explosive.explosionObjs)
        {
            var dir = (e.transform.position - transform.position).normalized;
            Ray ray = new Ray(transform.position, dir);
            RaycastHit hit;

            //Debug.DrawRay(transform.position, dir * 1000, Color.red, 50f);
            if (e.GetComponent<Explodable>())
            {
                
                //if (Physics.Raycast(transform.position, dir, out hit))
                //I was going to put the addforce into the if but it didn't work.
                if (e.GetComponent<Collider>().Raycast(ray, out hit, Mathf.Infinity))
                {
                }
                    e.GetComponent<Rigidbody>().AddForceAtPosition(ray.direction * LaunchPower, hit.point);
                //else
                //{
                //    e.GetComponent<Rigidbody>().AddForceAtPosition(-(dir) * LaunchPower, transform.position);
                //}
            }
        }
    }

}
