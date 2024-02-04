using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRangedBasic : RangedAttack
{


    
    private void Start()
    {
        

    }
    // Update is called once per frame
    private void Update()
    {
        base.Update();
        if (shooting)
        {
            MoveBullet(speed);

        }
    }
    public override void Shoot(Vector3 targetPosition)
    {
        shootDirection = (targetPosition - startPosition).normalized;
        
        StartCoroutine(ShotDelay());
        


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
