using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [Header("General Settings")]
    [SerializeField] private int lifes = 10;

    private int totalLifes;

    private UIController UIManager;

    public int CurrentWave { get; set; }

    void Start()
    {
        totalLifes = lifes;

        UIManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIController>();
        UIManager.UpdateTotalLives(totalLifes);

        CurrentWave = 1;
        UIManager.UpdateTotalWaves(CurrentWave);
    }

    private void RemoveLives(EnemyController enemy)
    {
        totalLifes--;

        if (totalLifes <= 0)
        {
            totalLifes = 0;
            UIManager.GameOver();
        }

        UIManager.UpdateTotalLives(totalLifes);
    }

    private void WaveCompleted()
    {
        CurrentWave++;
        UIManager.UpdateTotalWaves(CurrentWave);
    }

    private void OnEnable()
    {
        EnemyController.onPathFinished += RemoveLives;

        SpawnerController.onWaveCompleted += WaveCompleted;

        if (UIManager != null)
        {
            totalLifes = lifes;
            UIManager.UpdateTotalLives(totalLifes);

            CurrentWave = 1;
            UIManager.UpdateTotalWaves(CurrentWave);
        }
    }

    private void OnDisable()
    {
        EnemyController.onPathFinished -= RemoveLives;

        SpawnerController.onWaveCompleted -= WaveCompleted;
    }
}
