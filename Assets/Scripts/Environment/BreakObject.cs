using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BreakObject : MonoBehaviour
{
    [Header("Fractured Object")]
    [SerializeField] private GameObject brokenObject;
    [SerializeField] private float breakForce;
    [SerializeField] private string destructKey;
    [SerializeField] private float destoryBrokenObjectTime=1.5f;

    [Header("Destruction FX object")]
    public GameObject destructionSFXPlayer;
    public ParticleSystem particles;

    KeyCode thisKeyCode;
    
    [Header("Objects to Destory Simultaneously")]
    public GameObject[] objToDestroy;
    
    [Header("Support")]
    public GameObject[] supportObjects;
    private bool hasSupports = true;

    [Header("Objects to spawn")]
    public GameObject[] spawnObjects;
    private bool hasSpawns = true;

    // Start is called before the first frame update
    void Start()
    {
        thisKeyCode = (KeyCode)System.Enum.Parse(typeof(KeyCode), destructKey, true);
        
        if (supportObjects.Length==0) hasSupports = false;
        if (spawnObjects.Length == 0) hasSpawns = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(thisKeyCode)) breakObject();

        if (hasSupports) checkSupportingPlatforms();
    }
    //replaces game object with fractured or broken game object and applies force to break them up
    public void breakObject()
    {
        //destory all attached objects that need to be destroyed
        for(int i=0; i<objToDestroy.Length; i++)
        {
            if (objToDestroy[i] != null)
            {
                BreakObject destroy = objToDestroy[i].gameObject.GetComponent<BreakObject>();
                
                if (destroy != null)
                {
                    destroy.breakObject();
                }
            }
        }
        //creates object broken into pieces
        if (destructionSFXPlayer != null)
        {
            Instantiate(destructionSFXPlayer);
        }
        if (particles != null)
        {
            particles.Play();
        }
        GameObject broken = Instantiate(brokenObject, transform.position, transform.rotation);

        //adds destruction force to objects
        foreach(Rigidbody rb in broken.GetComponentsInChildren<Rigidbody>())
        {
            Vector3 force = (rb.transform.position - transform.position).normalized*breakForce;
            Debug.Log(force);
            rb.AddForce(force);
            Destroy(rb.gameObject, destoryBrokenObjectTime);
        }

        Invoke("objectSpawner", destoryBrokenObjectTime);
        destroySequence();
    }
    //checks support object array to see if its supports still exist, if not break the object
    public void checkSupportingPlatforms()
    {
        for (int i = 0; i < supportObjects.Length; i++)
        {
            if (supportObjects[i] != null) return;
        }
        breakObject();
    }
    public void objectSpawner()
    {
        for (int i = 0; i < spawnObjects.Length; i++)
        {
            Instantiate(spawnObjects[i]);
        }
    }
    public void destroySequence()
    {
        gameObject.SetActive(false);
        Destroy(gameObject, destoryBrokenObjectTime + .05f);
    }
}
