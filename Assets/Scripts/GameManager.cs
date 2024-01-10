using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gm;

    [Header("Player Variables")]
    public GameObject playerPrefab;
    public GameObject playerRef;

    [Header("Camera Variables")]
    public GameObject cameraPrefab;
    public GameObject cameraRef;

    void Start()
    {
        if(gm == null)
        {
            gm = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void InitPlayer()
    {
        playerRef = Instantiate(playerPrefab);
    }

    private void InitCamera()
    {
        cameraRef = Instantiate(cameraPrefab);
    }
}
