using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneLoader : MonoBehaviour
{
    int currentSceneIndex;

    [SerializeField] private bool lvlLoaded = false;
    public void SaveCurrentSceneIndex()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        //set value to some singleton

    }

    public void AsyncLoadLevel(string lvlName)
    {
        //async load lvlName scene

        //wait until load is complete

        lvlLoaded = true;
    }

    public void AsyncLoadNextLevel()
    {
        //retrieve current index from singleton

        //async load next scene

        //wait until load is complete

        lvlLoaded = true;
    }

    public bool LoadCheck()
    {
        return lvlLoaded;
    }

    public void LoadLevel()
    {
        
        //switch scenes
        SceneManager.LoadScene(currentSceneIndex + 1);

        //reset bools
        lvlLoaded = false;
    }
}
