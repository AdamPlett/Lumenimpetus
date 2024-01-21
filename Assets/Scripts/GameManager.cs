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

    [Header("Boss Variables")]
    public GameObject bossRef;
    public Boss1StateMachine boss1;
    public BossHealth bh;



    [Header("Camera Variables")]
    public GameObject cameraRef;

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
    }

}
