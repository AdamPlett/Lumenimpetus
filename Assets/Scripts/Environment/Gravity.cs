using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR.Haptics;

[RequireComponent(typeof (ConstantForce))]
public class Gravity : MonoBehaviour
{
    public bool gravityEnabled;

    //public float defaultGravityForce = 9.81f;
    public float currentGravityForce = 9.81f;

    ConstantForce force;
    Vector3 up, down, left, right, forward, back;

    private float previousGravity = 0f;

    // Start is called before the first frame update
    void Start()
    {
        gravityEnabled = true;
        
        force = GetComponent<ConstantForce>();
        up = new Vector3(0, 1, 0);
        down = new Vector3(0, -1, 0);
        left = new Vector3(-1, 0, 0);
        right = new Vector3(1, 0, 0);
        forward = new Vector3(0, 0, 1);
        back = new Vector3(0, 0, -1);

        currentGravityForce = 15f;
        previousGravity = currentGravityForce;

        SetGravityForce(force.force.normalized * currentGravityForce);
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetKeyDown("g"))
        {
            ChangeGravityDirection(-force.force.normalized);
        }
        if (Input.GetKeyDown("i"))
        {
            AddGravityForce(-force.force.normalized);
        }
        if (Input.GetKeyDown("p"))
        {
            SetGravityForce(-force.force*10);
        }
        */
    }

    public void EnableGravity()
    {
        gravityEnabled = true;
        currentGravityForce = previousGravity;

        SetGravityForce(force.force.normalized * currentGravityForce);
    }

    public void DisableGravity()
    {
        gravityEnabled = false;
        
        if(currentGravityForce != 0f)
        {
            previousGravity = currentGravityForce;
            currentGravityForce = 0f;
        }

        SetGravityForce(Vector3.zero);
    }

    public void ChangeGravityDirection(Vector3 dir)
    {
        if (dir==up)
        {
            force.force = new Vector3(0, currentGravityForce, 0);
        }
        else if (dir==down)
        {
            force.force = new Vector3(0, -currentGravityForce, 0);
        }
        else if (dir==left)
        {
            force.force = new Vector3(-currentGravityForce, 0, 0);
        }
        else if (dir==right)
        {
            force.force = new Vector3(currentGravityForce, 0, 0);
        }
        else if (dir==forward)
        {
            force.force = new Vector3(0, 0, currentGravityForce);
        }
        else if (dir==back)
        {
            force.force = new Vector3(0, 0, -currentGravityForce);
        }
        else
        {

        }
    }

    public void AddGravityForce(Vector3 f)
    {
        force.force += f;
        currentGravityForce = force.force.magnitude;
    }
    
    public void SetGravityForce(Vector3 f)
    {
        force.force = f;
        currentGravityForce = force.force.magnitude;
    }
}
