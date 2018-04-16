using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;


public class BombList : MonoBehaviour {

    public LevelManager levelManager;
    public GameObject buttonTemplate;

	// Use this for initialization
	void Start () {
        //make a button for every placeable object
		foreach(ObjLevelData e in levelManager.placeableObjects)
        {
            //Make a new button
            GameObject newButton = Instantiate(buttonTemplate, transform, false);

            //Set up bomb select script
            uiBombSelect ubs = newButton.GetComponent<uiBombSelect>();
            ubs.objData = e;
            
            //Set up button
            Button b = newButton.GetComponent<Button>();
            //I suppose () => [function] lets you add a listener to a function easily
            b.onClick.AddListener(() => ubs.onClicked());

            //newButton.GetComponent<RawImage>().texture = AssetPreview.GetMiniThumbnail(e.obj);

            //Set up text
            Text t = newButton.gameObject.GetComponentInChildren<Text>();
            t.text = e.displayName + " - " + e.remaining;

        }
	}

    //Switch to something else if lag happens
    private void Update()
    {
        for (int i = 0; i < levelManager.placeableObjects.Count; i++)
        {
            ObjLevelData e = levelManager.placeableObjects[i];
            Text t = transform.GetChild(i).GetComponentInChildren<Text>();
            t.text = e.displayName + " - " + e.remaining;
        }
    }
}
