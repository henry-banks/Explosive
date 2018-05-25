using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Spawns an object
public class SpawnObj : ExplosionEffect {

    public GameObject obj;

    public Vector3 spawnLoc;
    public Quaternion spawnRot;

    //If true, the new object will be the child of Transform parent
    public bool isChild;
    public Transform parent;

    public override void Effect()
    {
        if (isChild)
        {
            Instantiate(obj, parent);
        }
        else
        {
            Instantiate(obj, spawnLoc, spawnRot);
        }
    }
}
