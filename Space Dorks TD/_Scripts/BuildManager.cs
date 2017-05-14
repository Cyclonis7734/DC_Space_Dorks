using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildManager : MonoBehaviour {

    public static BuildManager Instance;
    public BuildSelector bsCurrent;
    public GameObject gamoTurretA;
    public GameObject gamoTurretB;
    public GameObject gamoTurretC;

    public Button btnTurretA;
    public Button btnTurretB;
    public Button btnTurretC;

    public Button btnCancel;
    public Button btnUpgrade;
    public Text txtStoreUpdates;

    void Awake()
    {
        if (Instance != null)
            return;
        Instance = this;
        btnTurretB.interactable = false;
    }
    

    private GameObject turretToBuild;

    public GameObject GetTurretToBuild()
    {
        return turretToBuild;
    }

    public void SetTurretToBuild(GameObject turret)
    {
        turretToBuild = turret;
    }

    public void CancelSelection()
    {
        btnCancel.interactable = false;
        btnUpgrade.interactable = false;
        SetTurretToBuild(null);
        bsCurrent = null;
    }

    public void TurnOnButtonUpgrade()
    {
        btnUpgrade.interactable = true;
    }


    public void TurnOnButtonCancel()
    {
        btnCancel.interactable = true;
    }

    public void UpgradeCurrentTurret()
    {
        int intCurrCost = bsCurrent.gamoCurrentTurret.GetComponent<Turret_Controller>().gamoUpgrade.GetComponent<Turret_Controller>().GetTurretCost();
        if(PlayerData.Instance.pointTotal >= intCurrCost)
        {
            PlayerData.Instance.UpdatePoints(-intCurrCost);
            bsCurrent.SetCurrentTurret(bsCurrent.UpgradeTurretHere());
            StoreUpdateManager.Instance.UpdateTextCR("Upgrade Purchased!", 5);
        }
        else
        {
            StoreUpdateManager.Instance.UpdateTextCR("Not Enough Points. Need: " + intCurrCost.ToString(), 5);
        }
        
    }

    private void Update()
    {
        if(PlayerData.Instance.GetPoints() >= gamoTurretA.GetComponent<Turret_Controller>().GetTurretCost()) { btnTurretA.interactable = true; } else { btnTurretA.interactable = false; }
        //if (PlayerData.Instance.GetPoints() >= gamoTurretB.GetComponent<Turret_Controller>().GetTurretCost()) { btnTurretB.interactable = true; } else { btnTurretB.interactable = false; }
        if (PlayerData.Instance.GetPoints() >= gamoTurretC.GetComponent<Turret_Controller>().GetTurretCost()) { btnTurretC.interactable = true; } else { btnTurretC.interactable = false; }
    }

}
