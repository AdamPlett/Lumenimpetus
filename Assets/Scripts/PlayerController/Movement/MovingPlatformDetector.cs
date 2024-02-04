using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

public class MovingPlatformDetector : MonoBehaviour
{
    public bool isGrounded;
    public bool check;
    void Update()
    {
        isGrounded = gm.pm.grounded;
        if (!isGrounded)
        {
            check = false;
            /*
            if (Input.GetAxis("Horizontal") > .99f || Input.GetAxis("Horizontal") <-.99f || Input.GetAxis("Vertical") > .99f || Input.GetAxis("Vertical") < -.99f)
            {
                //transform.SetParent(null);
            }
            */
        }
        if (!check)
        {
            RaycastHit hit;
            Physics.Raycast(transform.position, Vector3.down, out hit, gm.pm.playerHeight * 0.5f + 0.2f);

            if (hit.collider != null)
            {
                if (hit.collider.CompareTag("MovingPlatform"))
                {
                    //transform.SetParent(hit.transform);

                    /*gm.pm.rb.velocity = gm.pm.rb.velocity+hit.collider.GetComponent<Rigidbody>().velocity;
                    Debug.Log(hit.collider.GetComponent<Rigidbody>().velocity);*/


                    MovingPlatformLoop mlp = hit.collider.GetComponent<MovingPlatformLoop>();
                    gm.pm.mpl = mlp;
                    /*
                    if (mlp == null)
                    {
                        Debug.Log("MovingPlatformLoop Script not found");
                    }
                    else if (mlp!=null)
                    {
                        //gm.pm.rb.AddForce(mlp.GetForce(), ForceMode.Force);
                        //Debug.Log(mlp.GetForce());
                        gm.pm.rb.velocity =  mlp.GetVelocity();
                        Debug.Log(mlp.GetVelocity());
                    }
                    */
                }
                else
                {
                    //transform.SetParent(null);
                    //gm.pm.keepMomentum = true;
                    //gm.pm.speedChangeFactor = 50f;
                }
                check = true;
            }
            else
            {
                //transform.SetParent(null);
                //gm.pm.keepMomentum = true;
                //gm.pm.speedChangeFactor = 50f;
            }
        }
    }
} 
