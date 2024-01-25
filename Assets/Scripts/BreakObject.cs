using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakObject : MonoBehaviour
{

    [SerializeField] private GameObject brokenObject;
    [SerializeField] private float breakForce;
    [SerializeField] private string destructKey;
    [SerializeField] private float destoryBrokenObjectTime=1.5f;
    KeyCode thisKeyCode;
    [Header("Objects to Destory Simultaneously")]
    public GameObject[] objToDestroy;
    [Header("Support")]
    public GameObject[] supportObjects;
    private bool hasSupports = true;
    // Start is called before the first frame update
    void Start()
    {
        thisKeyCode = (KeyCode)System.Enum.Parse(typeof(KeyCode), destructKey, true);
        if (supportObjects.Length==0) hasSupports = false;
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
            BreakObject destroy = objToDestroy[i].gameObject.GetComponent<BreakObject>();

            if (destroy != null)
            {
                destroy.breakObject();
            }
        }
        //creates object broken into pieces
        GameObject broken = Instantiate(brokenObject, transform.position, transform.rotation);

        //adds destruction force to objects
        foreach(Rigidbody rb in broken.GetComponentsInChildren<Rigidbody>())
        {
            Vector3 force = (rb.transform.position - transform.position).normalized*breakForce;
            Debug.Log(force);
            rb.AddForce(force);
            Destroy(rb.gameObject, destoryBrokenObjectTime);
        }
        Destroy(gameObject);
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
}
