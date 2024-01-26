using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnStart : MonoBehaviour
{
    public float destructTimer=0;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, destructTimer);
    }
}
