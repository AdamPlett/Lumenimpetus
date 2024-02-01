using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class MovingPlatformLoop : MonoBehaviour
{
    [Header("Movement Offset")]
    public float x = 0;
    public float y = 0;
    public float z = 0;

    [Header("Duration")]
    public float duration = 0f;
    
    // Start is called before the first frame update
    void Start()
    {
        Vector3 direction = new Vector3 (transform.position.x + x, transform.position.y + y, transform.position.z + z);
        transform.DOMove(direction, duration)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine);
    }

}
