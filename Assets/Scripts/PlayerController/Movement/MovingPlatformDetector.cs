using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

public class MovingPlatformDetector : MonoBehaviour
{
    public bool isGrounded;
    public bool check;
    void FixedUpdate()
    {
        isGrounded = gm.pm.grounded;
        if (!isGrounded)
        {
            check = false;
            /*if (Input.GetAxisRaw("Horizontal") > .25f || Input.GetAxisRaw("Horizontal") <-.25f || Input.GetAxisRaw("Vertical") > .25f || Input.GetAxisRaw("Vertical") < -.25f)
            {
                transform.SetParent(null);
            }*/
        }
        //if (!check)
        //{
            RaycastHit hit;
            Physics.Raycast(transform.position, Vector3.down, out hit, gm.pm.playerHeight * 0.5f + 0.2f);

            if (hit.collider != null)
            {
                if (hit.collider.CompareTag("MovingPlatform"))
                {
                    /*gm.pm.rb.velocity = gm.pm.rb.velocity+hit.collider.GetComponent<Rigidbody>().velocity;
                    Debug.Log(hit.collider.GetComponent<Rigidbody>().velocity);*/
                    //transform.SetParent(hit.transform);
                    MovingPlatformLoop mlp = hit.collider.GetComponent<MovingPlatformLoop>();
                    if (mlp == null)
                    {
                        Debug.Log("MovingPlatformLoop Script not found");
                    }
                    if (mlp!=null)
                    {
                        //gm.pm.rb.AddForce(mlp.GetForce(), ForceMode.Force);
                        //Debug.Log(mlp.GetForce());
                        gm.pm.rb.velocity =  mlp.GetVelocity();
                        Debug.Log(mlp.GetVelocity());
                    }
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
        //}
    }
} 
