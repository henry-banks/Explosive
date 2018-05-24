using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Variables that are modified by various explosion effects
public struct ExplodableData
{
    //Speed at which the object moves, either by itself or from being blown away.
    float moveSpeed;


}

public class Explodable : MonoBehaviour {

    public GameObject explosion;
    //Used to prevent infinite loop in Explodable/Explosive explosions
    public bool hasExploded = false;

    //Set true if you wish this object to be destroyed when it explodes
    public bool destroyOnExplode = true;
    public float explosionEffectScale = 1;

    //How long the explodable will wait before exploding.
    public float preDelay = 0;

    public ObjLevelData data;
    private LevelManager level;

    public ExplodableData explodableData;

    //Effects that happen during the pre-delay
    ExplosionEffect[] preEffects;

    private void OnDestroy()
    {
        level.placeableObjects[data.idx].currentUsed--;
    }

    // Use this for initialization
    void Start () {
        level = GameManager.Instance.levelManager;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Explode(float preDelay = 0)
    {
        StartCoroutine(delayExplode(preDelay));        
    }


    private IEnumerator delayExplode(float sec)
    {
        yield return new WaitForSeconds(sec + preDelay);

        //if its a bomb then blow up
        if (GetComponent<Explosive>() && !GetComponent<Explosive>().hasExploded)
        { GetComponent<Explosive>().Explode(); }
            

        GameObject g = Instantiate(explosion, transform.position, transform.rotation);
        g.transform.localScale *= explosionEffectScale;

        if (destroyOnExplode)
        {
            Destroy(gameObject);
        }
    }

    public void SlowDown(float slowAmount, float slowCooldown)
    {
        Rigidbody r = GetComponent<Rigidbody>();
        float originalDrag = r.drag;
        r.drag = slowAmount;

        StartCoroutine(generalDelay(slowCooldown));

        r.drag = originalDrag;
    }
    private IEnumerator generalDelay(float wait)
    {
        yield return new WaitForSeconds(wait);
    }
}
