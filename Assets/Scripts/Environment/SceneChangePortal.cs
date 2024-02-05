using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangePortal : MonoBehaviour
{
    public string sceneToLoad;
    private void OnTriggerEnter(Collider other)
    {
        Destroy(GameManager.gm.gameObject);
        GameManager.gm = null;

        SceneManager.LoadScene(sceneToLoad);
    }
}
