using System;
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

    public static Action<EnemyController> onEnemyDead;
    EnemyController enemy;

    public static Action<EnemyController> onEnemyHit;

    private bool enemyDead = false;
    
    void Start()
    {
        currentHealth = maxHealth;
        HPBarUpdate();

        enemy = GetComponent<EnemyController>();
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
        if (enemyDead)
        {
            return;
        }
        
        currentHealth -= damage;

        HPBarUpdate();

        if (currentHealth <=0)
        {
            currentHealth = 0;

            if (currentEnemyType == EnemyType.Enemy01)
            {
                EnemyDead();
            }

            if (currentEnemyType == EnemyType.Enemy02)
            {
                EnemyDead();
            }

            if (currentEnemyType == EnemyType.Enemy03)
            {
                EnemyDead();
            }

            if (currentEnemyType == EnemyType.Enemy04)
            {
                EnemyDead();
            }

            if (currentEnemyType == EnemyType.Enemy05)
            {
                EnemyDead();
            }
        }
        else
        {
            onEnemyHit?.Invoke(enemy);
        }
    }

    public void RestartHealth()
    {
        currentHealth = maxHealth;
        HPBarUpdate();

        Invoke("ResetEnemyDeadLater", 0.1f);
    }

    public void HPBarUpdate()
    {
        hpBar.fillAmount = (float)currentHealth / maxHealth;
    }

    void EnemyDead()
    {
        //RestartHealth();

        enemyDead = true;

        enemy.ResetCurrentPathPoint();

        onEnemyDead?.Invoke(enemy);

        //gameObject.SetActive(false);
    }

    public bool GetEnemyisDead()
    {
        return enemyDead;
    }

    private void ResetEnemyDeadLater()
    {
        enemyDead = false;
        enemy.ResumeMovement();
    }

    public int GetEnemyType()
    {
        return (int)currentEnemyType;
    }

    private void OnEnable()
    {
        currentHealth = maxHealth;
        HPBarUpdate();
    }
}
