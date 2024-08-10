using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (ConstantForce))]
public class Gravity : MonoBehaviour
{
    ConstantForce force;
    Vector3 up, down, left, right, forward, back;
    public float defaultGravityForce = 9.81f;

    // Start is called before the first frame update
    void Start()
    {
        force = GetComponent<ConstantForce>();
        up = new Vector3(0, 1, 0);
        down = new Vector3(0, -1, 0);
        left = new Vector3(-1, 0, 0);
        right = new Vector3(1, 0, 0);
        forward = new Vector3(0, 0, 1);
        back = new Vector3(0, 0, -1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangeGravityDirection(Vector3 dir)
    {
        if (dir==up)
        {
            force.force = new Vector3(0, defaultGravityForce, 0);
        }
        else if (dir==down)
        {
            force.force = new Vector3(0, -defaultGravityForce, 0);
        }
        else if (dir==left)
        {
            force.force = new Vector3(-defaultGravityForce, 0, 0);
        }
        else if (dir==right)
        {
            force.force = new Vector3(defaultGravityForce, 0, 0);
        }
        else if (dir==forward)
        {
            force.force = new Vector3(0, 0, defaultGravityForce);
        }
        else if (dir==back)
        {
            force.force = new Vector3(0, 0, -defaultGravityForce);
        }
        else
        {

        }
    }

    public void AddGravityForce(Vector3 f)
    {
        force.force += f;
    }
    
    public void ChangeGravityForce(Vector3 f)
    {
        force.force = f;
    }
}
