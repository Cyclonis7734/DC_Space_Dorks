using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DBMachineGunTC : Turret_Controller {

    [SerializeField] private const int intCost = 600;

    protected override void SetTurretValues()
    {
        floRange = 2.0f;
        floFireRate = 3;
        floTimeWait = .5f;
        booCanFire = true;
        floDamage = 75;
        intMuzzleFlashRate = 10;
    }

    protected override IEnumerator FireWeapons()
    {

        //intCount++;
        //Debug.Log("Fire " + intCount.ToString());
        if (gamoEnemy != null)
        {
            float floTempFireRate = 1.8f/(float)intMuzzleFlashRate;
            float floTempDamage = floDamage/(float)intMuzzleFlashRate;

            GetComponent<AudioSource>().Play();
            for (int i = 0; i < intMuzzleFlashRate; i++)
            {
                if (gamoEnemy != null)
                {
                    gamoEnemy.GetComponent<Enemy>().TakeDamage(floTempDamage);
                    gamoMuzzleFlash.SetActive(true);
                    yield return new WaitForEndOfFrame();
                    yield return new WaitForEndOfFrame();
                    gamoMuzzleFlash.SetActive(false);
                    yield return new WaitForSeconds(floTempFireRate);
                    
            }
                else
                {
                    GetComponent<AudioSource>().Stop();
                    booCanFire = true;
                break;
            }
            }
            GetComponent<AudioSource>().Stop();
            PlayAudClip(audcReload);
            yield return new WaitForSeconds(audcReload.length);
            booCanFire = true;
        }
    }

    public override int GetTurretCost()
    {
        return intCost;
    }


}
