using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretGMProjectileController : TurretProjectileController
{
    [SerializeField] private bool isDualTurret;
    [SerializeField] private float spreadRange;
    
    protected override void Update()
    {
        if (Time.time > _nextAttackTimer)
        {
            if (turret.CurrentEnemyTarget != null)
            {
                Vector2 directionToTarget = turret.CurrentEnemyTarget.transform.position - transform.position;
                FireProjectile(directionToTarget);
            }

            _nextAttackTimer = Time.time + DelayPerShot;
        }
    }

    private void FireProjectile(Vector2 direction)
    {
        GameObject _projectile = pooler.GetPoolObject(projectile);
        _projectile.transform.position = projectileFirePoint.position;

        _projectile.GetComponent<GunMachineProjectileController>().direction = direction;

        _projectile.GetComponent<GunMachineProjectileController>().damage = DamageToAssign;

        if (isDualTurret)
        {
            float randomSpeed = Random.Range(-spreadRange, spreadRange);

            Quaternion _spreadRotation = Quaternion.Euler(0, 0, randomSpeed);

            _projectile.GetComponent<GunMachineProjectileController>().direction = _spreadRotation * direction;
        }

        _projectile.SetActive(true);
    }
}
