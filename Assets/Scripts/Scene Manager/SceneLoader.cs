using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneLoader : MonoBehaviour
{
    int currentSceneIndex;
    private string sceneName;
    [SerializeField] private bool lvlLoaded = false;
    private AsyncOperation asyncLoad;
    public void SaveCurrentSceneIndex()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        //set value to some singleton

    }

    public void AsyncLoadLevel(string lvlName)
    {
        sceneName = lvlName;
        //async load lvlName scene
        StartCoroutine(LoadAsyncScene());
        //wait until load is complete

        lvlLoaded = true;
    }

    IEnumerator LoadAsyncScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        while (asyncLoad.progress <= .9)
        {
            yield return null;
        }
        lvlLoaded = true;
    }

    public void AsyncLoadNextLevel()
    {
        //async load next scene
        StartCoroutine(LoadAsyncNextScene());
    }

    IEnumerator LoadAsyncNextScene()
    {
        asyncLoad = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex+1);
        while (asyncLoad.progress <= .9)
        {
            yield return null;
        }
        lvlLoaded = true;
    }

    public bool LoadCheck()
    {
        return lvlLoaded;
    }

    public void LoadLevel()
    {
        while (!lvlLoaded)
        {

        }
        //load Scene
        asyncLoad.allowSceneActivation = true;
        //reset bools
        lvlLoaded = false;
    }
}
