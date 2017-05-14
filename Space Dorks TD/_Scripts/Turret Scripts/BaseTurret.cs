using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTurret : Turret_Controller {
    [SerializeField] private const int intCost = 150;
    
    public override int GetTurretCost()
    {
        return intCost;
    }

}
