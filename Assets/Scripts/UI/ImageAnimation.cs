using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ImageAnimation : MonoBehaviour
{
    public Sprite[] sprites;
    public int framesPerSprite;
    public bool loop;
    public bool destroyOnEnd;
    [SerializeField] private bool isActive=true;

    private int index = 0;
    private Image image;
    private int frame = 0;

    // Start is called before the first frame update
    void Awake()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isActive) return;
        //if animation is not looping and you reached the end of the sprite sheet than return
        if (!loop && index == sprites.Length) return;

        frame++;

        //if the frame count is less than the frame rate of the animation than return
        if (frame < framesPerSprite) return;

        image.sprite = sprites[index];
        frame = 0;
        index++;

        if (index >= sprites.Length)
        {
            //if looping than reset the index for animation
            if (loop) index = 0;

            if (destroyOnEnd) Destroy(gameObject);
        }
    }
    public void setActiveFalse()
    {
        index = 0;
        gameObject.SetActive(false);
    }
    public void setActiveTrue()
    {
        gameObject.SetActive(true);
    }
}
