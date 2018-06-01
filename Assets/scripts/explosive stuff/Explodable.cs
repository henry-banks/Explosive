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
    List<ExplosionEffect> preEffects;

    //Effects that happen RIGHT before death
    List<ExplosionEffect> deathEffects;

    //Whether or not this can be removed by player
    public bool removable = false;

    //Slowdown Stuff
    bool first = true;
    private Rigidbody rb;

    private float _timeScale = 1;
    public float timeScale
    {
        get { return _timeScale; }
        set
        {
            if (rb == null)
                return;
            if (!first)
            {
                rb.mass *= timeScale;
                //rb.velocity /= timeScale;
                //rb.angularVelocity /= timeScale;
            }
            first = false;
            //changeTimeScale = true;

            _timeScale = Mathf.Abs(value);

            rb.mass /= timeScale;
            //rb.velocity *= timeScale;
            //rb.angularVelocity *= timeScale;
        }
    }
    private bool changeTimeScale = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        timeScale = _timeScale;
    }

    // Use this for initialization
    void Start () {
        level = GameManager.Instance.levelManager;

        preEffects = new List<ExplosionEffect>();
        deathEffects = new List<ExplosionEffect>();
        foreach(ExplosionEffect e in GetComponents<ExplosionEffect>())
        {
            if (e.explodeState == EffectState.PREDELAY)
                preEffects.Add(e);
            if (e.explodeState == EffectState.PREDEATH)
                deathEffects.Add(e);
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (changeTimeScale)
        {
            rb.velocity *= timeScale;
            rb.angularVelocity *= timeScale;
        }
    }

    public void Explode(float preDelay = 0, bool ignoreDelay = false)
    {
        StartCoroutine(delayExplode(preDelay, ignoreDelay));        
    }


    private IEnumerator delayExplode(float sec, bool ignoreDelay)
    {
        if(preEffects.Count > 0)
            foreach (ExplosionEffect e in preEffects)
                e.Effect();

        if(!ignoreDelay)
            yield return new WaitForSeconds(sec + preDelay);

        //if its a bomb then blow up
        if (GetComponent<Explosive>() && !GetComponent<Explosive>().hasExploded)
        { GetComponent<Explosive>().Explode(); }        

        if(deathEffects.Count > 0)
            foreach (ExplosionEffect e in deathEffects)
                e.Effect();

        if (destroyOnExplode)
        {
            GameObject g = Instantiate(explosion, transform.position, transform.rotation);
            g.transform.localScale *= explosionEffectScale;

            Destroy(gameObject);
        }
    }

    public void SlowDown(float slowAmount, float slowCooldown)
    {
        float originalTimeScale = timeScale;
        timeScale = slowAmount;
        changeTimeScale = true;

        StartCoroutine(generalDelay(slowCooldown));

        rb.velocity /= timeScale;
        rb.angularVelocity /= timeScale;

        changeTimeScale = false;
        timeScale = originalTimeScale;
    }
    private IEnumerator generalDelay(float wait)
    {
        yield return new WaitForSeconds(wait);
    }

    private void OnDestroy()
    {
        level.placeableObjects[data.idx].currentUsed--;
    }
}
