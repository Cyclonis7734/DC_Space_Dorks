using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGunTC : Turret_Controller {
    [SerializeField] private const int intCost = 300;

    protected override void SetTurretValues()
    {
        floRange = 2.0f;
        floFireRate = 2;
        floTimeWait = .5f;
        booCanFire = true;
        floDamage = 65;
        intMuzzleFlashRate = (int)(floFireRate * 10);
    }

    public override int GetTurretCost()
    {
        return intCost;
    }
}
