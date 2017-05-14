using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildSelector : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private GameObject gamoBaseTower;
    private Color col;
    Renderer ren;
    public GameObject gamoCurrentTurret;
    //public Canvas canvTurretStore;

    public Vector2 positionOffset;

    private void Awake()
    {
        ren = GetComponent<Renderer>();
        col = ren.material.color;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        BuildManager.Instance.bsCurrent = this;
        if (BuildManager.Instance.GetTurretToBuild() == null && gamoCurrentTurret == null)
            return;

        if(gamoCurrentTurret != null)
        {
            Turret_Controller tcon = gamoCurrentTurret.GetComponent<Turret_Controller>();
            if (tcon.CanBeUpgraded()) { BuildManager.Instance.TurnOnButtonUpgrade(); }
            return;
        }

        GameObject turretToBuild = BuildManager.Instance.GetTurretToBuild();
        int intNewCost = turretToBuild.GetComponent<Turret_Controller>().GetTurretCost();

        if (PlayerData.Instance.pointTotal >= intNewCost)
        {
            PlayerData.Instance.UpdatePoints(-intNewCost);
            gamoCurrentTurret = (GameObject)Instantiate(turretToBuild, transform.position, Quaternion.identity);
            StoreUpdateManager.Instance.UpdateTextCR("Purchased: " + turretToBuild.name + " for " + intNewCost.ToString() + " points", 3);
            BuildManager.Instance.CancelSelection();
        }
        else
        {
            StoreUpdateManager.Instance.UpdateTextCR("Not Enough Points. Need: " + intNewCost.ToString(), 3);
        }
        //canvTurretStore.enabled = !canvTurretStore.enabled;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log(gameObject.name + " - Changing color");
        ren.material.color = Color.green;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ren.material.color = col;
    }


    public void SetCurrentTurret(GameObject gamo)
    {
        gamoCurrentTurret = gamo;
    }

    public GameObject UpgradeTurretHere()
    {
        GameObject gamo = gamoCurrentTurret.GetComponent<Turret_Controller>().UpgradeTurret();
        BuildManager.Instance.CancelSelection();
        return gamo;
    }

}
