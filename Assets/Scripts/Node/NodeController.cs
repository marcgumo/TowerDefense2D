using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class NodeController : MonoBehaviour
{
    public TurretController Turret { get; set; }

    public static Action<NodeController> onNodeSelected;

    public static Action onTurretSold;

    public Image effect;
    public Transform range;

    public void SetTurret(TurretController _turret)
    {
        Turret = _turret;
        StopAllEffects();
    }

    public bool IsNodeEmpty()
    {
        return Turret == null;
    }

    public void SelectTurret()
    {
        StopAllEffects();

        onNodeSelected?.Invoke(this);

        if (!IsNodeEmpty())
        {
            range.localScale = new Vector3(Turret.GetAttackRange() * 2, Turret.GetAttackRange() * 2, 1);
        }

        StartCoroutine(NodeEffect());
    }

    public void StopAllEffects()
    {
        Transform nodes = GameObject.Find("Nodes").transform;
        for (int i = 0; i < nodes.childCount; i++)
        {
            nodes.GetChild(i).GetComponent<NodeController>().StopAllCoroutines();
            nodes.GetChild(i).GetComponent<NodeController>().effect.gameObject.SetActive(false);
            nodes.GetChild(i).GetComponent<NodeController>().range.localScale = Vector3.one;
        }
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

        StopAllEffects();

        onTurretSold?.Invoke();
    }
}
