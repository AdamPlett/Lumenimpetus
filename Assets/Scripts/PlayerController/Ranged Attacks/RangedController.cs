using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

public class RangedController : MonoBehaviour
{
    public List<RangedAttack> attacks;
    public KeyCode rangedKey = KeyCode.Mouse1;
    public RangedAttack currentWeapon;
    public LayerMask bossLayer;
    public bool hitTarget;

    // Start is called before the first frame update
    void Start()
    {
        currentWeapon = attacks[0];
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(rangedKey) && currentWeapon.canShoot)
        {
            Vector3 target = GetShotTarget();
            currentWeapon.Shoot(target);
            gm.pm.ChangeAnimationState("RangedAttack");
        }
    }

    public void ChangeWeapon(EWeapons newWeapon)
    {
        switch(newWeapon)
        {
            case EWeapons.none:
                Debug.Log("Weapon Selection Error");
                break;

            case EWeapons.basic:
                currentWeapon = attacks[0];
                break;

            case EWeapons.mine:
                currentWeapon = attacks[1];
                break;

            case EWeapons.laser:
                currentWeapon = attacks[2];
                break;


        }
    }
    
    public Vector3 GetShotTarget()
    {
        hitTarget = Physics.Raycast(gm.pm.cam.transform.position, gm.pm.cam.transform.forward, out RaycastHit hit);
        return hit.point;
    }
}
