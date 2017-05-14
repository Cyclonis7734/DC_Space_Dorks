using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Turret_Controller : MonoBehaviour {

    [Header("Turret Values")]
    [SerializeField] protected float floRange;
    [SerializeField] protected float floFireRate;
    [SerializeField] protected float floDamage;
    public GameObject gamoUpgrade;
    public AudioClip audcFire1;
    public AudioClip audcFire2;
    public AudioClip audcReload;
    [SerializeField] private const int intCost = 300;

    [Header("Turret Details")]
    [SerializeField] protected Vector2 v2TargetDist;
    [SerializeField] protected GameObject gamoEnemy;
    [SerializeField] Vector3 v3Offset = new Vector3(0f, 0f, -90f);
    [SerializeField] protected float floTimeStamp;
    [SerializeField] protected float floTimeWait;
    [SerializeField] protected bool booCanFire;
    [SerializeField] protected GameObject gamoMuzzleFlash;
    [SerializeField] protected int intMuzzleFlashRate;

    protected int intCount = 0;

    protected void Awake()
    {
        SetTurretValues();
        floTimeStamp = Time.time;
    }

    protected virtual void SetTurretValues()
    {
        floRange = 2f;
        floFireRate = 2;
        floTimeWait = .5f;
        booCanFire = true;
        floDamage = 50;
        intMuzzleFlashRate = (int)(floFireRate * 10);
    }

    protected void Update()
    {

        if (Time.time - floTimeStamp < floTimeWait)
        {
            //Debug.Log(Time.time.ToString());
            UpdateTarget();
        }

        if(gamoEnemy != null)
        {   
            transform.right = gamoEnemy.transform.position - transform.position;
            transform.rotation *= Quaternion.Euler(v3Offset);
        }

        if (booCanFire && gamoEnemy != null)
        {
            booCanFire = false;
            StartCoroutine(FireWeapons());
        }

    }

    protected virtual void UpdateTarget()
    {
        try
        {
            if (gamoEnemy == null)  // added a null check so turrets only change target if they don't have one, seemed weird they constantly switched targets. Love, Charles.
            {
                //Debug.Log("Searching for game objects");
                foreach (GameObject gamo in GameObject.FindGameObjectsWithTag("Enemy"))
                {
                    //Debug.Log(gamo.name);
                    if (gamoEnemy != null)
                    {
                        if (Vector2.Distance(transform.position, gamo.transform.position) < Vector2.Distance(transform.position, gamoEnemy.transform.position))
                        {
                            gamoEnemy = gamo;
                        }
                    }
                    else
                    {

                        gamoEnemy = gamo;
                    }
                }//End For Each find closest enemy
            }
            if (Vector2.Distance(gamoEnemy.transform.position, transform.position) > floRange)
            {
                gamoEnemy = null;
            }
            floTimeStamp = Time.time;
        }
        catch (Exception)
        {
            //e.ToString();
            //Debug.Log("No Enemy's Found: " + e.Message.ToString());
            gamoEnemy = null;
            floTimeStamp = Time.time;
        }

    }//End of Method UpateTarget

    protected void PlayAudClip(AudioClip audcToPlay, float floVol = 1f)
    {
        AudioSource.PlayClipAtPoint(audcToPlay, transform.position, floVol);
    }

    protected void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,floRange);
    }//End Method OnDrawGizmosSelected

    protected virtual IEnumerator FireWeapons()
    {

        //intCount++;
        //Debug.Log("Fire " + intCount.ToString());
        if (gamoEnemy != null)
        {
            //floMuzzleFlashRate 20
            //floFireRate 2
            float floTempFireRate = ((floFireRate * 100) / intMuzzleFlashRate) / 100;
            float floTempDamage = ((floDamage * 100) / intMuzzleFlashRate) / 100;

            for (int i = 0; i < intMuzzleFlashRate; i++)
            {
                if (gamoEnemy != null)
                {
                    gamoMuzzleFlash.SetActive(true);
                    PlayAudClip(audcFire1,.25f);
                    if(audcFire2 != null) { PlayAudClip(audcFire2,.25f); }
                    gamoEnemy.GetComponent<Enemy>().TakeDamage(floTempDamage);
                    yield return new WaitForEndOfFrame();
                    gamoMuzzleFlash.SetActive(false);
                    yield return new WaitForSeconds(floTempFireRate);
                }
                else
                {
                    //PlayAudClip(audcReload);
                    //yield return new WaitForSeconds(audcReload.length);
                    booCanFire = true;
                    break;
                }
            }
            PlayAudClip(audcReload);
            yield return new WaitForSeconds(audcReload.length);
            booCanFire = true;
        }
    }

    public GameObject UpgradeTurret()
    {
        GameObject gamo = Instantiate(gamoUpgrade, transform.position,Quaternion.identity);
        Destroy(gameObject);
        return gamo;
    }

    public bool CanBeUpgraded()
    {
        if(gamoUpgrade == null) { return false; } else { return true; }
    }

    public virtual int GetTurretCost() { return intCost; }
}
