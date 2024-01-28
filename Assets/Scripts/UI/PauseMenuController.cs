using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField] GameObject MenuPanel;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject hud;
    [SerializeField] GameObject Crosshair;
    [SerializeField] Slider SFXSlider;
    [SerializeField] AudioMixer masterMixer;
    public TMP_Text SFXValueText;
    [SerializeField] Slider musicSlider;
    public TMP_Text musicValueText;

    private bool dead = false;
    private void Start()
    {
        SetSFXVolume(PlayerPrefs.GetFloat("SavedSFXVolume"));
        SetMusicVolume(PlayerPrefs.GetFloat("SavedMusicVolume"));
        Time.timeScale = 1;
        dead = false;
    }
    void Update()
    {
        //brings up pop up menu
        if ((Input.GetKeyDown(KeyCode.Escape)) && (MenuPanel.activeSelf == false) && (dead==false))
        {
            PopUpMenu();
        }
        else if ((Input.GetKeyDown(KeyCode.Escape)) && (MenuPanel.activeSelf == true))
        {
            Resume();
        }
    }
    public void ExitLevel()
    {
        //load main menu scene
        //SceneManager.LoadScene("MainMenu");
    }
    public void RestartLevel()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }
    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Quiting Game");
    }
    public void PopUpMenu()
    {
        //makes pop up menu visible
        MenuPanel.SetActive(true);
        //unlock and unhide cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Crosshair.SetActive(false);
        //freezes time
        Time.timeScale = 0;
    }
    public void Resume()
    {
        //makes pop up menu disappear
        MenuPanel.SetActive(false);
        //lock and hide cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Debug.Log("cursor should be locked");
        Crosshair.SetActive(true);
        //unfreezes time
        Time.timeScale = 1;
    }

    //on player death show gameover screen
    public void GameOverScreen()
    {
        gameOverPanel.SetActive(true);
        hud.SetActive(false);
        dead = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    //sets the current volume
    public void SetSFXVolume(float vol)
    {
        if (vol <1 )
        {
            vol = .001f;
        }
        RefreshSFXSlider(vol);
        PlayerPrefs.SetFloat("SavedSFXVolume", vol);
        masterMixer.SetFloat("SFXVolume", Mathf.Log10(vol/100)*20f);
        SFXValueText.text = vol.ToString();
    }
    //takes the value from the slider and set volume corresepondingly
    public void SetSFXVolumeFromSlider()
    {
        SetSFXVolume(SFXSlider.value);
    }
    //assign slider value 
    public void RefreshSFXSlider(float vol)
    {
        SFXSlider.value = vol;
    }

    //sets the current volume
    public void SetMusicVolume(float vol)
    {
        if (vol < 1)
        {
            vol = .001f;
        }
        RefreshMusicSlider(vol);
        PlayerPrefs.SetFloat("SavedMusicVolume", vol);
        masterMixer.SetFloat("MusicVolume", Mathf.Log10(vol / 100) * 20f);
        musicValueText.text = vol.ToString();
    }

    //takes the value from the slider and set volume corresepondingly
    public void SetMusicVolumeFromSlider()
    {
        SetMusicVolume(musicSlider.value);
    }

    //assign slider value 
    public void RefreshMusicSlider(float vol)
    {
        musicSlider.value = vol;
    }
}