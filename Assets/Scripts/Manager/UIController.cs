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

    [Header("GameOver Panel Settings")]
    [SerializeField] private GameObject gameOverPanel;

    [Header("MainMenu Panel Settings")]
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private List<GameObject> levelList;

    private int currentLevel = 0;

    private void Start()
    {
        DisableLevels();
        mainMenuPanel.SetActive(true);
    }

    private void Update()
    {
        if (currencyController != null)
        {
            currencyText.text = currencyController.CurrentCurrency.ToString();
        }
    }

    private void NodeSelected(NodeController nodeSelected)
    {
        CurrentNodeSelected = nodeSelected;
        if (CurrentNodeSelected.IsNodeEmpty())
        {
            shopPanel.SetActive(true);
            upgradePanel.SetActive(false);
        }
        else
        {
            upgradePanel.SetActive(true);
            shopPanel.SetActive(false);
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

    public void CloseGameOverPanel()
    {
        gameOverPanel.SetActive(false);
    }

    public void CloseMainMenuPanel()
    {
        mainMenuPanel.SetActive(false);
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
        if (CurrentNodeSelected.IsNodeEmpty())
        {
            return;
        }

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

    public void ChangeGameSpeed(int value)
    {
        switch (value)
        {
            case 0:
                Time.timeScale = 2f;
                break;
            case 1:
                Time.timeScale = 1f;
                break;
            default:
                Time.timeScale = 0.5f;
                break;
        }
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        gameOverPanel.SetActive(true);
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        DisableLevels();
        mainMenuPanel.SetActive(true);
    }

    private void DisableLevels()
    {
        for (int i = 0; i < levelList.Count; i++)
        {
            levelList[i].SetActive(false);
        }
    }

    public void SelectLevel(int level)
    {
        levelList[level].SetActive(true); 
        currencyController = GameObject.FindGameObjectWithTag("Currency").GetComponent<CurrencyController>();
        currentLevel = level;
    }

    public int GetCurrentLevel()
    {
        return currentLevel;
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
