using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class PlayerCam : MonoBehaviour
{
    public float sensX;
    public float sensY;

    public Transform orientation;
    public Transform camHolder;

    float xRotation;
    float yRotation;

    public TMP_Text sensText;
    public Slider sensSlider;
    string savedSens = "savedSens";

    public bool locked;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        adjustSliderValue(PlayerPrefs.GetFloat(savedSens));

    }

    private void LateUpdate()
    {
        //get mouse input
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        //rotate cam & orientation
        if (!locked)
        {
            camHolder.rotation = Quaternion.Euler(xRotation, yRotation, 0);
            orientation.rotation = Quaternion.Euler(0, yRotation, 0);
        }
    }

   //changes the FOV of the camera
    public void DoFov(float endFOV, float transitionTime)
    {
        GetComponent<Camera>().DOFieldOfView(endFOV, transitionTime);

    }
    //Tilts the camera
    public void DoTilt(float zTilt, float transitionTime) 
    {
        transform.DOLocalRotate(new Vector3(0, 0, zTilt), transitionTime);
    }


    public void adjustSens(float newSens)
    {
        sensX = newSens * 100;
        sensY = newSens * 100;
        sensText.text = newSens.ToString();
        PlayerPrefs.SetFloat(savedSens, newSens);
    }

    public void adjustSliderValue(float value)
    {
        sensSlider.value = value;
    }
}
