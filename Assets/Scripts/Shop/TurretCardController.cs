using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class TurretCardController : MonoBehaviour
{
    [Header("UI Settings")]
    [SerializeField] private Image turretImage;
    [SerializeField] private TextMeshProUGUI turretCost;

    public TurretSettingsController TurretToBePlaced { get; set; }

    public static Action<TurretSettingsController> onPlaceTurret;

    public void SetTurrertButton(TurretSettingsController turretSettings)
    {
        turretImage.sprite = turretSettings.turretShopSprite;
        turretCost.text = turretSettings.turretShopCost.ToString();

        TurretToBePlaced = turretSettings;
    }

    public void PlaceTuret()
    {
        CurrencyController _currency = GameObject.FindGameObjectWithTag("Currency").GetComponent<CurrencyController>();

        UIController _UIManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIController>();
        
        if (_currency.CurrentCurrency >= TurretToBePlaced.turretShopCost && _UIManager.CurrentNodeSelected != null)
        {
            if (_UIManager.CurrentNodeSelected.IsNodeEmpty())
            {
                _currency.RemoveCurrency(TurretToBePlaced.turretShopCost);

                onPlaceTurret?.Invoke(TurretToBePlaced);
            }

            _UIManager.CloseShopPanel();
        }
    }
}
