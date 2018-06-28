using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Scale an ob
public class SineScale : MonoBehaviour {

    public float scale = 1;
    public float minScale = 0.1f;
    public float maxScale = 100;
    public float offset = 0;

    //lessens the curve by dividing the sine.  DO NOT SET TO 0.
    public float div = 1;

    //If true, use cosine instead of sine.
    public bool useCosine = false;
	
	// Update is called once per frame
	void Update () {
        float sin = Mathf.Clamp(Mathf.Abs(((Mathf.Sin(Time.time))/div) + offset) * scale, minScale, maxScale);
        float cos = Mathf.Clamp(Mathf.Abs(((Mathf.Cos(Time.time)) / div) + offset) * scale, minScale, maxScale);
        transform.localScale = useCosine ? new Vector3(cos, cos, cos) : new Vector3(sin, sin, sin);
        //Vector3 scl = useCosine ? new Vector3(cos, cos) : new Vector3(sin, sin);
        //transform.localScale =  Vector3.Scale(transform.localScale, scl);

    }
}
