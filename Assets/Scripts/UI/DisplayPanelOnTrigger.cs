using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;
public class DisplayPanelOnTrigger : MonoBehaviour
{
    [SerializeField] private GameObject[] panelsToActivate;
    [SerializeField] private GameObject[] panelsToDeactivate;

    //activates UI items when player enters collider and disables other necessary UI elements
    private void OnTriggerEnter(Collider other)
    {
        if (other == gm.playerCollider)
        {
            if (panelsToActivate.Length > 0)
            {
                for (int i = 0; i < panelsToActivate.Length; i++)
                {
                    panelsToActivate[i].SetActive(true);
                }
            }
            if (panelsToDeactivate.Length > 0)
            {
                for (int i=0; i<panelsToDeactivate.Length; i++)
                {
                    panelsToDeactivate[i].SetActive(false);
                }
            }
        }
    }
}
