using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slamFX : MonoBehaviour
{
    public ParticleSystem particles1, particles2;

    private bool destroy = false;

    private void Start()
    {
        particles1.Play();
        particles2.Play();
    }

    private void Update()
    {
        if(!particles1.isPlaying && !particles2.isPlaying)
        {
            Destroy(gameObject);
        }
    }
}
