using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BombList : MonoBehaviour {

    public GameManager manager;
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
            ubs.manager = manager;
            ubs.obj = e.obj;
            
            //Set up button
            Button b = newButton.GetComponent<Button>();
            //I suppose () => [function] lets you add a listener to a function easily
            b.onClick.AddListener(() => ubs.onClicked());

            //Set up text
            Text t = newButton.gameObject.GetComponentInChildren<Text>();
            t.text = e.displayName;

        }
	}

}
