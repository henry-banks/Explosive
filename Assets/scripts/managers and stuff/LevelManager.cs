using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Info about a certain type of object
[System.Serializable]
public struct ObjLevelData
{
    public string displayName;
    public int id;
    public int idx;
    public GameObject obj;
    public int maxUsed;
    public int currentUsed;
    public Texture2D thumbnail;
}


public class LevelManager : MonoBehaviour {

    public List<ObjLevelData> placeableObjects;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnExplode(Explodable exp)
    {

    }

    public void Victory()
    {

    }
}
