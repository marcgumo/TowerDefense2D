using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [Header("Shop Panel Settings")]
    [SerializeField] private GameObject shopPanel;

    public NodeController CurrentNodeSelected { get; set; }

    [Header("Upgrade Panel Settings")]
    [SerializeField] private TextMeshProUGUI upgradeText;
    [SerializeField] private TextMeshProUGUI sellText;
    [SerializeField] private GameObject upgradePanel;

    [SerializeField] private TextMeshProUGUI levelText;

    [Header("HUD Panel Settings")]
    [SerializeField] private TextMeshProUGUI lifeText;
    [SerializeField] private TextMeshProUGUI currencyText;

    private CurrencyController currencyController;

    [SerializeField] private TextMeshProUGUI waveText;

    private void Start()
    {
        currencyController = GameObject.FindGameObjectWithTag("Currency").GetComponent<CurrencyController>();
    }

    private void Update()
    {
        currencyText.text = currencyController.CurrentCurrency.ToString();
    }

    private void NodeSelected(NodeController nodeSelected)
    {
        CurrentNodeSelected = nodeSelected;
        if (CurrentNodeSelected.IsNodeEmpty())
        {
            shopPanel.SetActive(true);
        }
        else
        {
            upgradePanel.SetActive(true);
            upgradeText.text = CurrentNodeSelected.Turret.TurretUpgrade.UpgradeCost.ToString();
            levelText.text = "Level " + CurrentNodeSelected.Turret.TurretUpgrade.LevelUpgrade.ToString();
            sellText.text = CurrentNodeSelected.Turret.TurretUpgrade.GetSellValue().ToString();
        }
    }

    public void CloseShopPanel()
    {
        shopPanel.SetActive(false);
    }

    public void CloseUpgradePanel()
    {
        upgradePanel.SetActive(false);
    }

    public void UpgradeTurret()
    {
        if (CurrentNodeSelected.IsNodeEmpty())
        {
            return;
        }
        
        CurrentNodeSelected.Turret.TurretUpgrade.UpgradeTurret();
        upgradeText.text = CurrentNodeSelected.Turret.TurretUpgrade.UpgradeCost.ToString();
        levelText.text = "Level " + CurrentNodeSelected.Turret.TurretUpgrade.LevelUpgrade.ToString();
        sellText.text = CurrentNodeSelected.Turret.TurretUpgrade.GetSellValue().ToString();
    }

    public void SellTurret()
    {
        CurrentNodeSelected.SellTurret();
        CloseUpgradePanel();
    }

    public void UpdateTotalLives(int amount)
    {
        lifeText.text = amount.ToString();
    }

    public void UpdateTotalWaves(int amount)
    {
        waveText.text = "Wave " + amount.ToString();
    }

    private void OnEnable()
    {
        NodeController.onNodeSelected += NodeSelected;
    }

    private void OnDisable()
    {
        NodeController.onNodeSelected -= NodeSelected;
    }
}
