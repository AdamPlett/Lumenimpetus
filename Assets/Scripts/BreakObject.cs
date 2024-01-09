using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakObject : MonoBehaviour
{
   
    [SerializeField] private GameObject brokenObject;
    [SerializeField] private float breakForce;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("f"))
            breakObject();
    }
    //replaces game object with fractured or broken game object and applies force to break them up
    public void breakObject()
    {
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
