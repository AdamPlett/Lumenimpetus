using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoaderObject : MonoBehaviour
{
    [SerializeField] private SceneLoader loader;
    [SerializeField] private string nextLevel;

    [Header("READ ONLY")]
    [SerializeField] private bool nextSceneLoaded = false;

    public float fxWaitTime = 0;
    private bool fxDone=false;

    private void Awake()
    {
        loader.AsyncLoadLevel(nextLevel);
    }
    //Testing purposes only
    private void Update()
    {
        if (Input.GetKeyDown("i"))
        {
            LoadNextLevel();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //LoadNextLevel();
        }
    }
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
