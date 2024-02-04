using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using static GameManager;
public class MovingPlatformLoop : MonoBehaviour
{
    [Header("Movement Offset")]
    public float x = 0;
    public float y = 0;
    public float z = 0;

    [Header("Duration & Ease type")]
    public float duration = 0f;
    public Ease ease = Ease.InOutSine;

    [SerializeField] private Vector3 prevPOS;
    [SerializeField] private Vector3 velocity;
    [SerializeField] private Vector3 prevVelocity;
    [SerializeField] private Vector3 currentVelocity;
    [SerializeField] private Vector3 calculatedVelocity;
    [SerializeField] private float lerpFactor=.95f;
    [SerializeField] private Vector3 force;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 direction = new Vector3 (transform.position.x + x, transform.position.y + y, transform.position.z + z);
        transform.DOMove(direction, duration)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(ease).SetUpdate(UpdateType.Fixed);
        prevPOS = transform.position;
        currentVelocity = Vector3.zero;
        prevVelocity = Vector3.zero;
    }

    private void FixedUpdate()
    {
        calculatedVelocity = (transform.position - prevPOS) / Time.deltaTime;
        currentVelocity = (lerpFactor * calculatedVelocity) + (1 - lerpFactor) * prevVelocity;
        prevVelocity = currentVelocity;
        //force = calculatedVelocity / Time.fixedDeltaTime;
        prevPOS = transform.position;
        //Debug.Log(velocity);
        //Debug.Log(force);
        
    }
    
    //alternate way to detect when player is on platform. Requires an on trigger collider above the platform 
    private void OnTriggerEnter(Collider other)
    {
        if (other == gm.playerCollider)
        {
            gm.playerRef.transform.SetParent(transform);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other == gm.playerCollider)
        {
            gm.playerRef.transform.SetParent(null);
            gm.pm.rb.velocity += GetVelocity();
            //gm.pm.speedChangeFactor = 50f;
            //gm.pm.keepMomentum = true;
        }
    } 
    
    public Vector3 GetVelocity()
    {
        return currentVelocity;
    }
    public Vector3 GetForce()
    {
        return force;
    } 
    
}
