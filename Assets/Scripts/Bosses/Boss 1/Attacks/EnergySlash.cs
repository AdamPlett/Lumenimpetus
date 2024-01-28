using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

public class EnergySlash : MonoBehaviour
{
    Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        transform.parent = null;

        direction = (gm.playerRef.transform.position - transform.position).normalized;

        Destroy(gameObject, gm.boss1.weapons.slashTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.up * Time.deltaTime * gm.boss1.weapons.slashSpeed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject == gm.playerRef)
        {
            ScreenShake.Shake(0.4f, 0.5f);
            gm.ph.DamagePlayer(gm.boss1.weapons.slashDamage);
        }
    }
}
