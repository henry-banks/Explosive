using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//Unnecessary?
public enum UIType
{
    BUTTON, CHECK, SLIDER, NUMBOX, TEXT
};

public class EditMenuScripts : MonoBehaviour {

    GameManager manager;
    public UIType type;
    public GameObject editMenu;

    //TEMP
    public TextMeshProUGUI text;
    public Slider radius;
    public Slider delay;
    public Toggle EOS;

    GameObject editObj { get { return manager.thingToEdit; } }
    Explodable explodable { get { return manager.thingToEdit.GetComponent<Explodable>(); } }
    Explosive explosive
    { get {
            if (manager.thingToEdit.GetComponent<Explosive>())
                return manager.thingToEdit.GetComponent<Explosive>();
            return null;
        } }
	
	// Update is called once per frame
	void Update () {
        if (!manager)
            manager = GameManager.Instance;
    }
    //Stuff to put in update but not EVERY update
    IEnumerator DelayUpdate(float time)
    {
        yield return new WaitForSeconds(time);
        if (!explodable) Exit();
    }
    //Set all the ui values to the current values of the thing we're editing.
    public void Refresh()
    {
        StartCoroutine(delayRefresh());
    }
    IEnumerator delayRefresh()
    {
        if (!manager)
            yield return new WaitForEndOfFrame();

        text.SetText(explodable.data.displayName);
        radius.value = explosive.explosionRadius;
        delay.value = explosive.explosionDelay;
        EOS.isOn = editObj.CompareTag("StartExplode");
    }

    public void Exit()
    {
        editMenu.SetActive(false);
    }

    public void Radius(Slider s)
    {
        explosive.exRadius = s.value;
    }
    public void Delay(Slider s)
    {
        explosive.gameObject.GetComponent<SphereCollider>().radius = explosive.explosionRadius = s.value;
    }
    public void ExplodeOnStart(Toggle t)
    {
        manager.thingToEdit.tag = t.isOn ? "StartExplode": "Bomb";
    }

}
