using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRangedBasic : RangedAttack
{
    private Vector3 startPosition;
    private Vector3 shootDirection;

    
    private void Start()
    {
        

    }
    // Update is called once per frame
    private void Update()
    {
        base.Update();
        if (shooting)
        {
            MoveBullet();
        }
    }
    public override void Shoot(Vector3 targetPosition)
    {
        shootDirection = (targetPosition - startPosition).normalized;
        
        ShotDelay();


    }
    private void MoveBullet()
    {

    }
    private IEnumerator ShotDelay()
    {
        firing = true;
        yield return new WaitForSeconds(shotDelay);
        firing = false;
        shooting = true;
        Debug.Log("Bang");
    }
    
    
}
