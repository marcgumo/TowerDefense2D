using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeController : MonoBehaviour
{
    public TurretController Turret { get; set; }

    public static Action<NodeController> onNodeSelected;

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
        onNodeSelected?.Invoke(this);
    }
}