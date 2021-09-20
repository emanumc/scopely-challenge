using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CreepType 
{ 
    Small, 
    Big 
}

public enum SpawnPoint 
{ 
    Northwest, 
    Northeast, 
    Southwest, 
    Southeast 
}

public class CreepSpawner : MonoBehaviour
{
    [System.Serializable]
    private class CreepTypeToPool : SerializableDictionary<CreepType, PrototypePool> { }

    [SerializeField] private CreepTypeToPool _creepTypeToPool = new CreepTypeToPool();

    [System.Serializable]
    private class SpawnPointToTransform : SerializableDictionary<SpawnPoint, Transform> { }

    [SerializeField] private SpawnPointToTransform _spawnPointToTransform = new SpawnPointToTransform();

    [SerializeField] private Wave[] _waves = new Wave[0];

    public List<(CreepType, Health)> Enemies { get; private set; } = new List<(CreepType, Health)>();

    public bool FinishSpawning { get; private set; }

    public Wave CurrentWave { get; private set; }

    private void Start()
    {
        FinishSpawning = false;

        foreach (var pool in _creepTypeToPool.Values)
        {
            pool.CreatePool();
        }

        StartCoroutine(SpawnWaves());
        StartCoroutine(DespawnDeadEnemies());
    }

    private IEnumerator SpawnWaves()
    {
        for (int i = 0; i < _waves.Length; i++)
        {
            CurrentWave = _waves[i];
            float maxWaitTime = float.MinValue;

            foreach (var spawn in CurrentWave.Spawns)
            {
                SpawnPoint spawnPoint = spawn.Key;
                Wave.CreepSpawn creepSpawn = spawn.Value;
                StartCoroutine(SpawnCreeps(spawnPoint, creepSpawn));

                float waitTime = creepSpawn.numSpawns * creepSpawn.spawnIntervalInSeconds;
                maxWaitTime = Mathf.Max(maxWaitTime, waitTime);
            }

            yield return new WaitForSeconds(maxWaitTime);
            yield return new WaitUntil(() => Enemies.Count <= 0);
        }

        FinishSpawning = true;
    }

    private IEnumerator SpawnCreeps(SpawnPoint spawnPoint, Wave.CreepSpawn creepSpawn)
    {
        Vector3 spawnPointPosition = _spawnPointToTransform[spawnPoint].position;
        CreepType creepType = creepSpawn.creepType;
        PrototypePool pool = _creepTypeToPool[creepType];
        float spawnInterval = creepSpawn.spawnIntervalInSeconds;

        for (int i = 0; i < creepSpawn.numSpawns; i++)
        {
            GameObject enemy = pool.Spawn(spawnPointPosition);
            Health enemyHealth = enemy.GetComponentInParent<Health>();
            enemyHealth.Restore(enemyHealth.MaxValue);

            Enemies.Add((creepType, enemyHealth));

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private IEnumerator DespawnDeadEnemies()
    {
        while (!FinishSpawning || Enemies.Count > 0)
        {
            for (int i = Enemies.Count - 1; i >= 0; i--)
            {
                CreepType creepType = Enemies[i].Item1;
                Health health = Enemies[i].Item2;
                if (health.Value <= 0)
                {
                    Enemies.RemoveAt(i);

                    _creepTypeToPool[creepType].Despawn(health.gameObject);
                }
            }
            yield return null;
        }
    }
}
