using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [Header("General Settings")]
    [SerializeField] protected float movementSpeed = 12f;
    //[SerializeField] protected int damage = 10;
    public int damage { get; set; }

    private EnemyController enemyTarget;

    [SerializeField] private float minDistanceToDealDamage = 1f;

    public TurretProjectileController TurretOwner { get; set; }

    protected virtual void Update()
    {
        if (enemyTarget != null)
        {
            MoveProjectile();

            RotateProjectile();
        }
    }

    protected virtual void MoveProjectile()
    {
        transform.position = Vector2.MoveTowards(transform.position, enemyTarget.transform.position, movementSpeed * Time.deltaTime);

        float distanceToTarget = (enemyTarget.transform.position - transform.position).magnitude;

        if (distanceToTarget < minDistanceToDealDamage)
        {
            enemyTarget.GetComponent<HealthManager>().TakeDamage(damage);

            TurretOwner.ResetTurretProjectile();

            gameObject.SetActive(false);
        }
    }

    public void SetEnemy(EnemyController enemy)
    {
        enemyTarget = enemy;
    }

    private void RotateProjectile()
    {
        Vector3 enemyPos = enemyTarget.transform.position - transform.position;
        float angle = Vector3.SignedAngle(transform.up, enemyPos, transform.forward);
        transform.Rotate(0, 0, angle);
    }

    public void ResetProjectile()
    {
        enemyTarget = null;
        transform.rotation = TurretOwner.GetComponent<TurretController>().GetRotationPoint().rotation;
    }
}
