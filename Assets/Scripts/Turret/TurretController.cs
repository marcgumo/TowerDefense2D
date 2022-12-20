using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    [Header("General Settings")]
    [SerializeField] private float attackRange = 3f;
    [SerializeField] private Transform rotationPoint;

    private List<EnemyController> enemyList;

    public EnemyController CurrentEnemyTarget { get; set; }

    void Start()
    {
        enemyList = new List<EnemyController>();
    }

    void Update()
    {
        SetCurrentEnemyTarget();

        RotateTowardsTarget();
    }

    private void SetCurrentEnemyTarget()
    {
        if (enemyList.Count <= 0)
        {
            CurrentEnemyTarget = null;
            return;
        }

        CurrentEnemyTarget = enemyList[0];

        if (CurrentEnemyTarget.GetComponent<HealthManager>().GetEnemyisDead())
        {
            enemyList.Remove(CurrentEnemyTarget);
        }
    }

    private void RotateTowardsTarget()
    {
        if (CurrentEnemyTarget == null)
        {
            return;
        }

        Vector3 targetPos = CurrentEnemyTarget.transform.position - transform.position;

        float angle = Vector3.SignedAngle(rotationPoint.transform.up, targetPos, rotationPoint.transform.forward);
        rotationPoint.transform.Rotate(0, 0, angle);
    }

    public Transform GetRotationPoint()
    {
        return rotationPoint;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<EnemyController>())
        {
            enemyList.Add(collision.GetComponent<EnemyController>());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<EnemyController>())
        {
            EnemyController enemy = collision.GetComponent<EnemyController>();

            if (enemyList.Contains(enemy))
            {
                enemyList.Remove(enemy);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, attackRange);

        GetComponent<CircleCollider2D>().radius = attackRange;
    }
}
