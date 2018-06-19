using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

//Place on an empty object in the upper-left corner of the canvas.
public class BombList : MonoBehaviour {

    public LevelManager levelManager;
    public GameObject buttonTemplate;

    //Padding between each button
    public Vector2 padding = new Vector2(20,0);

	// Use this for initialization
	void Start () {
        //make a button for every placeable object
		for(int i = 0; i < levelManager.placeableObjects.Count; i++)
        {
            ObjLevelData e = levelManager.placeableObjects[i];
            //Make a new button
            GameObject newButton = Instantiate(buttonTemplate, transform, false);
            RectTransform rt = buttonTemplate.GetComponent<RectTransform>();

            //Transform it
            newButton.GetComponent<RectTransform>().Translate(new Vector3(((rt.rect.width * i) + (padding.x * (i + 1))) + (rt.rect.width / 2), 0)); //-((rt.rect.height) + padding.y)));

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
        RectTransform tr = GetComponent<RectTransform>();
        Rect r = tr.rect;
        RectTransform childRt = tr.GetChild(tr.childCount - 1).GetComponent<RectTransform>();

        tr.offsetMax = tr.offsetMax + new Vector2(((childRt.position.x + childRt.offsetMax.x)/2) - (padding.x/2), 0);

        //r.xMin = tr.GetChild(0).GetComponent<RectTransform>().rect.xMin;
        //r.yMin = tr.GetChild(0).GetComponent<RectTransform>().rect.yMin;
        //r.yMax = tr.GetChild(0).GetComponent<RectTransform>().rect.yMax;
        //r.xMax = tr.GetChild(tr.childCount - 1).GetComponent<RectTransform>().rect.xMax;

    }

    //Switch to something else if lag happens
    private void Update()
    {
        if (!levelManager)
            levelManager = GameManager.Instance.levelManager;

        //Update text
        for (int i = 0; i < levelManager.placeableObjects.Count; i++)
        {
            ObjLevelData e = levelManager.placeableObjects[i];
            Text t = transform.GetChild(i).GetComponentInChildren<Text>();
            t.text = e.displayName + " - " + e.remaining;
        }
    }
}
