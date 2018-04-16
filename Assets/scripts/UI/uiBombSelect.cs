using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class uiBombSelect : MonoBehaviour {

    public ObjLevelData objData;

	public void onClicked()
    {
        GameManager.Instance.thingToPlace = objData.obj;
        GameManager.Instance.thingIdx = objData.idx;
    }
}
