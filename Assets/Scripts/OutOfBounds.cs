using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

public class OutOfBounds : MonoBehaviour
{
    [SerializeField] private float freezeDuration=.15f;
    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other == gm.playerCollider)
        {
            //gm.playerRef.transform.position = 
            gm.pm.playerVelocity = Vector3.zero;
            gm.pm.freeze = true;
            Invoke("unfreezePlayer", freezeDuration);
        }
    }
    public void unfreezePlayer()
    {
        gm.pm.freeze = false;
    }
}
