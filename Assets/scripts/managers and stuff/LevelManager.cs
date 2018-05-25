using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Info about a certain type of object
[System.Serializable]
public class ObjLevelData
{
    public string displayName;
    public int idx;
    public GameObject obj;
    public int maxUsed;
    public int currentUsed;
    public Texture2D thumbnail;

    public int remaining { get { return maxUsed - currentUsed; } }
}


public class LevelManager : MonoBehaviour {

    public VictoryScript victory;
    public List<ObjLevelData> placeableObjects;

	// Use this for initialization
	void Start () {
        GameManager.Instance.levelManager = this;
        for (int i = 0; i < placeableObjects.Count; i++)
            placeableObjects[i].idx = i;
	}
	
	// Update is called once per frame
	void Update () {
        StartCoroutine(victory.VictoryCheck());
	}


    public void setObjUsed(int idx, int num)
    {
        placeableObjects[idx].currentUsed = num;
    }

    public void OnExplode(Explodable exp)
    {

    }

    public void Victory()
    {

    }
}
