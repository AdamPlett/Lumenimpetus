using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoaderObject : MonoBehaviour
{
    [SerializeField] private bool nextSceneLoaded = false;

    [SerializeField] private SceneLoader loader;

    public float fxWaitTime = 0;
    private bool fxDone=false;

    private void LoadNextLevel()
    {
        //play FX

        Invoke("FXTimer", fxWaitTime);

        while (!nextSceneLoaded || !fxDone)
        {
            nextSceneLoaded = loader.LoadCheck();
        }
        loader.LoadLevel();
    }
    private void FXTimer()
    {
        fxDone = true;
    }
}
