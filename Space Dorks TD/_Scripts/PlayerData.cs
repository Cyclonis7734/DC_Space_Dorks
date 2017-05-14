using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerData : MonoBehaviour {
    public static PlayerData Instance;

    public Image healthBar;
    public Text healthText;
    public Text txtPointDisplay;
    

    public float currentHealth;
    private float maxHealth = 100;
    public float pointTotal = 0;

    void Awake()
    {
        Instance = this;
        currentHealth = maxHealth;
        UpdatePoints(500);
    }

    

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        healthBar.fillAmount = currentHealth / maxHealth;
        healthText.text = (currentHealth).ToString();

        if(currentHealth <= 0)
        {
            EndGame();
        }
    }

    public void EndGame()
    {
        GameOver.Instance.OnGameOver();
    }

    public void UpdatePoints(float floUpdateAmount)
    {
        pointTotal += floUpdateAmount;
        txtPointDisplay.text = "Points: " + pointTotal.ToString();
    }

    public float GetPoints() { return pointTotal; }

    
}
