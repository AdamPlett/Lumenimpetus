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
            
            if (Input.GetAxis("Horizontal") > .75f || Input.GetAxis("Horizontal") <-.75f || Input.GetAxis("Vertical") > .75f || Input.GetAxis("Vertical") < -.75f)
            {
                transform.SetParent(null);
            }
            
        }
        if (!check)
        {
            RaycastHit hit;
            Physics.Raycast(transform.position, Vector3.down, out hit, gm.pm.playerHeight * 0.5f + 0.0125f);
            Debug.DrawRay(transform.position, Vector3.down * (gm.pm.playerHeight * 0.5f + 0.0125f), new Color (0,255,0));

            if (hit.collider != null)
            {
                if (hit.collider.CompareTag("MovingPlatform"))
                {
                    transform.SetParent(hit.transform);

                    //gets moving platform from rigidbody velocity
                    /*gm.pm.rb.velocity = gm.pm.rb.velocity+hit.collider.GetComponent<Rigidbody>().velocity;
                    Debug.Log(hit.collider.GetComponent<Rigidbody>().velocity);*/

                    //gives refernce of moving platform and passes it to playermovement so playermovement can get and apply velocity
                    /*
                    MovingPlatformLoop mlp = hit.collider.GetComponent<MovingPlatformLoop>();
                    gm.pm.mpl = mlp;
                    */
                    
                    //aplies velocity to players rigidbody
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
                    transform.SetParent(null);
                    //gm.pm.keepMomentum = true;
                    //gm.pm.speedChangeFactor = 50f;
                }
                check = true;
            }
            //unparents player if not grounded
            else
            {
                //transform.SetParent(null);
                //gm.pm.keepMomentum = true;
                //gm.pm.speedChangeFactor = 50f;
            }
        }
    }
} 
