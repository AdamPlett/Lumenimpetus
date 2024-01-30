using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

public class PhyicsMaterialControl : MonoBehaviour
{
    private Collider col;
    public PhysicMaterial mat;

    public void Start()
    {
        col = gm.playerObject.GetComponent<Collider>();
    }

    public void Update()
    {
        if (gm.pm.state == PlayerMovement.MovementState.air)
        {
            
            col.material = mat;
        }
        else
        {
            col.material = null;
        }
    }
}
