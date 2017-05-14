using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DBLazerTC : Turret_Controller
{
    public GameObject gamoMuzzleFlash2; //refers to Lazer Shot for this Script
    [SerializeField] private const int intCost = 1500;

    protected override void SetTurretValues()
    {
        floRange = 6.0f;
        floFireRate = 5;
        floTimeWait = .5f;
        booCanFire = true;
        floDamage = 400;
        intMuzzleFlashRate = (int)(floFireRate * 10);
    }

    private void SetLineRenderers(GameObject gamo)
    {
        LineRenderer lren = gamo.GetComponent<LineRenderer>();
        lren.SetPosition(0, gamo.transform.position);
        lren.SetPosition(1, gamoEnemy.transform.position);
        Debug.Log("Name: " + gamo.name + " - " + lren.GetPosition(0).ToString() + " - " + lren.GetPosition(1).ToString());
    }

    protected override IEnumerator FireWeapons()
    {

        //intCount++;
        //Debug.Log("Fire " + intCount.ToString());
        if (gamoEnemy != null)
        {
            //Charge Up Shot
            yield return new WaitForSeconds(floFireRate - audcFire1.length);
            PlayAudClip(audcFire1);
            yield return new WaitForSeconds(audcFire1.length);

            if (gamoEnemy != null)
            {
                SetLineRenderers(gamoMuzzleFlash);
                //Fire Shot 1
                gamoMuzzleFlash.SetActive(true);
                PlayAudClip(audcFire2);
                yield return new WaitForEndOfFrame();
                yield return new WaitForEndOfFrame();
                gamoMuzzleFlash.SetActive(false);
                gamoEnemy.GetComponent<Enemy>().TakeDamage(floDamage / 2);
                yield return new WaitForSeconds(audcFire2.length / 2);

                //Fire Shot 2
                if (gamoEnemy != null)
                {
                    SetLineRenderers(gamoMuzzleFlash2);
                    gamoMuzzleFlash2.SetActive(true);
                    yield return new WaitForEndOfFrame();
                    yield return new WaitForEndOfFrame();
                    gamoMuzzleFlash2.SetActive(false);
                    gamoEnemy.GetComponent<Enemy>().TakeDamage(floDamage / 2);
                    yield return new WaitForSeconds(audcFire2.length / 2);
                }
            }

            PlayAudClip(audcReload);
            yield return new WaitForSeconds(audcReload.length);

            booCanFire = true;
        }
        booCanFire = true;
    }

    public override int GetTurretCost()
    {
        return intCost;
    }

}
