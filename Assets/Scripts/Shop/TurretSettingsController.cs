using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TurretModel", menuName = "Model/Turret/TurretShopSettings", order = 0)]

public class TurretSettingsController : ScriptableObject
{
    public GameObject turretPrefab;
    public int turretShopCost;
    public Sprite turretShopSprite;
}
