using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BoxSides
{
    TOP,BOTTOM,RIGHT,LEFT,FRONT,BACK
};

[ExecuteInEditMode]
public class InvisibleBox : MonoBehaviour {

    //Width = x, Height = y, Length = z
    public float width = 1, height = 1, length = 1;
    public float thickness = 1;

    /*There must be 6 sides.
    ORDER: Top, Bottom, Right, Left, Back, Front*/
    public GameObject[] sides;

    private void Update()
    {
        //Top/Bottom
        float topPos = ((height / 2) + .5f);// + (thickness - 1);
        sides[0].transform.localPosition = new Vector3(0, topPos, 0);
        sides[1].transform.localPosition = new Vector3(0, -topPos, 0);
        sides[1].transform.localScale = sides[0].transform.localScale = new Vector3(width, thickness, length);

        //Left/Right
        float leftPos = ((width / 2) + .5f);// + (thickness - 1);
        sides[2].transform.localPosition = new Vector3(leftPos, 0, 0);
        sides[3].transform.localPosition = new Vector3(-leftPos, 0, 0);
        sides[3].transform.localScale = sides[2].transform.localScale = new Vector3(thickness, height, length);

        //Back/Front
        float backPos = ((length / 2) + .5f);// + (thickness - 1);
        sides[4].transform.localPosition = new Vector3(0, 0, backPos);
        sides[5].transform.localPosition = new Vector3(0, 0, -backPos);
        sides[5].transform.localScale = sides[4].transform.localScale = new Vector3(width, height, thickness);
    }
}
