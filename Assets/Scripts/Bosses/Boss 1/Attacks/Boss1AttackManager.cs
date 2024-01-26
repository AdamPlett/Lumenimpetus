using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

public enum eMine { energy, explosive }

public class Boss1AttackManager : MonoBehaviour
{
    [Header("Melee")]
    [SerializeField] private GameObject blade;

    [Space(8)]
    public float meleeRange;
    public float meleeDamage;
    public float meleeCooldown;
    public float meleeTimer;
    public bool canMelee;
    [Space(5)]
    public int comboCounter;

    [Header("Enraged Melee")]
    public bool enraged;
    public float slashSpeed;
    public float slashTime;
    public float slashDamage;
    public Transform slashPoint;
    public GameObject[] slashPrefabs;

    [Header("Cannon")]
    public float cannonRangeMin;
    public float cannonRangeMax;
    public float cannonCooldown;
    public float cannonTimer;
    public bool canShoot;
    [Space(8)]
    public eMine currentAmmo;
    public GameObject energyMine;
    public GameObject explosiveMine;
    public Transform bulletSpawnPoint;
    public AudioSource BossAudioSource;
    public AudioClip cannonSFX;

    [Header("Grapple Hook - Grapple")]
    public LayerMask grappleLayer;
    public Transform grappleTarget;
    public Transform prevTarget;
    public LineRenderer lineRender;
    [Space(10)]
    public GameObject grappleBullet;
    public GrappleBullet bulletScript;
    public AudioClip grappleSFX;
    public bool swinging;
    public bool pulling;
    public bool noHit;
    [Space(8)]
    public float grappleRangeMin;
    public float grappleRangeMax;
    public float grappleSpeed;
    public float grappleCooldown;
    public float grappleTimer;
    public bool canGrapple;
    [Space(8)]
    public List<Transform> topFloorPoints;
    public List<Transform> bottomFloorPoints;
    public List<Transform> allGrapplePoints;

    [Header("Grapple Hook - Pull")]
    public float pullRangeMin;
    public float pullRangeMax;
    public float pullSpeed;
    public float pullCooldown;
    public float pullTimer;
    public bool canPull;

    [Header("Grapple Hook - Slam")]
    public float slamRangeMin;
    public float slamRangeMax;
    public float slamSpeed;
    public float slamDamage;
    public float slamCooldown;
    public float slamTimer;
    public bool canSlam;
    public GameObject slamFX;

    [Header("Player Detection")]
    public LayerMask playerLayer;
    public GameObject playerRef;

    public void Update()
    {
        SetCanMelee();
        SetCanShoot();
        SetCanGrapple();
        SetCanPull();
        SetCanSlam();
    }

    #region Melee

    public void SetCanMelee()
    {
        if (meleeTimer < meleeCooldown)
        {
            meleeTimer += Time.deltaTime;
            canMelee = false;
        }
        else
        {
            meleeTimer = meleeCooldown;
            canMelee = true;
        }
    }

    public bool CheckMeleeRange()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, playerRef.transform.position);

        if(distanceToPlayer < meleeRange)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SpawnEnergyWave(int attackNum)
    {
        if(enraged)
        {
            StartCoroutine(SpawnWave(slashPrefabs[attackNum]));
        }
    }

    private IEnumerator SpawnWave(GameObject slash)
    {
        yield return new WaitForSeconds(0.75f);

        Instantiate(slash, slashPoint);
    }

    public void ActivateBlade()
    {
        StartCoroutine(Activate());
    }

    private IEnumerator Activate()
    {
        gm.boss1.anim.bossAnimator.speed = 0f;

        yield return new WaitForSeconds(0.1f);

        blade.SetActive(true);
        gm.boss1.anim.bossAnimator.speed = 1f;
    }

    public void DeactivateBlade()
    {
        blade.SetActive(false);
    }

    #endregion

    #region Cannon

    public void SetCanShoot()
    {
        if (cannonTimer < cannonCooldown)
        {
            cannonTimer += Time.deltaTime;
            canShoot = false;
        }
        else
        {
            cannonTimer = cannonCooldown;
            canShoot = true;
        }
    }

    public bool CheckCannonRange()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, playerRef.transform.position);

        if(distanceToPlayer < cannonRangeMax && distanceToPlayer > cannonRangeMin)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Shoot()
    {
        BossAudioSource.PlayOneShot(cannonSFX);
        if(currentAmmo == eMine.energy)
        {
            GameObject mine = Instantiate(energyMine, bulletSpawnPoint);
            mine.GetComponent<Mine>().InitBullet(playerRef);
        }
        else if (currentAmmo == eMine.explosive)
        {
            GameObject mine = Instantiate(explosiveMine, bulletSpawnPoint);
            mine.GetComponent<Mine>().InitBullet(playerRef);
        }
    }

    #endregion

    #region Grapple

    public void SetCanGrapple()
    {
        if (grappleTimer < grappleCooldown)
        {
            grappleTimer += Time.deltaTime;
            canGrapple = false;
        }
        else
        {
            grappleTimer = grappleCooldown;
            canGrapple = true;
        }
    }

    public void SetCanPull()
    {
        if (grappleTimer < grappleCooldown)
        {
            pullTimer += Time.deltaTime;
            canPull = false;
        }
        else
        {
            pullTimer = pullCooldown;
            canPull = true;
        }
    }

    public void SetCanSlam()
    {
        if (grappleTimer < grappleCooldown)
        {
            slamTimer += Time.deltaTime;
            canSlam = false;
        }
        else
        {
            slamTimer = slamCooldown;
            canSlam = true;
        }
    }

    public bool CheckSeePoint(Transform point)
    {
        Vector3 viewDirection = (point.position - gm.boss1.viewPoint.position);

        Debug.DrawRay(gm.boss1.viewPoint.position, viewDirection, Color.yellow, 1f);

        int layerMask = 1 << 7;
        layerMask = ~layerMask;

        if (Physics.Raycast(gm.boss1.viewPoint.position, viewDirection, out RaycastHit hitInfo, Mathf.Infinity, layerMask))
        {
            Debug.Log("Hit something!");

            if (hitInfo.transform.gameObject.layer.Equals(LayerMask.NameToLayer("grapple")))
            {
                Debug.Log("Can see grapple point!");

                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    public bool CheckGrappleRange()
    {
        List<Transform> possiblePoints = new List<Transform>();

        if(gm.bh.GetCurrentPhase() == 1)
        {
            if (bottomFloorPoints.Count > 0)
            {
                foreach (var point in bottomFloorPoints)
                {
                    if (point)
                    {
                        if (CheckSeePoint(point.transform))
                        {
                            float distance = Vector3.Distance(point.transform.position, transform.position);

                            if (distance < grappleRangeMax && distance > grappleRangeMin)
                            {
                                possiblePoints.Add(point.transform);
                            }
                        }
                    }
                }
            }
            else
            {
                return false;
            }
        }
        else if (gm.bh.GetCurrentPhase() > 1)
        {
            if(gm.boss1.CheckAbovePlayer() || gm.boss1.transform.position.y < 10f)
            {
                if (topFloorPoints.Count > 0)
                {
                    foreach (var point in topFloorPoints)
                    {
                        if (point)
                        {
                            if (CheckSeePoint(point.transform))
                            {
                                float distance = Vector3.Distance(point.transform.position, transform.position);

                                if (distance < grappleRangeMax && distance > grappleRangeMin)
                                {
                                    possiblePoints.Add(point.transform);
                                }
                            }
                        }
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                if (allGrapplePoints.Count > 0)
                {
                    foreach (var point in allGrapplePoints)
                    {
                        if (point != null)
                        {
                            if (CheckSeePoint(point.transform))
                            {
                                float distance = Vector3.Distance(point.transform.position, transform.position);

                                if (distance > grappleRangeMin)
                                {
                                    possiblePoints.Add(point.transform);
                                }
                            }
                        }
                    }
                }
                else
                {
                    return false;
                }
            }
        }

        if(possiblePoints.Count > 0)
        {
            int randomInt = Random.Range(0, possiblePoints.Count);

            Transform target = possiblePoints[randomInt];

            if (target != grappleTarget)
            {
                prevTarget = grappleTarget;
                grappleTarget = target;
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    public bool CheckPullRange()
    {
        Collider[] grapplePoints = Physics.OverlapSphere(transform.position, pullRangeMax, playerLayer);

        if (grapplePoints.Length > 0)
        {
            grappleTarget = grapplePoints[0].transform;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ShootGrapple()
    {
        BossAudioSource.PlayOneShot(grappleSFX);
        ActivateGrapple();
        
        grappleBullet.SetActive(true);

        grappleBullet.transform.parent = lineRender.transform;
        grappleBullet.transform.position = lineRender.transform.position;
        bulletScript.InitBullet(gm.playerRef.transform);
    }

    public void ActivateGrapple()
    {
        lineRender.enabled = true;
    }

    public void DeactivateGrapple()
    {
        lineRender.enabled = false;
    }

    public void StartSwing()
    {
        swinging = true;
    }

    public void EndSwing()
    {
        swinging = false;
    }

    #endregion
}
