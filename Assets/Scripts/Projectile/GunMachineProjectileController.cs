using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunMachineProjectileController : ProjectileController
{
    public Vector2 direction { get; set; }

    protected override void Update()
    {
        MoveProjectile();
    }

    protected override void MoveProjectile()
    {
        Vector2 movement = direction.normalized * movementSpeed * Time.deltaTime;
        transform.Translate(movement);
    }

    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<EnemyController>())
        {
            EnemyController enemy = collision.GetComponent<EnemyController>();

            if (!enemy.GetComponent<HealthManager>().GetEnemyisDead())
            {
                enemy.GetComponent<HealthManager>().TakeDamage(damage);
            }
        }

        gameObject.SetActive(false);
    }
}
