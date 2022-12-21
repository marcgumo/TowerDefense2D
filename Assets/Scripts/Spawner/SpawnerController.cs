using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpawnerController : MonoBehaviour
{
    public enum SpawnerType { Fixed = 0, Random }

    [Header("General Settings")]
    [SerializeField] private SpawnerType spawnerType = SpawnerType.Fixed;
    public int currentLevel;

    [Header("Fixed Time Settings")]
    [SerializeField] private float spawnDelay = 1f;

    [Header("Random Timer Settings")]
    [SerializeField] private float minRandomSpawnDelay = 1f;
    [SerializeField] private float maxRandomSpawnDelay = 4f;

    [Header("Enemy Settings")]
    [SerializeField] private int enemiesToSpawn = 10;
    int enemiesSpawned = 0;

    [SerializeField] private List<GameObject> enemyList;

    [Header("Pooler Settings")]
    [SerializeField] private int enemiesToStore = 5;
    ObjectPooler pooler;

    [Header("Waves Settings")]
    [SerializeField] private float timeBetweenWaves = 4;
    int enemiesRemaining = 0;

    public static Action onWaveCompleted;

    private GameObject newEnemy;
    private int totalEnemiesToSpawn;
    private UIController UIManager;

    void Start()
    {
        StartCoroutine(StartTimer());

        pooler = new ObjectPooler();

        pooler.StorePoolObject(enemiesToStore, enemyList[0]);
        pooler.StorePoolObject(enemiesToStore, enemyList[1]);
        pooler.StorePoolObject(enemiesToStore, enemyList[2]);
        pooler.StorePoolObject(enemiesToStore, enemyList[3]);
        pooler.StorePoolObject(enemiesToStore, enemyList[4]);

        totalEnemiesToSpawn = enemiesRemaining = enemiesToSpawn;
    }

    float SetRandomDelayTime()
    {
        return UnityEngine.Random.Range(minRandomSpawnDelay, maxRandomSpawnDelay);
    }

    void SpawnEnemy()
    {
        //GameObject newEnemy = pooler.GetPoolObject(enemy);

        if (enemiesSpawned < totalEnemiesToSpawn * 0.2f)
        {
            newEnemy = pooler.GetPoolObject(enemyList[0], enemyList[0].GetComponent<HealthManager>().GetEnemyType());
        }
        else if (enemiesSpawned < totalEnemiesToSpawn * 0.4f)
        {
            newEnemy = pooler.GetPoolObject(enemyList[1], enemyList[1].GetComponent<HealthManager>().GetEnemyType());
        }
        else if (enemiesSpawned < totalEnemiesToSpawn * 0.6f)
        {
            newEnemy = pooler.GetPoolObject(enemyList[2], enemyList[2].GetComponent<HealthManager>().GetEnemyType());
        }
        else if (enemiesSpawned < totalEnemiesToSpawn * 0.8f)
        {
            newEnemy = pooler.GetPoolObject(enemyList[3], enemyList[3].GetComponent<HealthManager>().GetEnemyType());
        }
        else
        {
            newEnemy = pooler.GetPoolObject(enemyList[4], enemyList[4].GetComponent<HealthManager>().GetEnemyType());
        }

        newEnemy.transform.position = transform.position;
        newEnemy.SetActive(true);
        newEnemy.GetComponent<EnemyController>().Flip();
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

        enemiesToSpawn = enemiesRemaining = enemiesSpawned + 2;
        enemiesSpawned = 0;

        StartCoroutine(StartTimer());

        onWaveCompleted?.Invoke();
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

        UIManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIController>();

        if (currentLevel != UIManager.GetCurrentLevel())
        {
            pooler = new ObjectPooler();
            pooler.RemovePoolObjects();
        }

        if (enemiesToSpawn == 0)
        {
            enemiesRemaining = enemiesToSpawn = totalEnemiesToSpawn;
            enemiesSpawned = 0;

            pooler.RemovePoolObjects();

            pooler.StorePoolObject(enemiesToStore, enemyList[0]);
            pooler.StorePoolObject(enemiesToStore, enemyList[1]);
            pooler.StorePoolObject(enemiesToStore, enemyList[2]);
            pooler.StorePoolObject(enemiesToStore, enemyList[3]);
            pooler.StorePoolObject(enemiesToStore, enemyList[4]);

            GameObject[] turrets = GameObject.FindGameObjectsWithTag("Turret");
            for (int i = 0; i < turrets.Length; i++)
            {
                turrets[i].GetComponentInParent<NodeController>().SetTurret(null);
                Destroy(turrets[i].gameObject);
            }

            StartCoroutine(StartTimer());
        }
    }

    private void OnDisable()
    {
        EnemyController.onPathFinished -= EnemyDismiss;
        HealthManager.onEnemyDead -= EnemyDismiss;
    }
}
