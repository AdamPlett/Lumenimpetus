using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoaderObject : MonoBehaviour
{
    [SerializeField] private SceneLoader loader;
    [SerializeField] private string nextLevel;

    public float fxWaitTime = 0f;
    [Header("READ ONLY")]
    [SerializeField] private bool nextSceneLoaded = false;

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
            LoadNextLevel();
        }
    }
    private void LoadNextLevel()
    {

        while (!nextSceneLoaded)
        {
            nextSceneLoaded = loader.LoadCheck();
            Debug.Log("SceneLoaded: "+nextSceneLoaded);
        }
        loader.StartLoadLevel();
    }
}
