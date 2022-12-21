using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretShopController : MonoBehaviour
{
    [Header("Turret Settings")]
    [SerializeField] private GameObject turretButtonPrefab;
    [SerializeField] private Transform turretPanelContent;
    [SerializeField] private TurretSettingsController[] turret;

    private NodeController currentNodeSelected;
    
    void Start()
    {
        for (int i = 0; i < turret.Length; i++)
        {
            CreateTurretCard(turret[i]);
        }
    }

    private void CreateTurretCard(TurretSettingsController turretSettings)
    {
        GameObject _instance = Instantiate(turretButtonPrefab, turretPanelContent.position, Quaternion.identity);
        _instance.transform.SetParent(turretPanelContent);
        _instance.transform.localScale = Vector3.one;

        TurretCardController _cardButton = _instance.GetComponent<TurretCardController>();
        _cardButton.SetTurrertButton(turretSettings);
    }

    private NodeController NodeSelected()
    {
        return GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIController>().CurrentNodeSelected;
    }

    private void PlaceTurret(TurretSettingsController turretToBePlaced)
    {
        currentNodeSelected = NodeSelected();

        if (currentNodeSelected.IsNodeEmpty())
        {
            GameObject _instance = Instantiate(turretToBePlaced.turretPrefab);
            _instance.transform.position = currentNodeSelected.transform.position;
            _instance.transform.parent = currentNodeSelected.transform;

            TurretController turretPlaced = _instance.GetComponent<TurretController>();
            currentNodeSelected.SetTurret(turretPlaced);
        }
    }

    private void OnEnable()
    {
        TurretCardController.onPlaceTurret += PlaceTurret;
    }

    private void OnDisable()
    {
        TurretCardController.onPlaceTurret -= PlaceTurret;
    }
}
