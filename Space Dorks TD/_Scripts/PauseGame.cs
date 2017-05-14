using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour {

    public GameObject UI;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnPauseGame();
        }
    }

    public void OnPauseGame()
    {
        if (UI.activeSelf == true)
        {
            UnPauseGame();
        }
        else
        {
            UI.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void UnPauseGame()
    {
        UI.SetActive(false);
        Time.timeScale = 1;
    }


}
