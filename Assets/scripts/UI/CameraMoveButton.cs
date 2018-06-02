using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraMoveButton : MonoBehaviour {

    public bool isLeft = false;
    public PlayerController pc;

    private void Update()
    {
        if (!pc)
            pc = FindObjectOfType<PlayerController>();
    }

    public void Move()
    {
        if(isLeft)
        {
            if (pc.cameraLocIdx == 0)
                pc.cameraLocIdx = pc.cameraLocations.Count - 1;
            else
                pc.cameraLocIdx--;
        }
        else
        {
            if (pc.cameraLocIdx + 1 < pc.cameraLocations.Count)
                pc.cameraLocIdx++;
            else pc.cameraLocIdx = 0;
        }
    }
}
