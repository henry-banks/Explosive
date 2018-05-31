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

    public List<VictoryScript> victories;
    public List<ObjLevelData> placeableObjects;
    public bool customVictories = false;

    public bool isWin { get {
            foreach (VictoryScript v in victories)
            { if (!v.isVictory) return false; }
            return true;
                } }

	// Use this for initialization
	void Start () {
        GameManager.Instance.levelManager = this;
        for (int i = 0; i < placeableObjects.Count; i++)
            placeableObjects[i].idx = i;

        //If we're not using custom victories, assume that all the victoryScripts attached to the levelEditor are all the victories we want.
        if (!customVictories)
        {
            victories.Clear();
            foreach (VictoryScript v in GetComponents<VictoryScript>())
                victories.Add(v);
        }
	}
	
    //Delayed victory check so we aren't running EVERY TICK.
    private IEnumerator delayVictoryCheck()
    {
        if (isWin)
            Victory();

        yield return new WaitForSeconds(0.2f);
    }

	// Update is called once per frame
	void Update () {
        
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
