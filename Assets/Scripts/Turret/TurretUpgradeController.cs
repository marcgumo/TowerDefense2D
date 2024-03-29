using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretUpgradeController : MonoBehaviour
{
    [Header("General Settings")]
    [SerializeField] private int updateInitialCost;
    [SerializeField] private int updateCostIncremental;
    [SerializeField] private int damageIncremental;

    private TurretProjectileController turretProjectile;

    [SerializeField] private float delayReduce;

    [Header("Sell Settings")]
    [Range(0, 1)]
    [SerializeField] private float sellPercent;

    public float SellPerc { get; set; }

    public int UpgradeCost { get; set; }
    private CurrencyController currencyController;

    public int LevelUpgrade { get; set; }

    void Start()
    {
        turretProjectile = GetComponent<TurretProjectileController>();

        UpgradeCost = updateInitialCost;

        currencyController = GameObject.FindGameObjectWithTag("Currency").GetComponent<CurrencyController>();

        LevelUpgrade = 1;

        SellPerc = sellPercent;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            UpgradeTurret();
        }
    }

    public void UpgradeTurret()
    {
        if (currencyController.CurrentCurrency < UpgradeCost)
        {
            return;
        }
        
        turretProjectile.DamageToAssign += damageIncremental;

        turretProjectile.DelayPerShot -= delayReduce;

        if (turretProjectile.DelayPerShot <= 0)
        {
            turretProjectile.DelayPerShot = 0;
        }

        UpdateTurretUpgrade();
    }

    private void UpdateTurretUpgrade()
    {
        currencyController.RemoveCurrency(UpgradeCost);
        UpgradeCost += updateCostIncremental;

        LevelUpgrade++;
    }

    public int GetSellValue()
    {
        return Mathf.RoundToInt(UpgradeCost * SellPerc);
    }
}
