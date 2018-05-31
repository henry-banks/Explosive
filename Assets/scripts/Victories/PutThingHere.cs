using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PutThingHere : VictoryScript {

    public TrackOverlaps winThing;
    public List<GameObject> thingsThatNeedToBePutHere;

    protected override bool VictoryCondition()
    {
        foreach(GameObject e in thingsThatNeedToBePutHere)
        {
            //If any of the things are not colliding wiht the win collision, this is FALSE.
            if (!winThing.currentCollisions.Contains(e))
                return false;
        }
        return true;
    }

}
