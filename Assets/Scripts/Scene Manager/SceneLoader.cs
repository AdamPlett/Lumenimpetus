using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneLoader : MonoBehaviour
{
    int currentSceneIndex;
    public void SaveCurrentSceneIndex()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        //set value to some singleton
    }
    public void LoadTransitionScene()
    {

    }
    public void LoadLevel()
    {

    }
    public void LoadNextLevel()
    {
        //retrieve current index from singleton

        //play FX

        //load next level

        //switch scenes
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}
