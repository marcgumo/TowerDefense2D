using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    Animator anim;

    EnemyController enemy;
    
    void Start()
    {
        anim = GetComponent<Animator>();

        enemy = GetComponent<EnemyController>();
    }

    private void PlayHurtAnimation()
    {
        anim.SetTrigger("Hit");
    }

    private void PlayDieAnimation()
    {
        anim.SetTrigger("Die");
    }

    private void EnemyHit(EnemyController _enemy)
    {
        if (enemy == _enemy)
        {
            StartCoroutine(PlayHurt());
        }
    }

    private void EnemyDead(EnemyController _enemy)
    {
        if (enemy == _enemy)
        {
            StartCoroutine(PlayDie());
        }
    }

    private float GetCurrentAnimationLength()
    {
        return anim.GetCurrentAnimatorClipInfo(0).Length;
    }

    IEnumerator PlayHurt()
    {
        enemy.StopMovement();
        PlayHurtAnimation();
        yield return new WaitForSeconds(GetCurrentAnimationLength() - 0.25f);
        enemy.ResumeMovement();
    }

    IEnumerator PlayDie()
    {
        enemy.StopMovement();
        PlayDieAnimation();
        yield return new WaitForSeconds(GetCurrentAnimationLength() + 0.25f);
        enemy.GetComponent<HealthManager>().RestartHealth();
        //enemy.ResumeMovement();
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        HealthManager.onEnemyHit += EnemyHit;
        HealthManager.onEnemyDead += EnemyDead;
    }

    private void OnDisable()
    {
        HealthManager.onEnemyHit -= EnemyHit;
        HealthManager.onEnemyDead -= EnemyDead;
    }
}
