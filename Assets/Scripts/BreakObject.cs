using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakObject : MonoBehaviour
{

    [SerializeField] private GameObject brokenObject;
    [SerializeField] private float breakForce;
    [SerializeField] private string destructKey;
    KeyCode thisKeyCode;
    [Header("Objects to Destory Simultaneously")]
    public GameObject[] objToDestroy;
    [Header("Support")]
    public GameObject[] supportObjects;

    // Start is called before the first frame update
    void Start()
    {
        thisKeyCode = (KeyCode)System.Enum.Parse(typeof(KeyCode), destructKey, true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(thisKeyCode))
            breakObject();
    }
    //replaces game object with fractured or broken game object and applies force to break them up
    public void breakObject()
    {
        for(int i=0; i<objToDestroy.Length; i++)
        {
            BreakObject destroy = objToDestroy[i].gameObject.GetComponent<BreakObject>();

            if (destroy != null)
            {
                destroy.breakObject();
            }
        }
        GameObject broken = Instantiate(brokenObject, transform.position, transform.rotation);

        foreach(Rigidbody rb in broken.GetComponentsInChildren<Rigidbody>())
        {
            Vector3 force = (rb.transform.position - transform.position).normalized*breakForce;
            Debug.Log(force);
            rb.AddForce(force);
        }
        Destroy(gameObject);
    }
}
