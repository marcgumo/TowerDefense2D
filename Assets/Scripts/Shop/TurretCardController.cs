using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TurretCardController : MonoBehaviour
{
    [Header("UI Settings")]
    [SerializeField] private Image turretImage;
    [SerializeField] private TextMeshProUGUI turretCost;

    public void SetTurrertButton(TurretSettingsController turretSettings)
    {
        turretImage.sprite = turretSettings.turretShopSprite;
        turretCost.text = turretSettings.turretShopCost.ToString();
    }
}
