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

        Destroy(gameObject, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.up * Time.deltaTime * 25f;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject == gm.playerRef)
        {
            gm.ph.DamagePlayer(10f);
        }
    }
}
