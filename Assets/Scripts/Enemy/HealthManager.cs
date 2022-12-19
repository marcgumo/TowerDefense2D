using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public enum EnemyType { Enemy01, Enemy02, Enemy03, Enemy04, Enemy05 }

    [Header("Health Manager")]
    [SerializeField] private EnemyType currentEnemyType;
    [SerializeField] private Image hpBar;

    [Header("Stats")]
    [SerializeField] private int maxHealth;
    int currentHealth;
    
    void Start()
    {
        currentHealth = maxHealth;
        HPBarUpdate();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            TakeDamage(10);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        HPBarUpdate();

        if (currentHealth <=0)
        {
            currentHealth = 0;

            if (currentEnemyType == EnemyType.Enemy01)
            {
                //die
            }

            if (currentEnemyType == EnemyType.Enemy02)
            {
                //die
            }

            if (currentEnemyType == EnemyType.Enemy03)
            {
                //die
            }

            if (currentEnemyType == EnemyType.Enemy04)
            {
                //die
            }

            if (currentEnemyType == EnemyType.Enemy05)
            {
                //die
            }
        }
    }

    public void RestartHealth()
    {
        currentHealth = maxHealth;
        HPBarUpdate();
    }

    public void HPBarUpdate()
    {
        hpBar.fillAmount = (float)currentHealth / maxHealth;
    }
}
