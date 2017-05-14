using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreUpdateManager : MonoBehaviour {

    private static StoreUpdateManager instance;

    public static StoreUpdateManager Instance
    {
        get
        {
            if (instance == null) { instance = GameObject.FindObjectOfType<StoreUpdateManager>(); }
            return instance;
        }
    }

    public Text txtSU;

    public void UpdateTextCR(string strMsgMy,int intWaitForSecsMy)
    {
        //Debug.Log(txtSU.name);
        StartCoroutine(UpdateText(strMsgMy, intWaitForSecsMy));
    }

    private IEnumerator UpdateText(string strMsg, int intWaitForSecs)
    {
        txtSU.text = strMsg;
        yield return new WaitForSeconds(intWaitForSecs);
        txtSU.text = "";
    }

}
