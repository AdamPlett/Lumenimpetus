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
            /*if (Input.GetAxisRaw("Horizontal") > .25f || Input.GetAxisRaw("Horizontal") <-.25f || Input.GetAxisRaw("Vertical") > .25f || Input.GetAxisRaw("Vertical") < -.25f)
            {
                transform.SetParent(null);
            }*/
        }
        if (!check)
        {
            RaycastHit hit;
            Physics.Raycast(transform.position, Vector3.down, out hit, gm.pm.playerHeight * 0.5f + 0.2f);

            if (hit.collider != null)
            {
                if (hit.collider.CompareTag("MovingPlatform"))
                {
                    //gm.pm.rb.velocity = gm.pm.rb.velocity+hit.rigidbody.velocity;
                    transform.SetParent(hit.transform);
                    /*MovingPlatformLoop mlp = hit.collider.GetComponent<MovingPlatformLoop>();
                    if (mlp!=null)
                    {
                        gm.pm.rb.AddForce(mlp.getForce(), ForceMode.Force);
                    } */
                }
                else
                {
                    transform.SetParent(null);
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
