using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//used to pause/unpause.  Stores rigidbody values so they can be reset later.
public struct PauseData
{
    float oMass;
    Vector3 oVelocity;
    Vector3 oAngularVelocity;
    bool oGravity;
    bool oKinematic;

    //Saves state of the rigidbody.
    public void Save(Rigidbody rb)
    {
        oMass = rb.mass;
        oVelocity = rb.velocity;
        oAngularVelocity = rb.angularVelocity;
        oGravity = rb.useGravity;
        oKinematic = rb.isKinematic;
    }

    //Restores rigidbody to the stored state.
    public void Load(Rigidbody rb)
    {
        rb.mass = oMass;
        rb.velocity = oVelocity;
        rb.angularVelocity = oAngularVelocity;
        rb.useGravity = oGravity;
        rb.isKinematic = oKinematic;
    }
}

[RequireComponent(typeof(Rigidbody))]
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

    public PauseData pauseData;

    //Effects that happen during the pre-delay
    List<ExplosionEffect> preEffects;

    //Effects that happen RIGHT before death
    List<ExplosionEffect> deathEffects;

    //Whether or not this can be removed by player
    public bool removable = false;

    //Used to toggle pause/unpause;
    bool paused = false;

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
                rb.velocity /= timeScale;
                rb.angularVelocity /= timeScale;
            }
            first = false;
            //changeTimeScale = true;

            _timeScale = Mathf.Abs(value);

            rb.mass /= timeScale;
            rb.velocity *= timeScale;
            rb.angularVelocity *= timeScale;
        }
    }
    private bool changeTimeScale = false;

    private void Awake()
    {
        
    }

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        timeScale = _timeScale;
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

    private void Update()
    {
        if (!rb && GetComponent<Rigidbody>())
        {
            rb = GetComponent<Rigidbody>();
            timeScale = _timeScale;
        }
        if (!level) level = GameManager.Instance.levelManager;
    }

    // Update is called once per frame
    void FixedUpdate () {

        //Stuff to change when the level is paused
        if(level.isPaused)
        {
            //If we were not previously paused, save state.
            if (!paused)
            {
                pauseData.Save(rb);
                paused = true;
            }

            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.mass = 0;
            rb.useGravity = false;
            rb.isKinematic = true;
        }
        //Vs. not paused.
        else
        {
            //If we were previously paused, restore to unpaused state.
            if(paused)
            {
                pauseData.Load(rb);
                paused = false;
            }
            //if (changeTimeScale)
            //{
            //    rb.velocity *= timeScale;
            //    rb.angularVelocity *= timeScale;
            //}

            rb.velocity += Physics.gravity * (Time.deltaTime * timeScale);
        }
    }

    public void Explode(float preDelay = 0, bool ignoreDelay = false)
    {
        StartCoroutine(delayExplode(preDelay, ignoreDelay));        
    }


    private IEnumerator delayExplode(float sec, bool ignoreDelay)
    {
        //Do not explode if paused.
        if (level.isPaused)
        {
            yield return new WaitForEndOfFrame();
        }

        //Execute all pre-explosion effects
        if (preEffects.Count > 0)
            foreach (ExplosionEffect e in preEffects)
                e.Effect();

        //Do the delay unless we're explicitly ignoring the delay
        if (!ignoreDelay)
            yield return new WaitForSeconds(sec + preDelay);

        //if its a bomb then blow up
        if (GetComponent<Explosive>() && !GetComponent<Explosive>().hasExploded)
        { GetComponent<Explosive>().Explode(); }        

        //Execute all pre-death(post-explosion? effects
        if(deathEffects.Count > 0)
            foreach (ExplosionEffect e in deathEffects)
                e.Effect();

        //If we destroy on explosion...
        if (destroyOnExplode)
        {
            //Make an explosion
            GameObject g = Instantiate(explosion, transform.position, transform.rotation);
            g.transform.localScale *= explosionEffectScale;

            //DIE.
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
        //PAUSE.
        while(level.isPaused)
        {
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(wait);
    }

    private void OnDestroy()
    {
        if(data.idx >=0 )
            level.placeableObjects[data.idx].currentUsed--;
    }
}
