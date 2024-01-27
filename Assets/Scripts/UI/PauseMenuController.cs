using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField] GameObject _MenuPanel;
    [SerializeField] GameObject _Crosshair;
    void Update()
    {
        //brings up pop up menu
        if ((Input.GetKeyDown(KeyCode.Escape)) && (_MenuPanel.activeSelf == false))
        {
            PopUpMenu();
        }
        else if ((Input.GetKeyDown(KeyCode.Escape)) && (_MenuPanel.activeSelf == true))
        {
            Resume();
        }
    }
    public void ExitLevel()
    {
        //load main menu scene
        //SceneManager.LoadScene("MainMenu");
    }
    public void PopUpMenu()
    {
        //makes pop up menu visible
        _MenuPanel.SetActive(true);
        //unlock and unhide cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        _Crosshair.SetActive(false);
        //freezes time
        Time.timeScale = 0;
    }
    public void Resume()
    {
        //makes pop up menu disappear
        _MenuPanel.SetActive(false);
        //lock and hide cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Debug.Log("cursor should be locked");
        _Crosshair.SetActive(true);
        //unfreezes time
        Time.timeScale = 1;
    }
}