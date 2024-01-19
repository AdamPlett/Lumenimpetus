using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineExplosion : MonoBehaviour
{
    Collider collision;
    ParticleSystem particles;

    void Start()
    {
        particles.Play();

        Invoke("ActivateCollider", 0.25f);
    }

    public void ActivateCollider()
    {
        collision.gameObject.SetActive(true);
    }
}
