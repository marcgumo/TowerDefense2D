using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretProjectileController : MonoBehaviour
{
    [Header("General Settings")]
    [SerializeField] private Transform projectileFirePoint;

    [SerializeField] private float attackDelay = 0.75f;
    float _nextAttackTimer;

    [Header("Pooler Settings")]
    [SerializeField] private int projectileToStored = 10;
    [SerializeField] private GameObject projectile;

    ObjectPooler pooler;

    ProjectileController currentProjectileLoaded;
    TurretController turret;

    void Start()
    {
        pooler = new ObjectPooler();
        pooler.StorePoolObject(projectileToStored, projectile);

        turret = GetComponent<TurretController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            LoadProjectile();
        }

        if (isTurretEmpty())
        {
            LoadProjectile();
        }

        if (Time.time > _nextAttackTimer)
        {
            if (turret.CurrentEnemyTarget != null && currentProjectileLoaded != null)
            {
                currentProjectileLoaded.transform.parent = null;
                currentProjectileLoaded.SetEnemy(turret.CurrentEnemyTarget);
            }

            _nextAttackTimer = Time.time + attackDelay;
        }
    }

    private void LoadProjectile()
    {
        GameObject _projectile = pooler.GetPoolObject(projectile);
        _projectile.transform.position = projectileFirePoint.position;
        _projectile.transform.SetParent(projectileFirePoint.transform);

        currentProjectileLoaded = _projectile.GetComponent<ProjectileController>();

        currentProjectileLoaded.TurretOwner = this;

        currentProjectileLoaded.ResetProjectile();

        _projectile.SetActive(true);
    }

    public void ResetTurretProjectile()
    {
        currentProjectileLoaded = null;
    }

    private bool isTurretEmpty()
    {
        return currentProjectileLoaded == null;
    }
}
