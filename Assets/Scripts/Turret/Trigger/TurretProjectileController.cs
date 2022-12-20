using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretProjectileController : MonoBehaviour
{
    [Header("General Settings")]
    [SerializeField] protected Transform projectileFirePoint;

    [SerializeField] protected float attackDelay = 0.75f;
    protected float _nextAttackTimer;

    [Header("Pooler Settings")]
    [SerializeField] protected int projectileToStored = 10;
    [SerializeField] protected GameObject projectile;

    protected ObjectPooler pooler;

    [SerializeField] int initialDamage;

    public int DamageToAssign { get; set; }

    public float DelayPerShot { get; set; }

    ProjectileController currentProjectileLoaded;
    protected TurretController turret;

    void Start()
    {
        pooler = new ObjectPooler();
        pooler.StorePoolObject(projectileToStored, projectile);

        turret = GetComponent<TurretController>();

        DamageToAssign = initialDamage;

        DelayPerShot = attackDelay;
    }

    protected virtual void Update()
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

            _nextAttackTimer = Time.time + DelayPerShot;
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

        currentProjectileLoaded.damage = DamageToAssign;

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
