using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChangeCollider : MonoBehaviour
{
    public GameObject sceneChangeUI;
    public GameObject sceneChangePortal;
    private bool canSwitchScene = false;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X) && canSwitchScene)
        {
            Instantiate(sceneChangePortal, transform);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        sceneChangeUI.SetActive(true);
        canSwitchScene = true;
    }
    private void OnTriggerExit(Collider other)
    {
        sceneChangeUI.SetActive(false);
        canSwitchScene = false;
    }
}
    
