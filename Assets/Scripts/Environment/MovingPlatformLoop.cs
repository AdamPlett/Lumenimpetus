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
    
    // Start is called before the first frame update
    void Start()
    {
        Vector3 direction = new Vector3 (transform.position.x + x, transform.position.y + y, transform.position.z + z);
        transform.DOMove(direction, duration)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(ease).SetUpdate(UpdateType.Fixed);
    }
    
    //alternate way to detect when player is on platform. Requires an on trigger collider above the platform
    /*private void OnTriggerEnter(Collider other)
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
        }
    } */

}
