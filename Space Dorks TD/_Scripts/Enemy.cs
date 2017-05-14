using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Enemy : MonoBehaviour {
    public static Enemy Instance;
    protected int intPointsWorth;

    void Awake()
    {
        Instance = this;
        intPointsWorth = 25;
    }

    

    public Image healthBar;

    public float startHealth;
    //Updated Current Health to be public
    //for testing/Debugging purposes.
    //-Tom
    public float currentHealth;

    void Start()
    {
        startHealth = Random.Range(90f, (float)(125f + (EnemySpawner.Instance.waveIndex) * 2));
        currentHealth = startHealth;
    }


    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        healthBar.fillAmount = currentHealth / startHealth;

        if (currentHealth <= 0)
        {
            DestroyEnemy(gameObject);
        }
    }

    public void DoDamage()
    {

    }

    public void DestroyEnemy(GameObject enemy, bool booAwardPoints = true)
    {
        EnemySpawner.enemyCount--;
        Destroy(enemy);
        if (booAwardPoints)
        {
            PlayerData.Instance.UpdatePoints(intPointsWorth);
            EnemySpawner.enemyKillCount++;
        }
    }
	
}
