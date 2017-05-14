using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour {
    public static EnemySpawner Instance;

    void Awake()
    {
        Instance = this;

        //reset static counts
        enemyKillCount = 0;
        enemyCount = 0;
    }

    public Transform enemyPrefab;
    public Transform spawnPoint;
    public Text waveCountDownTimer;
    public Text waveNumber;

    public float waveTimer = 30.5f;
    private float countdown = 30f; //change to 30s for beginning setup
    public float spawnGap = 0.5f;

    public int waveIndex = 0;
    public static int enemyCount;
    private static int _enemyKillCount;
    public static int enemyKillCount
    {
        get { return _enemyKillCount; }
        set
        {
            _enemyKillCount = value;
            Debug.Log("Kill Count Now: " + _enemyKillCount);
        }
    }

    void Update()
    {

        if (countdown <= 0f)
        {
            WaveSpawnMethod();
        }
        countdown -= Time.deltaTime;
        waveCountDownTimer.text = Mathf.Round(countdown).ToString();
        waveNumber.text = "Wave: " + waveIndex.ToString();

    }

    public IEnumerator SpawnWave()
    {
        waveIndex++;

        for (int i = 0; i < waveIndex; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnGap);
        }

    }

    void SpawnEnemy()
    {
        enemyCount++;
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }

    public void WaveSpawnMethod()
    {
        StartCoroutine(SpawnWave());
        countdown = waveTimer;
    }
}
