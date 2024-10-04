using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

public class Crouching : MonoBehaviour
{
    [Header("Crouching Variables")]
    public bool crouched;
    [Space(5)]
    public float crouchScale;

    private float playerScaleX, playerScaleY, playerScaleZ;

    private void Start()
    {
        playerScaleX = gm.player.transform.localScale.x;
        playerScaleY = gm.player.transform.localScale.y;
        playerScaleZ = gm.player.transform.localScale.z;
    }

    // FIX LATER: When crouching, camera moves down gradually, but when uncrouching the camera snaps back to the default position

    public void Crouch()
    {
        crouched = true;
        AdjustPlayerScale(crouchScale);
    }

    public void Uncrouch()
    {
        crouched = false;
        AdjustPlayerScale(playerScaleY);
    }

    private void AdjustPlayerScale(float yScale)
    {
        gm.player.transform.localScale = new Vector3(playerScaleX, yScale, playerScaleZ);
    }
}
