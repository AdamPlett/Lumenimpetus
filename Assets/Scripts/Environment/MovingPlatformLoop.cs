using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class MovingPlatformLoop : MonoBehaviour
{
    [Header("Direction Vector")]
    public float x = 0;
    public float y = 0;
    public float z = 0;

    public float duration = 0f;
    
    // Start is called before the first frame update
    void Start()
    {
        Vector3 direction = new Vector3 (x, y, z);
        transform.DOLocalMove(direction, duration)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine);
    }

}
