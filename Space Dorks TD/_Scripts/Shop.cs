using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour {

    BuildManager buildManager;

    void Start()
    {
        buildManager = BuildManager.Instance;
    }
    
    public void PurchaseTurretA()
    {
        buildManager.SetTurretToBuild(buildManager.gamoTurretA);
    }

    public void PurchaseTurretB()
    {
        buildManager.SetTurretToBuild(buildManager.gamoTurretB);
    }

    public void PurchaseTurretC()
    {
        buildManager.SetTurretToBuild(buildManager.gamoTurretC);
    }


}
