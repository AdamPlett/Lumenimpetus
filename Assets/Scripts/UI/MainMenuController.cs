using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;
using System;
public class MainMenuController : MonoBehaviour
{
    [SerializeField] Slider SFXSlider;
    [SerializeField] AudioMixer masterMixer;
    public TMP_Text SFXValueText;
    [SerializeField] Slider musicSlider;
    public TMP_Text musicValueText;
    public TMP_Text sensText;
    string savedSens = "savedSens";
    public Slider sensSlider;
    private void Start()
    {
        SetSFXVolume(PlayerPrefs.GetFloat("SavedSFXVolume"));
        SetMusicVolume(PlayerPrefs.GetFloat("SavedMusicVolume"));
        Time.timeScale = 1;
        adjustSensSliderValue(PlayerPrefs.GetFloat(savedSens));
    }

    public void StartGame()
    {
        //load main menu scene
        SceneManager.LoadScene("Arena01");
    }
    public void StartTutorial()
    {
        //load main menu scene
        SceneManager.LoadScene("PlayerHub");
    }
    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Quiting Game");
    }
    
    //sets the current volume
    public void SetSFXVolume(float vol)
    {
        if (vol < 1)
        {
            vol = .001f;
        }
        RefreshSFXSlider(vol);
        PlayerPrefs.SetFloat("SavedSFXVolume", vol);
        masterMixer.SetFloat("SFXVolume", Mathf.Log10(vol / 100) * 20f);
        SFXValueText.text = Math.Round(vol).ToString();
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
        musicValueText.text = Math.Round(vol).ToString();
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
    public void SetSens(float newSens)
    {
        sensText.text = Math.Round(newSens, 2).ToString();
        PlayerPrefs.SetFloat(savedSens, newSens);
    }
    public void adjustSensSliderValue(float value)
    {
        sensSlider.value = value;
    }
}