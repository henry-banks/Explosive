using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlowThingsUp : VictoryScript {

    public List<Explodable> thingsToBlowUp;
    public int blownUpCount = 0;

    protected override bool VictoryCondition()
    {
        //foreach(Explodable e in thingsToBlowUp)
        //{
        //    if (!e.gameObject)
        //    {
        //        if (e.hasExploded)
        //        {
        //            blownUpCount++;
        //            continue;
        //        }

        //    }
        //    else
        //    {
        //        blownUpCount++;
        //    }
        //}

        return blownUpCount >= thingsToBlowUp.Count;
    }
}
