using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [Header("General Settings")]
    [SerializeField] private int lifes = 10;

    private int totalLives;

    
    void Start()
    {
        totalLives = lifes;
    }

    private void RemoveLives(EnemyController enemy)
    {
        totalLives--;

        if (totalLives <= 0)
        {
            totalLives = 0;
            //Game over
        }
    }

    private void OnEnable()
    {
        EnemyController.onPathFinished += RemoveLives;
    }

    private void OnDisable()
    {
        EnemyController.onPathFinished -= RemoveLives;
    }
}
