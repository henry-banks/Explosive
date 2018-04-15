using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explodable : MonoBehaviour {

    public GameObject explosion;
    //Used to prevent infinite loop in Explodable/Explosive explosions
    public bool hasExploded = false;
    public float explosionEffectScale = 1;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Explode(float preDelay = 0)
    {
        StartCoroutine(delay(preDelay));        
    }
    private IEnumerator delay(float sec)
    {
        yield return new WaitForSeconds(sec);

        //if its a bomb then blow up
        if (GetComponent<Explosive>() && !GetComponent<Explosive>().hasExploded)
        { GetComponent<Explosive>().Explode(); }
            

        GameObject g = Instantiate(explosion, transform.position, transform.rotation);
        g.transform.localScale *= explosionEffectScale;
        Destroy(gameObject);
    }
}
