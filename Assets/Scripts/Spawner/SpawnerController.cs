using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    public enum SpawnerType { Fixed = 0, Random }

    [Header("General Settings")]
    [SerializeField] private SpawnerType spawnerType = SpawnerType.Fixed;

    [Header("Fixed Time Settings")]
    [SerializeField] private float spawnDelay = 1f;

    [Header("Random Timer Settings")]
    [SerializeField] private float minRandomSpawnDelay = 1f;
    [SerializeField] private float maxRandomSpawnDelay = 4f;

    [Header("Enemy Settings")]
    [SerializeField] private int enemiesToSpawn = 10;
    [SerializeField] private GameObject enemy;
    int enemiesSpawned = 0;

    [Header("Pooler Settings")]
    [SerializeField] private int enemiesToStore = 5;
    ObjectPooler pooler;

    [Header("Waves Settings")]
    [SerializeField] private float timeBetweenWaves = 4;
    int enemiesRemaining = 0;

    void Start()
    {
        StartCoroutine(StartTimer());

        pooler = new ObjectPooler();
        pooler.StorePoolObject(enemiesToStore, enemy);

        enemiesRemaining = enemiesToSpawn;
    }

    float SetRandomDelayTime()
    {
        return Random.Range(minRandomSpawnDelay, maxRandomSpawnDelay);
    }

    void SpawnEnemy()
    {
        GameObject newEnemy = pooler.GetPoolObject(enemy);
        newEnemy.transform.position = transform.position;
        newEnemy.SetActive(true);
    }

    private IEnumerator StartTimer()
    {
        if (spawnerType == SpawnerType.Fixed)
        {
            yield return new WaitForSeconds(spawnDelay);
        }
        else
        {
            yield return new WaitForSeconds(SetRandomDelayTime());
        }

        if (enemiesToSpawn > 0)
        {
            SpawnEnemy();
            enemiesSpawned++;
            enemiesToSpawn--;
            StartCoroutine(StartTimer());
        }
    }

    IEnumerator NextWave()
    {
        yield return new WaitForSeconds(timeBetweenWaves);

        enemiesToSpawn = enemiesRemaining = enemiesSpawned;
        enemiesSpawned = 0;

        StartCoroutine(StartTimer());
    }

    void EnemyDismiss(EnemyController enemy)
    {
        enemiesRemaining--;

        if (enemiesRemaining <= 0)
        {
            StartCoroutine(NextWave());
        }
    }

    private void OnEnable()
    {
        EnemyController.onPathFinished += EnemyDismiss;
        HealthManager.onEnemyDead += EnemyDismiss;
    }

    private void OnDisable()
    {
        EnemyController.onPathFinished -= EnemyDismiss;
        HealthManager.onEnemyDead -= EnemyDismiss;
    }
}
