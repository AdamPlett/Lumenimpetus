using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gm;

    [Header("Player Variables")]
    public GameObject playerRef;
    public PlayerMovement pm;
    public PlayerHealth ph;
    public Collider playerCollider;
    public GameObject playerObject;


    [Header("Boss Variables")]
    public GameObject bossRef;

    public Boss1StateMachine boss1;
    public Boss1Health bh;

    public Boss2StateMachine boss2;
    public Boss2Health bh2;


    [Header("Camera Variables")]
    public GameObject cameraRef;
    public int targetFPS=60;


    [Header("UI")]
    public UIManager ui;
    public Canvas playerCanvas;

    void Awake()
    {
        if(gm == null)
        {
            gm = this;
        }
        else
        {
            Destroy(this);
        }
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = targetFPS;
    }

}
