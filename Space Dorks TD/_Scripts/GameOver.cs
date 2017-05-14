using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameOver : MonoBehaviour {
    public static GameOver Instance;

    public GameObject endUI;
    public Text endStats;

    void Awake()
    {
        if (Instance != null)
        {
            return;
        }
        Instance = this;
    }

    public void OnGameOver()
    {
        endUI.SetActive(true);
        endStats.text = "Wave: " + EnemySpawner.Instance.waveIndex + "\nEnemies Killed: " + EnemySpawner.enemyCount;
        Time.timeScale = 0;
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }



}
