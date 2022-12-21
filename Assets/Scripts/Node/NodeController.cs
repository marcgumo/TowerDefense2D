using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeController : MonoBehaviour
{
    public TurretController Turret { get; set; }

    public static Action<NodeController> onNodeSelected;

    public static Action onTurretSold;

    public Image effect;

    public void SetTurret(TurretController _turret)
    {
        Turret = _turret;
    }

    public bool IsNodeEmpty()
    {
        return Turret == null;
    }

    public void SelectTurret()
    {
        Transform nodes = GameObject.Find("Nodes").transform;
        for (int i = 0; i < nodes.childCount; i++)
        {
            nodes.GetChild(i).GetComponent<NodeController>().StopAllCoroutines();
            nodes.GetChild(i).GetComponent<NodeController>().effect.gameObject.SetActive(false);
        }

        onNodeSelected?.Invoke(this);
        StartCoroutine(NodeEffect());
    }

    IEnumerator NodeEffect()
    {
        effect.gameObject.SetActive(true);
        
        yield return new WaitForSeconds(0.25f);

        effect.gameObject.SetActive(false);

        yield return new WaitForSeconds(0.25f);

        StartCoroutine(NodeEffect());
    }

    public void SellTurret()
    {
        GameObject.FindGameObjectWithTag("Currency").GetComponent<CurrencyController>().AddCurrency(
            Turret.TurretUpgrade.GetSellValue());
        Destroy(Turret.gameObject);
        Turret = null;
        GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIController>().CurrentNodeSelected = null;

        onTurretSold?.Invoke();
    }
}
