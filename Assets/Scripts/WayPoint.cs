using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour
{
    [Header("General Settings")]
    [SerializeField] private List<Transform> wayPointList;


    public List<Transform> _wayPointList => wayPointList;

    private void OnDrawGizmos()
    {
        for (int i = 0; i < wayPointList.Count; i++)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(wayPointList[i].position, 0.5f);

            if (i < wayPointList.Count - 1)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(wayPointList[i].position, wayPointList[i + 1].position);
            }
        }
    }
}
