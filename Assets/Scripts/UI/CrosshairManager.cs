using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CrosshairManager : MonoBehaviour
{
    [Header("Refereneces")]
    public GameObject crosshairGrapple;
    public Image grappleImage;

    [Header("Shake Settings")]
    public float duration;
    public float strength;
    public int virbrato;
    public float randomness;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeGrappleColor(Color rgb)
    {
        grappleImage.color = rgb;
    }
    public void grappleCrosshair(bool grappling)
    {
        gameObject.SetActive(!grappling);
        crosshairGrapple.SetActive(grappling);

    }
    public void shakeGrappleCrosshair()
    {
        crosshairGrapple.GetComponent<RectTransform>().DOShakeAnchorPos(duration, strength, virbrato, randomness);
    }
}
