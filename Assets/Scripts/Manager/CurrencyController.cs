using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyController : MonoBehaviour
{
    [Header("General Settings")]
    [SerializeField] private int currency;

    public int CurrentCurrency { get; set; }

    void Start()
    {
        LoadCurrency();
    }

    private void LoadCurrency()
    {
        CurrentCurrency = currency;
    }

    public void AddCurrency(int amount)
    {
        CurrentCurrency += amount;
    }

    public void RemoveCurrency(int amount)
    {
        if (CurrentCurrency >= amount)
        {
            CurrentCurrency -= amount;
        }
    }

    public void AddCurrencyOnEnemyDead(EnemyController enemy)
    {
        AddCurrency(10);
    }

    private void OnEnable()
    {
        HealthManager.onEnemyDead += AddCurrencyOnEnemyDead;

        LoadCurrency();
    }

    private void OnDisable()
    {
        HealthManager.onEnemyDead -= AddCurrencyOnEnemyDead;
    }
}
