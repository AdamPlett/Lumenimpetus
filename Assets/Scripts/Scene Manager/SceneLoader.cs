using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneLoader : MonoBehaviour
{
    int currentSceneIndex;
    private string sceneName;
    [Header("READ ONLY")]
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
    }

    IEnumerator LoadAsyncScene()
    {
        yield return null;
        asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = false;
        Debug.Log("Progress: " +asyncLoad.progress + "%");
        while (asyncLoad.progress < .9f)
        {
            Debug.Log("Progress: " + asyncLoad.progress + "%");
            yield return null;
        }
        Debug.Log("Scene Loaded ready to switch");
        lvlLoaded = true;
    }

    public void AsyncLoadNextLevel()
    {
        //async load next scene
        StartCoroutine(LoadAsyncNextScene());
    }

    IEnumerator LoadAsyncNextScene()
    {
        yield return null;
        asyncLoad = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex+1);
        asyncLoad.allowSceneActivation = false;
        while (asyncLoad.progress < .9f)
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
        Debug.Log("Loading scene the proper way!");
        asyncLoad.allowSceneActivation = true;
        //reset bools
        lvlLoaded = false;
    }
}
